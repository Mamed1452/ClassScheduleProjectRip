using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces
{
    [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces)]
    public class ClassScheduleModeSpacesAppService : ClassScheduleProjectAppServiceBase, IClassScheduleModeSpacesAppService
    {
        private readonly IRepository<ClassScheduleModeSpace, long> _classScheduleModeSpaceRepository;
        private readonly IClassScheduleModeSpacesExcelExporter _classScheduleModeSpacesExcelExporter;
        private readonly IRepository<ListOfClassScheduleModeSpace, long> _lookup_listOfClassScheduleModeSpaceRepository;
        private readonly IRepository<UniversityProfessor, int> _lookup_universityProfessorRepository;
        private readonly IRepository<WorkTimeInDay, long> _lookup_workTimeInDayRepository;
        private readonly IRepository<Lesson, long> _lookup_lessonRepository;

        public ClassScheduleModeSpacesAppService(IRepository<ClassScheduleModeSpace, long> classScheduleModeSpaceRepository, IClassScheduleModeSpacesExcelExporter classScheduleModeSpacesExcelExporter, IRepository<ListOfClassScheduleModeSpace, long> lookup_listOfClassScheduleModeSpaceRepository, IRepository<UniversityProfessor, int> lookup_universityProfessorRepository, IRepository<WorkTimeInDay, long> lookup_workTimeInDayRepository, IRepository<Lesson, long> lookup_lessonRepository)
        {
            _classScheduleModeSpaceRepository = classScheduleModeSpaceRepository;
            _classScheduleModeSpacesExcelExporter = classScheduleModeSpacesExcelExporter;
            _lookup_listOfClassScheduleModeSpaceRepository = lookup_listOfClassScheduleModeSpaceRepository;
            _lookup_universityProfessorRepository = lookup_universityProfessorRepository;
            _lookup_workTimeInDayRepository = lookup_workTimeInDayRepository;
            _lookup_lessonRepository = lookup_lessonRepository;

        }

        public async Task<PagedResultDto<GetClassScheduleModeSpaceForViewDto>> GetAll(GetAllClassScheduleModeSpacesInput input)
        {

            var filteredClassScheduleModeSpaces = _classScheduleModeSpaceRepository.GetAll()
                        .Include(e => e.ListOfClassScheduleModeSpaceFk)
                        .Include(e => e.UniversityProfessorFk)
                        .Include(e => e.WorkTimeInDayFk)
                        .Include(e => e.LessonFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameClassScheduleModeSpaces.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameClassScheduleModeSpacesFilter), e => e.NameClassScheduleModeSpaces == input.NameClassScheduleModeSpacesFilter)
                        .WhereIf(input.IsLockFilter.HasValue && input.IsLockFilter > -1, e => (input.IsLockFilter == 1 && e.IsLock) || (input.IsLockFilter == 0 && !e.IsLock))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceNameFilter), e => e.ListOfClassScheduleModeSpaceFk != null && e.ListOfClassScheduleModeSpaceFk.ListOfClassScheduleModeSpaceName == input.ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorUniversityProfessorNameFilter), e => e.UniversityProfessorFk != null && e.UniversityProfessorFk.UniversityProfessorName == input.UniversityProfessorUniversityProfessorNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkTimeInDayNameWorkTimeInDayFilter), e => e.WorkTimeInDayFk != null && e.WorkTimeInDayFk.NameWorkTimeInDay == input.WorkTimeInDayNameWorkTimeInDayFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonNameLessonFilter), e => e.LessonFk != null && e.LessonFk.NameLesson == input.LessonNameLessonFilter)
                        .WhereIf(input.ListOfClassScheduleModeSpaceIdFilter.HasValue, e => false || e.ListOfClassScheduleModeSpaceId == input.ListOfClassScheduleModeSpaceIdFilter.Value);

            var pagedAndFilteredClassScheduleModeSpaces = filteredClassScheduleModeSpaces
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var classScheduleModeSpaces = from o in pagedAndFilteredClassScheduleModeSpaces
                                          join o1 in _lookup_listOfClassScheduleModeSpaceRepository.GetAll() on o.ListOfClassScheduleModeSpaceId equals o1.Id into j1
                                          from s1 in j1.DefaultIfEmpty()

                                          join o2 in _lookup_universityProfessorRepository.GetAll() on o.UniversityProfessorId equals o2.Id into j2
                                          from s2 in j2.DefaultIfEmpty()

                                          join o3 in _lookup_workTimeInDayRepository.GetAll() on o.WorkTimeInDayId equals o3.Id into j3
                                          from s3 in j3.DefaultIfEmpty()

                                          join o4 in _lookup_lessonRepository.GetAll() on o.LessonId equals o4.Id into j4
                                          from s4 in j4.DefaultIfEmpty()

                                          select new
                                          {

                                              o.NameClassScheduleModeSpaces,
                                              o.IsLock,
                                              Id = o.Id,
                                              ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceName = s1 == null || s1.ListOfClassScheduleModeSpaceName == null ? "" : s1.ListOfClassScheduleModeSpaceName.ToString(),
                                              UniversityProfessorUniversityProfessorName = s2 == null || s2.UniversityProfessorName == null ? "" : s2.UniversityProfessorName.ToString(),
                                              WorkTimeInDayNameWorkTimeInDay = s3 == null || s3.NameWorkTimeInDay == null ? "" : s3.NameWorkTimeInDay.ToString(),
                                              LessonNameLesson = s4 == null || s4.NameLesson == null ? "" : s4.NameLesson.ToString()
                                          };

            var totalCount = await filteredClassScheduleModeSpaces.CountAsync();

            var dbList = await classScheduleModeSpaces.ToListAsync();
            var results = new List<GetClassScheduleModeSpaceForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClassScheduleModeSpaceForViewDto()
                {
                    ClassScheduleModeSpace = new ClassScheduleModeSpaceDto
                    {

                        NameClassScheduleModeSpaces = o.NameClassScheduleModeSpaces,
                        IsLock = o.IsLock,
                        Id = o.Id,
                    },
                    ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceName = o.ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceName,
                    UniversityProfessorUniversityProfessorName = o.UniversityProfessorUniversityProfessorName,
                    WorkTimeInDayNameWorkTimeInDay = o.WorkTimeInDayNameWorkTimeInDay,
                    LessonNameLesson = o.LessonNameLesson
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClassScheduleModeSpaceForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClassScheduleModeSpaceForViewDto> GetClassScheduleModeSpaceForView(long id)
        {
            var classScheduleModeSpace = await _classScheduleModeSpaceRepository.GetAsync(id);

            var output = new GetClassScheduleModeSpaceForViewDto { ClassScheduleModeSpace = ObjectMapper.Map<ClassScheduleModeSpaceDto>(classScheduleModeSpace) };

            if (output.ClassScheduleModeSpace.ListOfClassScheduleModeSpaceId != null)
            {
                var _lookupListOfClassScheduleModeSpace = await _lookup_listOfClassScheduleModeSpaceRepository.FirstOrDefaultAsync((long)output.ClassScheduleModeSpace.ListOfClassScheduleModeSpaceId);
                output.ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceName = _lookupListOfClassScheduleModeSpace?.ListOfClassScheduleModeSpaceName?.ToString();
            }

            if (output.ClassScheduleModeSpace.UniversityProfessorId != null)
            {
                var _lookupUniversityProfessor = await _lookup_universityProfessorRepository.FirstOrDefaultAsync((int)output.ClassScheduleModeSpace.UniversityProfessorId);
                output.UniversityProfessorUniversityProfessorName = _lookupUniversityProfessor?.UniversityProfessorName?.ToString();
            }

            if (output.ClassScheduleModeSpace.WorkTimeInDayId != null)
            {
                var _lookupWorkTimeInDay = await _lookup_workTimeInDayRepository.FirstOrDefaultAsync((long)output.ClassScheduleModeSpace.WorkTimeInDayId);
                output.WorkTimeInDayNameWorkTimeInDay = _lookupWorkTimeInDay?.NameWorkTimeInDay?.ToString();
            }

            if (output.ClassScheduleModeSpace.LessonId != null)
            {
                var _lookupLesson = await _lookup_lessonRepository.FirstOrDefaultAsync((long)output.ClassScheduleModeSpace.LessonId);
                output.LessonNameLesson = _lookupLesson?.NameLesson?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces_Edit)]
        public async Task<GetClassScheduleModeSpaceForEditOutput> GetClassScheduleModeSpaceForEdit(EntityDto<long> input)
        {
            var classScheduleModeSpace = await _classScheduleModeSpaceRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClassScheduleModeSpaceForEditOutput { ClassScheduleModeSpace = ObjectMapper.Map<CreateOrEditClassScheduleModeSpaceDto>(classScheduleModeSpace) };

            if (output.ClassScheduleModeSpace.ListOfClassScheduleModeSpaceId != null)
            {
                var _lookupListOfClassScheduleModeSpace = await _lookup_listOfClassScheduleModeSpaceRepository.FirstOrDefaultAsync((long)output.ClassScheduleModeSpace.ListOfClassScheduleModeSpaceId);
                output.ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceName = _lookupListOfClassScheduleModeSpace?.ListOfClassScheduleModeSpaceName?.ToString();
            }

            if (output.ClassScheduleModeSpace.UniversityProfessorId != null)
            {
                var _lookupUniversityProfessor = await _lookup_universityProfessorRepository.FirstOrDefaultAsync((int)output.ClassScheduleModeSpace.UniversityProfessorId);
                output.UniversityProfessorUniversityProfessorName = _lookupUniversityProfessor?.UniversityProfessorName?.ToString();
            }

            if (output.ClassScheduleModeSpace.WorkTimeInDayId != null)
            {
                var _lookupWorkTimeInDay = await _lookup_workTimeInDayRepository.FirstOrDefaultAsync((long)output.ClassScheduleModeSpace.WorkTimeInDayId);
                output.WorkTimeInDayNameWorkTimeInDay = _lookupWorkTimeInDay?.NameWorkTimeInDay?.ToString();
            }

            if (output.ClassScheduleModeSpace.LessonId != null)
            {
                var _lookupLesson = await _lookup_lessonRepository.FirstOrDefaultAsync((long)output.ClassScheduleModeSpace.LessonId);
                output.LessonNameLesson = _lookupLesson?.NameLesson?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClassScheduleModeSpaceDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces_Create)]
        protected virtual async Task Create(CreateOrEditClassScheduleModeSpaceDto input)
        {
            var classScheduleModeSpace = ObjectMapper.Map<ClassScheduleModeSpace>(input);

            if (AbpSession.TenantId != null)
            {
                classScheduleModeSpace.TenantId = (int)AbpSession.TenantId;
            }

            await _classScheduleModeSpaceRepository.InsertAsync(classScheduleModeSpace);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces_Edit)]
        protected virtual async Task Update(CreateOrEditClassScheduleModeSpaceDto input)
        {
            var classScheduleModeSpace = await _classScheduleModeSpaceRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, classScheduleModeSpace);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _classScheduleModeSpaceRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetClassScheduleModeSpacesToExcel(GetAllClassScheduleModeSpacesForExcelInput input)
        {

            var filteredClassScheduleModeSpaces = _classScheduleModeSpaceRepository.GetAll()
                        .Include(e => e.ListOfClassScheduleModeSpaceFk)
                        .Include(e => e.UniversityProfessorFk)
                        .Include(e => e.WorkTimeInDayFk)
                        .Include(e => e.LessonFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameClassScheduleModeSpaces.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameClassScheduleModeSpacesFilter), e => e.NameClassScheduleModeSpaces == input.NameClassScheduleModeSpacesFilter)
                        .WhereIf(input.IsLockFilter.HasValue && input.IsLockFilter > -1, e => (input.IsLockFilter == 1 && e.IsLock) || (input.IsLockFilter == 0 && !e.IsLock))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceNameFilter), e => e.ListOfClassScheduleModeSpaceFk != null && e.ListOfClassScheduleModeSpaceFk.ListOfClassScheduleModeSpaceName == input.ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorUniversityProfessorNameFilter), e => e.UniversityProfessorFk != null && e.UniversityProfessorFk.UniversityProfessorName == input.UniversityProfessorUniversityProfessorNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkTimeInDayNameWorkTimeInDayFilter), e => e.WorkTimeInDayFk != null && e.WorkTimeInDayFk.NameWorkTimeInDay == input.WorkTimeInDayNameWorkTimeInDayFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonNameLessonFilter), e => e.LessonFk != null && e.LessonFk.NameLesson == input.LessonNameLessonFilter);

            var query = (from o in filteredClassScheduleModeSpaces
                         join o1 in _lookup_listOfClassScheduleModeSpaceRepository.GetAll() on o.ListOfClassScheduleModeSpaceId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_universityProfessorRepository.GetAll() on o.UniversityProfessorId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_workTimeInDayRepository.GetAll() on o.WorkTimeInDayId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _lookup_lessonRepository.GetAll() on o.LessonId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         select new GetClassScheduleModeSpaceForViewDto()
                         {
                             ClassScheduleModeSpace = new ClassScheduleModeSpaceDto
                             {
                                 NameClassScheduleModeSpaces = o.NameClassScheduleModeSpaces,
                                 IsLock = o.IsLock,
                                 Id = o.Id
                             },
                             ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceName = s1 == null || s1.ListOfClassScheduleModeSpaceName == null ? "" : s1.ListOfClassScheduleModeSpaceName.ToString(),
                             UniversityProfessorUniversityProfessorName = s2 == null || s2.UniversityProfessorName == null ? "" : s2.UniversityProfessorName.ToString(),
                             WorkTimeInDayNameWorkTimeInDay = s3 == null || s3.NameWorkTimeInDay == null ? "" : s3.NameWorkTimeInDay.ToString(),
                             LessonNameLesson = s4 == null || s4.NameLesson == null ? "" : s4.NameLesson.ToString()
                         });

            var classScheduleModeSpaceListDtos = await query.ToListAsync();

            return _classScheduleModeSpacesExcelExporter.ExportToFile(classScheduleModeSpaceListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces)]
        public async Task<PagedResultDto<ClassScheduleModeSpaceListOfClassScheduleModeSpaceLookupTableDto>> GetAllListOfClassScheduleModeSpaceForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_listOfClassScheduleModeSpaceRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.ListOfClassScheduleModeSpaceName != null && e.ListOfClassScheduleModeSpaceName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var listOfClassScheduleModeSpaceList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleModeSpaceListOfClassScheduleModeSpaceLookupTableDto>();
            foreach (var listOfClassScheduleModeSpace in listOfClassScheduleModeSpaceList)
            {
                lookupTableDtoList.Add(new ClassScheduleModeSpaceListOfClassScheduleModeSpaceLookupTableDto
                {
                    Id = listOfClassScheduleModeSpace.Id,
                    DisplayName = listOfClassScheduleModeSpace.ListOfClassScheduleModeSpaceName?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleModeSpaceListOfClassScheduleModeSpaceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces)]
        public async Task<PagedResultDto<ClassScheduleModeSpaceUniversityProfessorLookupTableDto>> GetAllUniversityProfessorForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_universityProfessorRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.UniversityProfessorName != null && e.UniversityProfessorName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var universityProfessorList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleModeSpaceUniversityProfessorLookupTableDto>();
            foreach (var universityProfessor in universityProfessorList)
            {
                lookupTableDtoList.Add(new ClassScheduleModeSpaceUniversityProfessorLookupTableDto
                {
                    Id = universityProfessor.Id,
                    DisplayName = universityProfessor.UniversityProfessorName?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleModeSpaceUniversityProfessorLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces)]
        public async Task<PagedResultDto<ClassScheduleModeSpaceWorkTimeInDayLookupTableDto>> GetAllWorkTimeInDayForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_workTimeInDayRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameWorkTimeInDay != null && e.NameWorkTimeInDay.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var workTimeInDayList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleModeSpaceWorkTimeInDayLookupTableDto>();
            foreach (var workTimeInDay in workTimeInDayList)
            {
                lookupTableDtoList.Add(new ClassScheduleModeSpaceWorkTimeInDayLookupTableDto
                {
                    Id = workTimeInDay.Id,
                    DisplayName = workTimeInDay.NameWorkTimeInDay?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleModeSpaceWorkTimeInDayLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleModeSpaces)]
        public async Task<PagedResultDto<ClassScheduleModeSpaceLessonLookupTableDto>> GetAllLessonForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_lessonRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameLesson != null && e.NameLesson.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var lessonList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleModeSpaceLessonLookupTableDto>();
            foreach (var lesson in lessonList)
            {
                lookupTableDtoList.Add(new ClassScheduleModeSpaceLessonLookupTableDto
                {
                    Id = lesson.Id,
                    DisplayName = lesson.NameLesson?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleModeSpaceLessonLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}