using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults
{
    [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults)]
    public class ClassScheduleResultsAppService : ClassScheduleProjectAppServiceBase, IClassScheduleResultsAppService
    {
        private readonly IRepository<ClassScheduleResult, long> _classScheduleResultRepository;
        private readonly IClassScheduleResultsExcelExporter _classScheduleResultsExcelExporter;
        private readonly IRepository<ListOfAllCalculatedResult, long> _lookup_listOfAllCalculatedResultRepository;
        private readonly IRepository<ClassScheduleModeSpace, long> _lookup_classScheduleModeSpaceRepository;

        public ClassScheduleResultsAppService(IRepository<ClassScheduleResult, long> classScheduleResultRepository, IClassScheduleResultsExcelExporter classScheduleResultsExcelExporter, IRepository<ListOfAllCalculatedResult, long> lookup_listOfAllCalculatedResultRepository, IRepository<ClassScheduleModeSpace, long> lookup_classScheduleModeSpaceRepository)
        {
            _classScheduleResultRepository = classScheduleResultRepository;
            _classScheduleResultsExcelExporter = classScheduleResultsExcelExporter;
            _lookup_listOfAllCalculatedResultRepository = lookup_listOfAllCalculatedResultRepository;
            _lookup_classScheduleModeSpaceRepository = lookup_classScheduleModeSpaceRepository;

        }

        public async Task<PagedResultDto<GetClassScheduleResultForViewDto>> GetAll(GetAllClassScheduleResultsInput input)
        {

            var filteredClassScheduleResults = _classScheduleResultRepository.GetAll()
                        .Include(e => e.ListOfAllCalculatedResultFk)
                        .Include(e => e.ClassScheduleModeSpaceFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinWorkTimeInDayIdFilter != null, e => e.WorkTimeInDayId >= input.MinWorkTimeInDayIdFilter)
                        .WhereIf(input.MaxWorkTimeInDayIdFilter != null, e => e.WorkTimeInDayId <= input.MaxWorkTimeInDayIdFilter)
                        .WhereIf(input.MinLessonIdFilter != null, e => e.LessonId >= input.MinLessonIdFilter)
                        .WhereIf(input.MaxLessonIdFilter != null, e => e.LessonId <= input.MaxLessonIdFilter)
                        .WhereIf(input.MinUniversityProfessorIdFilter != null, e => e.UniversityProfessorId >= input.MinUniversityProfessorIdFilter)
                        .WhereIf(input.MaxUniversityProfessorIdFilter != null, e => e.UniversityProfessorId <= input.MaxUniversityProfessorIdFilter)
                        .WhereIf(input.MinSemesterIdFilter != null, e => e.SemesterId >= input.MinSemesterIdFilter)
                        .WhereIf(input.MaxSemesterIdFilter != null, e => e.SemesterId <= input.MaxSemesterIdFilter)
                        .WhereIf(input.MinGradeIdFilter != null, e => e.GradeId >= input.MinGradeIdFilter)
                        .WhereIf(input.MaxGradeIdFilter != null, e => e.GradeId <= input.MaxGradeIdFilter)
                        .WhereIf(input.MinUniversityMajorIdFilter != null, e => e.UniversityMajorId >= input.MinUniversityMajorIdFilter)
                        .WhereIf(input.MaxUniversityMajorIdFilter != null, e => e.UniversityMajorId <= input.MaxUniversityMajorIdFilter)
                        .WhereIf(input.MinUniversityDepartmentIdFilter != null, e => e.UniversityDepartmentId >= input.MinUniversityDepartmentIdFilter)
                        .WhereIf(input.MaxUniversityDepartmentIdFilter != null, e => e.UniversityDepartmentId <= input.MaxUniversityDepartmentIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfAllCalculatedResultNameCalculatedResultFilter), e => e.ListOfAllCalculatedResultFk != null && e.ListOfAllCalculatedResultFk.NameCalculatedResult == input.ListOfAllCalculatedResultNameCalculatedResultFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter), e => e.ClassScheduleModeSpaceFk != null && e.ClassScheduleModeSpaceFk.NameClassScheduleModeSpaces == input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter)
                        .WhereIf(input.ListOfAllCalculatedResultIdFilter.HasValue, e => false || e.ListOfAllCalculatedResultId == input.ListOfAllCalculatedResultIdFilter.Value);

            var pagedAndFilteredClassScheduleResults = filteredClassScheduleResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var classScheduleResults = from o in pagedAndFilteredClassScheduleResults
                                       join o1 in _lookup_listOfAllCalculatedResultRepository.GetAll() on o.ListOfAllCalculatedResultId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()

                                       join o2 in _lookup_classScheduleModeSpaceRepository.GetAll() on o.ClassScheduleModeSpaceId equals o2.Id into j2
                                       from s2 in j2.DefaultIfEmpty()

                                       select new
                                       {

                                           o.WorkTimeInDayId,
                                           o.LessonId,
                                           o.UniversityProfessorId,
                                           o.SemesterId,
                                           o.GradeId,
                                           o.UniversityMajorId,
                                           o.UniversityDepartmentId,
                                           Id = o.Id,
                                           ListOfAllCalculatedResultNameCalculatedResult = s1 == null || s1.NameCalculatedResult == null ? "" : s1.NameCalculatedResult.ToString(),
                                           ClassScheduleModeSpaceNameClassScheduleModeSpaces = s2 == null || s2.NameClassScheduleModeSpaces == null ? "" : s2.NameClassScheduleModeSpaces.ToString()
                                       };

            var totalCount = await filteredClassScheduleResults.CountAsync();

            var dbList = await classScheduleResults.ToListAsync();
            var results = new List<GetClassScheduleResultForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClassScheduleResultForViewDto()
                {
                    ClassScheduleResult = new ClassScheduleResultDto
                    {

                        WorkTimeInDayId = o.WorkTimeInDayId,
                        LessonId = o.LessonId,
                        UniversityProfessorId = o.UniversityProfessorId,
                        SemesterId = o.SemesterId,
                        GradeId = o.GradeId,
                        UniversityMajorId = o.UniversityMajorId,
                        UniversityDepartmentId = o.UniversityDepartmentId,
                        Id = o.Id,
                    },
                    ListOfAllCalculatedResultNameCalculatedResult = o.ListOfAllCalculatedResultNameCalculatedResult,
                    ClassScheduleModeSpaceNameClassScheduleModeSpaces = o.ClassScheduleModeSpaceNameClassScheduleModeSpaces
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClassScheduleResultForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClassScheduleResultForViewDto> GetClassScheduleResultForView(long id)
        {
            var classScheduleResult = await _classScheduleResultRepository.GetAsync(id);

            var output = new GetClassScheduleResultForViewDto { ClassScheduleResult = ObjectMapper.Map<ClassScheduleResultDto>(classScheduleResult) };

            if (output.ClassScheduleResult.ListOfAllCalculatedResultId != null)
            {
                var _lookupListOfAllCalculatedResult = await _lookup_listOfAllCalculatedResultRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ListOfAllCalculatedResultId);
                output.ListOfAllCalculatedResultNameCalculatedResult = _lookupListOfAllCalculatedResult?.NameCalculatedResult?.ToString();
            }

            if (output.ClassScheduleResult.ClassScheduleModeSpaceId != null)
            {
                var _lookupClassScheduleModeSpace = await _lookup_classScheduleModeSpaceRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ClassScheduleModeSpaceId);
                output.ClassScheduleModeSpaceNameClassScheduleModeSpaces = _lookupClassScheduleModeSpace?.NameClassScheduleModeSpaces?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Edit)]
        public async Task<GetClassScheduleResultForEditOutput> GetClassScheduleResultForEdit(EntityDto<long> input)
        {
            var classScheduleResult = await _classScheduleResultRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClassScheduleResultForEditOutput { ClassScheduleResult = ObjectMapper.Map<CreateOrEditClassScheduleResultDto>(classScheduleResult) };

            if (output.ClassScheduleResult.ListOfAllCalculatedResultId != null)
            {
                var _lookupListOfAllCalculatedResult = await _lookup_listOfAllCalculatedResultRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ListOfAllCalculatedResultId);
                output.ListOfAllCalculatedResultNameCalculatedResult = _lookupListOfAllCalculatedResult?.NameCalculatedResult?.ToString();
            }

            if (output.ClassScheduleResult.ClassScheduleModeSpaceId != null)
            {
                var _lookupClassScheduleModeSpace = await _lookup_classScheduleModeSpaceRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ClassScheduleModeSpaceId);
                output.ClassScheduleModeSpaceNameClassScheduleModeSpaces = _lookupClassScheduleModeSpace?.NameClassScheduleModeSpaces?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClassScheduleResultDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Create)]
        protected virtual async Task Create(CreateOrEditClassScheduleResultDto input)
        {
            var classScheduleResult = ObjectMapper.Map<ClassScheduleResult>(input);

            if (AbpSession.TenantId != null)
            {
                classScheduleResult.TenantId = (int)AbpSession.TenantId;
            }

            await _classScheduleResultRepository.InsertAsync(classScheduleResult);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Edit)]
        protected virtual async Task Update(CreateOrEditClassScheduleResultDto input)
        {
            var classScheduleResult = await _classScheduleResultRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, classScheduleResult);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _classScheduleResultRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetClassScheduleResultsToExcel(GetAllClassScheduleResultsForExcelInput input)
        {

            var filteredClassScheduleResults = _classScheduleResultRepository.GetAll()
                        .Include(e => e.ListOfAllCalculatedResultFk)
                        .Include(e => e.ClassScheduleModeSpaceFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinWorkTimeInDayIdFilter != null, e => e.WorkTimeInDayId >= input.MinWorkTimeInDayIdFilter)
                        .WhereIf(input.MaxWorkTimeInDayIdFilter != null, e => e.WorkTimeInDayId <= input.MaxWorkTimeInDayIdFilter)
                        .WhereIf(input.MinLessonIdFilter != null, e => e.LessonId >= input.MinLessonIdFilter)
                        .WhereIf(input.MaxLessonIdFilter != null, e => e.LessonId <= input.MaxLessonIdFilter)
                        .WhereIf(input.MinUniversityProfessorIdFilter != null, e => e.UniversityProfessorId >= input.MinUniversityProfessorIdFilter)
                        .WhereIf(input.MaxUniversityProfessorIdFilter != null, e => e.UniversityProfessorId <= input.MaxUniversityProfessorIdFilter)
                        .WhereIf(input.MinSemesterIdFilter != null, e => e.SemesterId >= input.MinSemesterIdFilter)
                        .WhereIf(input.MaxSemesterIdFilter != null, e => e.SemesterId <= input.MaxSemesterIdFilter)
                        .WhereIf(input.MinGradeIdFilter != null, e => e.GradeId >= input.MinGradeIdFilter)
                        .WhereIf(input.MaxGradeIdFilter != null, e => e.GradeId <= input.MaxGradeIdFilter)
                        .WhereIf(input.MinUniversityMajorIdFilter != null, e => e.UniversityMajorId >= input.MinUniversityMajorIdFilter)
                        .WhereIf(input.MaxUniversityMajorIdFilter != null, e => e.UniversityMajorId <= input.MaxUniversityMajorIdFilter)
                        .WhereIf(input.MinUniversityDepartmentIdFilter != null, e => e.UniversityDepartmentId >= input.MinUniversityDepartmentIdFilter)
                        .WhereIf(input.MaxUniversityDepartmentIdFilter != null, e => e.UniversityDepartmentId <= input.MaxUniversityDepartmentIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfAllCalculatedResultNameCalculatedResultFilter), e => e.ListOfAllCalculatedResultFk != null && e.ListOfAllCalculatedResultFk.NameCalculatedResult == input.ListOfAllCalculatedResultNameCalculatedResultFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter), e => e.ClassScheduleModeSpaceFk != null && e.ClassScheduleModeSpaceFk.NameClassScheduleModeSpaces == input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter);

            var query = (from o in filteredClassScheduleResults
                         join o1 in _lookup_listOfAllCalculatedResultRepository.GetAll() on o.ListOfAllCalculatedResultId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_classScheduleModeSpaceRepository.GetAll() on o.ClassScheduleModeSpaceId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetClassScheduleResultForViewDto()
                         {
                             ClassScheduleResult = new ClassScheduleResultDto
                             {
                                 WorkTimeInDayId = o.WorkTimeInDayId,
                                 LessonId = o.LessonId,
                                 UniversityProfessorId = o.UniversityProfessorId,
                                 SemesterId = o.SemesterId,
                                 GradeId = o.GradeId,
                                 UniversityMajorId = o.UniversityMajorId,
                                 UniversityDepartmentId = o.UniversityDepartmentId,
                                 Id = o.Id
                             },
                             ListOfAllCalculatedResultNameCalculatedResult = s1 == null || s1.NameCalculatedResult == null ? "" : s1.NameCalculatedResult.ToString(),
                             ClassScheduleModeSpaceNameClassScheduleModeSpaces = s2 == null || s2.NameClassScheduleModeSpaces == null ? "" : s2.NameClassScheduleModeSpaces.ToString()
                         });

            var classScheduleResultListDtos = await query.ToListAsync();

            return _classScheduleResultsExcelExporter.ExportToFile(classScheduleResultListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults)]
        public async Task<PagedResultDto<ClassScheduleResultListOfAllCalculatedResultLookupTableDto>> GetAllListOfAllCalculatedResultForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_listOfAllCalculatedResultRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameCalculatedResult != null && e.NameCalculatedResult.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var listOfAllCalculatedResultList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleResultListOfAllCalculatedResultLookupTableDto>();
            foreach (var listOfAllCalculatedResult in listOfAllCalculatedResultList)
            {
                lookupTableDtoList.Add(new ClassScheduleResultListOfAllCalculatedResultLookupTableDto
                {
                    Id = listOfAllCalculatedResult.Id,
                    DisplayName = listOfAllCalculatedResult.NameCalculatedResult?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleResultListOfAllCalculatedResultLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults)]
        public async Task<PagedResultDto<ClassScheduleResultClassScheduleModeSpaceLookupTableDto>> GetAllClassScheduleModeSpaceForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_classScheduleModeSpaceRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameClassScheduleModeSpaces != null && e.NameClassScheduleModeSpaces.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var classScheduleModeSpaceList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleResultClassScheduleModeSpaceLookupTableDto>();
            foreach (var classScheduleModeSpace in classScheduleModeSpaceList)
            {
                lookupTableDtoList.Add(new ClassScheduleResultClassScheduleModeSpaceLookupTableDto
                {
                    Id = classScheduleModeSpace.Id,
                    DisplayName = classScheduleModeSpace.NameClassScheduleModeSpaces?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleResultClassScheduleModeSpaceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}