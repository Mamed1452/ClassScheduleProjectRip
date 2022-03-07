using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;

using Mohajer.ClassScheduleProject.CentralUnit.Lessons;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons
{
    [AbpAuthorize(AppPermissions.Pages_Lessons)]
    public class LessonsAppService : ClassScheduleProjectAppServiceBase, ILessonsAppService
    {
        private readonly IRepository<Lesson, long> _lessonRepository;
        private readonly ILessonsExcelExporter _lessonsExcelExporter;
        private readonly IRepository<ClassroomBuilding, int> _lookup_classroomBuildingRepository;

        public LessonsAppService(IRepository<Lesson, long> lessonRepository, ILessonsExcelExporter lessonsExcelExporter, IRepository<ClassroomBuilding, int> lookup_classroomBuildingRepository)
        {
            _lessonRepository = lessonRepository;
            _lessonsExcelExporter = lessonsExcelExporter;
            _lookup_classroomBuildingRepository = lookup_classroomBuildingRepository;

        }

        public async Task<PagedResultDto<GetLessonForViewDto>> GetAll(GetAllLessonsInput input)
        {
            var lessonTypeFilter = input.LessonTypeFilter.HasValue
                        ? (LessonTypeEnum)input.LessonTypeFilter
                        : default;
            var classificationLessonFilter = input.ClassificationLessonFilter.HasValue
                ? (ClassificationLessonEnum)input.ClassificationLessonFilter
                : default;

            var filteredLessons = _lessonRepository.GetAll()
                        .Include(e => e.ClassroomBuildingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameLesson.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameLessonFilter), e => e.NameLesson == input.NameLessonFilter)
                        .WhereIf(input.MinHoursPerWeekFilter != null, e => e.HoursPerWeek >= input.MinHoursPerWeekFilter)
                        .WhereIf(input.MaxHoursPerWeekFilter != null, e => e.HoursPerWeek <= input.MaxHoursPerWeekFilter)
                        .WhereIf(input.LessonTypeFilter.HasValue && input.LessonTypeFilter > -1, e => e.LessonType == lessonTypeFilter)
                        .WhereIf(input.ClassificationLessonFilter.HasValue && input.ClassificationLessonFilter > -1, e => e.ClassificationLesson == classificationLessonFilter)
                        .WhereIf(input.MinNumberOfUnitsFilter != null, e => e.NumberOfUnits >= input.MinNumberOfUnitsFilter)
                        .WhereIf(input.MaxNumberOfUnitsFilter != null, e => e.NumberOfUnits <= input.MaxNumberOfUnitsFilter)
                        .WhereIf(input.MinNumberOfGroupsFilter != null, e => e.NumberOfGroups >= input.MinNumberOfGroupsFilter)
                        .WhereIf(input.MaxNumberOfGroupsFilter != null, e => e.NumberOfGroups <= input.MaxNumberOfGroupsFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassroomBuildingClassroomBuildingNameFilter), e => e.ClassroomBuildingFk != null && e.ClassroomBuildingFk.ClassroomBuildingName == input.ClassroomBuildingClassroomBuildingNameFilter);

            var pagedAndFilteredLessons = filteredLessons
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var lessons = from o in pagedAndFilteredLessons
                          join o1 in _lookup_classroomBuildingRepository.GetAll() on o.ClassroomBuildingId equals o1.Id into j1
                          from s1 in j1.DefaultIfEmpty()

                          select new
                          {

                              o.NameLesson,
                              o.HoursPerWeek,
                              o.LessonType,
                              o.ClassificationLesson,
                              o.NumberOfUnits,
                              o.NumberOfGroups,
                              o.IsActive,
                              Id = o.Id,
                              ClassroomBuildingClassroomBuildingName = s1 == null || s1.ClassroomBuildingName == null ? "" : s1.ClassroomBuildingName.ToString()
                          };

            var totalCount = await filteredLessons.CountAsync();

            var dbList = await lessons.ToListAsync();
            var results = new List<GetLessonForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetLessonForViewDto()
                {
                    Lesson = new LessonDto
                    {

                        NameLesson = o.NameLesson,
                        HoursPerWeek = o.HoursPerWeek,
                        LessonType = o.LessonType,
                        ClassificationLesson = o.ClassificationLesson,
                        NumberOfUnits = o.NumberOfUnits,
                        NumberOfGroups = o.NumberOfGroups,
                        IsActive = o.IsActive,
                        Id = o.Id,
                    },
                    ClassroomBuildingClassroomBuildingName = o.ClassroomBuildingClassroomBuildingName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetLessonForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetLessonForViewDto> GetLessonForView(long id)
        {
            var lesson = await _lessonRepository.GetAsync(id);

            var output = new GetLessonForViewDto { Lesson = ObjectMapper.Map<LessonDto>(lesson) };

            if (output.Lesson.ClassroomBuildingId != null)
            {
                var _lookupClassroomBuilding = await _lookup_classroomBuildingRepository.FirstOrDefaultAsync((int)output.Lesson.ClassroomBuildingId);
                output.ClassroomBuildingClassroomBuildingName = _lookupClassroomBuilding?.ClassroomBuildingName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Lessons_Edit)]
        public async Task<GetLessonForEditOutput> GetLessonForEdit(EntityDto<long> input)
        {
            var lesson = await _lessonRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLessonForEditOutput { Lesson = ObjectMapper.Map<CreateOrEditLessonDto>(lesson) };

            if (output.Lesson.ClassroomBuildingId != null)
            {
                var _lookupClassroomBuilding = await _lookup_classroomBuildingRepository.FirstOrDefaultAsync((int)output.Lesson.ClassroomBuildingId);
                output.ClassroomBuildingClassroomBuildingName = _lookupClassroomBuilding?.ClassroomBuildingName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLessonDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Lessons_Create)]
        protected virtual async Task Create(CreateOrEditLessonDto input)
        {
            var lesson = ObjectMapper.Map<Lesson>(input);

            if (AbpSession.TenantId != null)
            {
                lesson.TenantId = (int)AbpSession.TenantId;
            }

            await _lessonRepository.InsertAsync(lesson);

        }

        [AbpAuthorize(AppPermissions.Pages_Lessons_Edit)]
        protected virtual async Task Update(CreateOrEditLessonDto input)
        {
            var lesson = await _lessonRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, lesson);

        }

        [AbpAuthorize(AppPermissions.Pages_Lessons_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _lessonRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetLessonsToExcel(GetAllLessonsForExcelInput input)
        {
            var lessonTypeFilter = input.LessonTypeFilter.HasValue
                        ? (LessonTypeEnum)input.LessonTypeFilter
                        : default;
            var classificationLessonFilter = input.ClassificationLessonFilter.HasValue
                ? (ClassificationLessonEnum)input.ClassificationLessonFilter
                : default;

            var filteredLessons = _lessonRepository.GetAll()
                        .Include(e => e.ClassroomBuildingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameLesson.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameLessonFilter), e => e.NameLesson == input.NameLessonFilter)
                        .WhereIf(input.MinHoursPerWeekFilter != null, e => e.HoursPerWeek >= input.MinHoursPerWeekFilter)
                        .WhereIf(input.MaxHoursPerWeekFilter != null, e => e.HoursPerWeek <= input.MaxHoursPerWeekFilter)
                        .WhereIf(input.LessonTypeFilter.HasValue && input.LessonTypeFilter > -1, e => e.LessonType == lessonTypeFilter)
                        .WhereIf(input.ClassificationLessonFilter.HasValue && input.ClassificationLessonFilter > -1, e => e.ClassificationLesson == classificationLessonFilter)
                        .WhereIf(input.MinNumberOfUnitsFilter != null, e => e.NumberOfUnits >= input.MinNumberOfUnitsFilter)
                        .WhereIf(input.MaxNumberOfUnitsFilter != null, e => e.NumberOfUnits <= input.MaxNumberOfUnitsFilter)
                        .WhereIf(input.MinNumberOfGroupsFilter != null, e => e.NumberOfGroups >= input.MinNumberOfGroupsFilter)
                        .WhereIf(input.MaxNumberOfGroupsFilter != null, e => e.NumberOfGroups <= input.MaxNumberOfGroupsFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassroomBuildingClassroomBuildingNameFilter), e => e.ClassroomBuildingFk != null && e.ClassroomBuildingFk.ClassroomBuildingName == input.ClassroomBuildingClassroomBuildingNameFilter);

            var query = (from o in filteredLessons
                         join o1 in _lookup_classroomBuildingRepository.GetAll() on o.ClassroomBuildingId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetLessonForViewDto()
                         {
                             Lesson = new LessonDto
                             {
                                 NameLesson = o.NameLesson,
                                 HoursPerWeek = o.HoursPerWeek,
                                 LessonType = o.LessonType,
                                 ClassificationLesson = o.ClassificationLesson,
                                 NumberOfUnits = o.NumberOfUnits,
                                 NumberOfGroups = o.NumberOfGroups,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             },
                             ClassroomBuildingClassroomBuildingName = s1 == null || s1.ClassroomBuildingName == null ? "" : s1.ClassroomBuildingName.ToString()
                         });

            var lessonListDtos = await query.ToListAsync();

            return _lessonsExcelExporter.ExportToFile(lessonListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Lessons)]
        public async Task<PagedResultDto<LessonClassroomBuildingLookupTableDto>> GetAllClassroomBuildingForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_classroomBuildingRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.ClassroomBuildingName != null && e.ClassroomBuildingName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var classroomBuildingList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<LessonClassroomBuildingLookupTableDto>();
            foreach (var classroomBuilding in classroomBuildingList)
            {
                lookupTableDtoList.Add(new LessonClassroomBuildingLookupTableDto
                {
                    Id = classroomBuilding.Id,
                    DisplayName = classroomBuilding.ClassroomBuildingName?.ToString()
                });
            }

            return new PagedResultDto<LessonClassroomBuildingLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}