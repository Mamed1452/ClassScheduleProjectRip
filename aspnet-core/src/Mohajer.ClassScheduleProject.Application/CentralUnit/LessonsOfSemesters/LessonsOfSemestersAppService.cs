using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters
{
    [AbpAuthorize(AppPermissions.Pages_LessonsOfSemesters)]
    public class LessonsOfSemestersAppService : ClassScheduleProjectAppServiceBase, ILessonsOfSemestersAppService
    {
        private readonly IRepository<LessonsOfSemester, long> _lessonsOfSemesterRepository;
        private readonly ILessonsOfSemestersExcelExporter _lessonsOfSemestersExcelExporter;
        private readonly IRepository<Lesson, long> _lookup_lessonRepository;
        private readonly IRepository<Semester, long> _lookup_semesterRepository;

        public LessonsOfSemestersAppService(IRepository<LessonsOfSemester, long> lessonsOfSemesterRepository, ILessonsOfSemestersExcelExporter lessonsOfSemestersExcelExporter, IRepository<Lesson, long> lookup_lessonRepository, IRepository<Semester, long> lookup_semesterRepository)
        {
            _lessonsOfSemesterRepository = lessonsOfSemesterRepository;
            _lessonsOfSemestersExcelExporter = lessonsOfSemestersExcelExporter;
            _lookup_lessonRepository = lookup_lessonRepository;
            _lookup_semesterRepository = lookup_semesterRepository;

        }

        public async Task<PagedResultDto<GetLessonsOfSemesterForViewDto>> GetAll(GetAllLessonsOfSemestersInput input)
        {
            var lessonOfSemesterTypeFilter = input.LessonOfSemesterTypeFilter.HasValue
                        ? (LessonOfSemesterTypeEnum)input.LessonOfSemesterTypeFilter
                        : default;

            var filteredLessonsOfSemesters = _lessonsOfSemesterRepository.GetAll()
                        .Include(e => e.LessonFk)
                        .Include(e => e.SemesterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.LessonOfSemesterTypeFilter.HasValue && input.LessonOfSemesterTypeFilter > -1, e => e.LessonOfSemesterType == lessonOfSemesterTypeFilter)
                        .WhereIf(input.MinNumberOfClassesToBeFormedFilter != null, e => e.NumberOfClassesToBeFormed >= input.MinNumberOfClassesToBeFormedFilter)
                        .WhereIf(input.MaxNumberOfClassesToBeFormedFilter != null, e => e.NumberOfClassesToBeFormed <= input.MaxNumberOfClassesToBeFormedFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonNameLessonFilter), e => e.LessonFk != null && e.LessonFk.NameLesson == input.LessonNameLessonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SemesterSemesterNameFilter), e => e.SemesterFk != null && e.SemesterFk.SemesterName == input.SemesterSemesterNameFilter);

            var pagedAndFilteredLessonsOfSemesters = filteredLessonsOfSemesters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var lessonsOfSemesters = from o in pagedAndFilteredLessonsOfSemesters
                                     join o1 in _lookup_lessonRepository.GetAll() on o.LessonId equals o1.Id into j1
                                     from s1 in j1.DefaultIfEmpty()

                                     join o2 in _lookup_semesterRepository.GetAll() on o.SemesterId equals o2.Id into j2
                                     from s2 in j2.DefaultIfEmpty()

                                     select new
                                     {

                                         o.LessonOfSemesterType,
                                         o.NumberOfClassesToBeFormed,
                                         o.IsActive,
                                         Id = o.Id,
                                         LessonNameLesson = s1 == null || s1.NameLesson == null ? "" : s1.NameLesson.ToString(),
                                         SemesterSemesterName = s2 == null || s2.SemesterName == null ? "" : s2.SemesterName.ToString()
                                     };

            var totalCount = await filteredLessonsOfSemesters.CountAsync();

            var dbList = await lessonsOfSemesters.ToListAsync();
            var results = new List<GetLessonsOfSemesterForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetLessonsOfSemesterForViewDto()
                {
                    LessonsOfSemester = new LessonsOfSemesterDto
                    {

                        LessonOfSemesterType = o.LessonOfSemesterType,
                        NumberOfClassesToBeFormed = o.NumberOfClassesToBeFormed,
                        IsActive = o.IsActive,
                        Id = o.Id,
                    },
                    LessonNameLesson = o.LessonNameLesson,
                    SemesterSemesterName = o.SemesterSemesterName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetLessonsOfSemesterForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetLessonsOfSemesterForViewDto> GetLessonsOfSemesterForView(long id)
        {
            var lessonsOfSemester = await _lessonsOfSemesterRepository.GetAsync(id);

            var output = new GetLessonsOfSemesterForViewDto { LessonsOfSemester = ObjectMapper.Map<LessonsOfSemesterDto>(lessonsOfSemester) };

            if (output.LessonsOfSemester.LessonId != null)
            {
                var _lookupLesson = await _lookup_lessonRepository.FirstOrDefaultAsync((long)output.LessonsOfSemester.LessonId);
                output.LessonNameLesson = _lookupLesson?.NameLesson?.ToString();
            }

            if (output.LessonsOfSemester.SemesterId != null)
            {
                var _lookupSemester = await _lookup_semesterRepository.FirstOrDefaultAsync((long)output.LessonsOfSemester.SemesterId);
                output.SemesterSemesterName = _lookupSemester?.SemesterName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfSemesters_Edit)]
        public async Task<GetLessonsOfSemesterForEditOutput> GetLessonsOfSemesterForEdit(EntityDto<long> input)
        {
            var lessonsOfSemester = await _lessonsOfSemesterRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLessonsOfSemesterForEditOutput { LessonsOfSemester = ObjectMapper.Map<CreateOrEditLessonsOfSemesterDto>(lessonsOfSemester) };

            if (output.LessonsOfSemester.LessonId != null)
            {
                var _lookupLesson = await _lookup_lessonRepository.FirstOrDefaultAsync((long)output.LessonsOfSemester.LessonId);
                output.LessonNameLesson = _lookupLesson?.NameLesson?.ToString();
            }

            if (output.LessonsOfSemester.SemesterId != null)
            {
                var _lookupSemester = await _lookup_semesterRepository.FirstOrDefaultAsync((long)output.LessonsOfSemester.SemesterId);
                output.SemesterSemesterName = _lookupSemester?.SemesterName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLessonsOfSemesterDto input)
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

        [AbpAuthorize(AppPermissions.Pages_LessonsOfSemesters_Create)]
        protected virtual async Task Create(CreateOrEditLessonsOfSemesterDto input)
        {
            var lessonsOfSemester = ObjectMapper.Map<LessonsOfSemester>(input);

            if (AbpSession.TenantId != null)
            {
                lessonsOfSemester.TenantId = (int)AbpSession.TenantId;
            }

            await _lessonsOfSemesterRepository.InsertAsync(lessonsOfSemester);

        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfSemesters_Edit)]
        protected virtual async Task Update(CreateOrEditLessonsOfSemesterDto input)
        {
            var lessonsOfSemester = await _lessonsOfSemesterRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, lessonsOfSemester);

        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfSemesters_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _lessonsOfSemesterRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetLessonsOfSemestersToExcel(GetAllLessonsOfSemestersForExcelInput input)
        {
            var lessonOfSemesterTypeFilter = input.LessonOfSemesterTypeFilter.HasValue
                        ? (LessonOfSemesterTypeEnum)input.LessonOfSemesterTypeFilter
                        : default;

            var filteredLessonsOfSemesters = _lessonsOfSemesterRepository.GetAll()
                        .Include(e => e.LessonFk)
                        .Include(e => e.SemesterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.LessonOfSemesterTypeFilter.HasValue && input.LessonOfSemesterTypeFilter > -1, e => e.LessonOfSemesterType == lessonOfSemesterTypeFilter)
                        .WhereIf(input.MinNumberOfClassesToBeFormedFilter != null, e => e.NumberOfClassesToBeFormed >= input.MinNumberOfClassesToBeFormedFilter)
                        .WhereIf(input.MaxNumberOfClassesToBeFormedFilter != null, e => e.NumberOfClassesToBeFormed <= input.MaxNumberOfClassesToBeFormedFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonNameLessonFilter), e => e.LessonFk != null && e.LessonFk.NameLesson == input.LessonNameLessonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SemesterSemesterNameFilter), e => e.SemesterFk != null && e.SemesterFk.SemesterName == input.SemesterSemesterNameFilter);

            var query = (from o in filteredLessonsOfSemesters
                         join o1 in _lookup_lessonRepository.GetAll() on o.LessonId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_semesterRepository.GetAll() on o.SemesterId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetLessonsOfSemesterForViewDto()
                         {
                             LessonsOfSemester = new LessonsOfSemesterDto
                             {
                                 LessonOfSemesterType = o.LessonOfSemesterType,
                                 NumberOfClassesToBeFormed = o.NumberOfClassesToBeFormed,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             },
                             LessonNameLesson = s1 == null || s1.NameLesson == null ? "" : s1.NameLesson.ToString(),
                             SemesterSemesterName = s2 == null || s2.SemesterName == null ? "" : s2.SemesterName.ToString()
                         });

            var lessonsOfSemesterListDtos = await query.ToListAsync();

            return _lessonsOfSemestersExcelExporter.ExportToFile(lessonsOfSemesterListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfSemesters)]
        public async Task<PagedResultDto<LessonsOfSemesterLessonLookupTableDto>> GetAllLessonForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_lessonRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameLesson != null && e.NameLesson.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var lessonList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<LessonsOfSemesterLessonLookupTableDto>();
            foreach (var lesson in lessonList)
            {
                lookupTableDtoList.Add(new LessonsOfSemesterLessonLookupTableDto
                {
                    Id = lesson.Id,
                    DisplayName = lesson.NameLesson?.ToString()
                });
            }

            return new PagedResultDto<LessonsOfSemesterLessonLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfSemesters)]
        public async Task<PagedResultDto<LessonsOfSemesterSemesterLookupTableDto>> GetAllSemesterForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_semesterRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.SemesterName != null && e.SemesterName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var semesterList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<LessonsOfSemesterSemesterLookupTableDto>();
            foreach (var semester in semesterList)
            {
                lookupTableDtoList.Add(new LessonsOfSemesterSemesterLookupTableDto
                {
                    Id = semester.Id,
                    DisplayName = semester.SemesterName?.ToString()
                });
            }

            return new PagedResultDto<LessonsOfSemesterSemesterLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}