using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors
{
    [AbpAuthorize(AppPermissions.Pages_LessonsOfUniversityProfessors)]
    public class LessonsOfUniversityProfessorsAppService : ClassScheduleProjectAppServiceBase, ILessonsOfUniversityProfessorsAppService
    {
        private readonly IRepository<LessonsOfUniversityProfessor, long> _lessonsOfUniversityProfessorRepository;
        private readonly ILessonsOfUniversityProfessorsExcelExporter _lessonsOfUniversityProfessorsExcelExporter;
        private readonly IRepository<Lesson, long> _lookup_lessonRepository;
        private readonly IRepository<UniversityProfessor, int> _lookup_universityProfessorRepository;

        public LessonsOfUniversityProfessorsAppService(IRepository<LessonsOfUniversityProfessor, long> lessonsOfUniversityProfessorRepository, ILessonsOfUniversityProfessorsExcelExporter lessonsOfUniversityProfessorsExcelExporter, IRepository<Lesson, long> lookup_lessonRepository, IRepository<UniversityProfessor, int> lookup_universityProfessorRepository)
        {
            _lessonsOfUniversityProfessorRepository = lessonsOfUniversityProfessorRepository;
            _lessonsOfUniversityProfessorsExcelExporter = lessonsOfUniversityProfessorsExcelExporter;
            _lookup_lessonRepository = lookup_lessonRepository;
            _lookup_universityProfessorRepository = lookup_universityProfessorRepository;

        }

        public async Task<PagedResultDto<GetLessonsOfUniversityProfessorForViewDto>> GetAll(GetAllLessonsOfUniversityProfessorsInput input)
        {

            var filteredLessonsOfUniversityProfessors = _lessonsOfUniversityProfessorRepository.GetAll()
                        .Include(e => e.LessonFk)
                        .Include(e => e.UniversityProfessorFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonNameLessonFilter), e => e.LessonFk != null && e.LessonFk.NameLesson == input.LessonNameLessonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorUniversityProfessorNameFilter), e => e.UniversityProfessorFk != null && e.UniversityProfessorFk.UniversityProfessorName == input.UniversityProfessorUniversityProfessorNameFilter);

            var pagedAndFilteredLessonsOfUniversityProfessors = filteredLessonsOfUniversityProfessors
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var lessonsOfUniversityProfessors = from o in pagedAndFilteredLessonsOfUniversityProfessors
                                                join o1 in _lookup_lessonRepository.GetAll() on o.LessonId equals o1.Id into j1
                                                from s1 in j1.DefaultIfEmpty()

                                                join o2 in _lookup_universityProfessorRepository.GetAll() on o.UniversityProfessorId equals o2.Id into j2
                                                from s2 in j2.DefaultIfEmpty()

                                                select new
                                                {

                                                    o.IsActive,
                                                    Id = o.Id,
                                                    LessonNameLesson = s1 == null || s1.NameLesson == null ? "" : s1.NameLesson.ToString(),
                                                    UniversityProfessorUniversityProfessorName = s2 == null || s2.UniversityProfessorName == null ? "" : s2.UniversityProfessorName.ToString()
                                                };

            var totalCount = await filteredLessonsOfUniversityProfessors.CountAsync();

            var dbList = await lessonsOfUniversityProfessors.ToListAsync();
            var results = new List<GetLessonsOfUniversityProfessorForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetLessonsOfUniversityProfessorForViewDto()
                {
                    LessonsOfUniversityProfessor = new LessonsOfUniversityProfessorDto
                    {

                        IsActive = o.IsActive,
                        Id = o.Id,
                    },
                    LessonNameLesson = o.LessonNameLesson,
                    UniversityProfessorUniversityProfessorName = o.UniversityProfessorUniversityProfessorName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetLessonsOfUniversityProfessorForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetLessonsOfUniversityProfessorForViewDto> GetLessonsOfUniversityProfessorForView(long id)
        {
            var lessonsOfUniversityProfessor = await _lessonsOfUniversityProfessorRepository.GetAsync(id);

            var output = new GetLessonsOfUniversityProfessorForViewDto { LessonsOfUniversityProfessor = ObjectMapper.Map<LessonsOfUniversityProfessorDto>(lessonsOfUniversityProfessor) };

            if (output.LessonsOfUniversityProfessor.LessonId != null)
            {
                var _lookupLesson = await _lookup_lessonRepository.FirstOrDefaultAsync((long)output.LessonsOfUniversityProfessor.LessonId);
                output.LessonNameLesson = _lookupLesson?.NameLesson?.ToString();
            }

            if (output.LessonsOfUniversityProfessor.UniversityProfessorId != null)
            {
                var _lookupUniversityProfessor = await _lookup_universityProfessorRepository.FirstOrDefaultAsync((int)output.LessonsOfUniversityProfessor.UniversityProfessorId);
                output.UniversityProfessorUniversityProfessorName = _lookupUniversityProfessor?.UniversityProfessorName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfUniversityProfessors_Edit)]
        public async Task<GetLessonsOfUniversityProfessorForEditOutput> GetLessonsOfUniversityProfessorForEdit(EntityDto<long> input)
        {
            var lessonsOfUniversityProfessor = await _lessonsOfUniversityProfessorRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLessonsOfUniversityProfessorForEditOutput { LessonsOfUniversityProfessor = ObjectMapper.Map<CreateOrEditLessonsOfUniversityProfessorDto>(lessonsOfUniversityProfessor) };

            if (output.LessonsOfUniversityProfessor.LessonId != null)
            {
                var _lookupLesson = await _lookup_lessonRepository.FirstOrDefaultAsync((long)output.LessonsOfUniversityProfessor.LessonId);
                output.LessonNameLesson = _lookupLesson?.NameLesson?.ToString();
            }

            if (output.LessonsOfUniversityProfessor.UniversityProfessorId != null)
            {
                var _lookupUniversityProfessor = await _lookup_universityProfessorRepository.FirstOrDefaultAsync((int)output.LessonsOfUniversityProfessor.UniversityProfessorId);
                output.UniversityProfessorUniversityProfessorName = _lookupUniversityProfessor?.UniversityProfessorName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLessonsOfUniversityProfessorDto input)
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

        [AbpAuthorize(AppPermissions.Pages_LessonsOfUniversityProfessors_Create)]
        protected virtual async Task Create(CreateOrEditLessonsOfUniversityProfessorDto input)
        {
            var lessonsOfUniversityProfessor = ObjectMapper.Map<LessonsOfUniversityProfessor>(input);

            if (AbpSession.TenantId != null)
            {
                lessonsOfUniversityProfessor.TenantId = (int)AbpSession.TenantId;
            }

            await _lessonsOfUniversityProfessorRepository.InsertAsync(lessonsOfUniversityProfessor);

        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfUniversityProfessors_Edit)]
        protected virtual async Task Update(CreateOrEditLessonsOfUniversityProfessorDto input)
        {
            var lessonsOfUniversityProfessor = await _lessonsOfUniversityProfessorRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, lessonsOfUniversityProfessor);

        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfUniversityProfessors_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _lessonsOfUniversityProfessorRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetLessonsOfUniversityProfessorsToExcel(GetAllLessonsOfUniversityProfessorsForExcelInput input)
        {

            var filteredLessonsOfUniversityProfessors = _lessonsOfUniversityProfessorRepository.GetAll()
                        .Include(e => e.LessonFk)
                        .Include(e => e.UniversityProfessorFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonNameLessonFilter), e => e.LessonFk != null && e.LessonFk.NameLesson == input.LessonNameLessonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorUniversityProfessorNameFilter), e => e.UniversityProfessorFk != null && e.UniversityProfessorFk.UniversityProfessorName == input.UniversityProfessorUniversityProfessorNameFilter);

            var query = (from o in filteredLessonsOfUniversityProfessors
                         join o1 in _lookup_lessonRepository.GetAll() on o.LessonId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_universityProfessorRepository.GetAll() on o.UniversityProfessorId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetLessonsOfUniversityProfessorForViewDto()
                         {
                             LessonsOfUniversityProfessor = new LessonsOfUniversityProfessorDto
                             {
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             },
                             LessonNameLesson = s1 == null || s1.NameLesson == null ? "" : s1.NameLesson.ToString(),
                             UniversityProfessorUniversityProfessorName = s2 == null || s2.UniversityProfessorName == null ? "" : s2.UniversityProfessorName.ToString()
                         });

            var lessonsOfUniversityProfessorListDtos = await query.ToListAsync();

            return _lessonsOfUniversityProfessorsExcelExporter.ExportToFile(lessonsOfUniversityProfessorListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfUniversityProfessors)]
        public async Task<PagedResultDto<LessonsOfUniversityProfessorLessonLookupTableDto>> GetAllLessonForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_lessonRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameLesson != null && e.NameLesson.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var lessonList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<LessonsOfUniversityProfessorLessonLookupTableDto>();
            foreach (var lesson in lessonList)
            {
                lookupTableDtoList.Add(new LessonsOfUniversityProfessorLessonLookupTableDto
                {
                    Id = lesson.Id,
                    DisplayName = lesson.NameLesson?.ToString()
                });
            }

            return new PagedResultDto<LessonsOfUniversityProfessorLessonLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_LessonsOfUniversityProfessors)]
        public async Task<PagedResultDto<LessonsOfUniversityProfessorUniversityProfessorLookupTableDto>> GetAllUniversityProfessorForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_universityProfessorRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.UniversityProfessorName != null && e.UniversityProfessorName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var universityProfessorList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<LessonsOfUniversityProfessorUniversityProfessorLookupTableDto>();
            foreach (var universityProfessor in universityProfessorList)
            {
                lookupTableDtoList.Add(new LessonsOfUniversityProfessorUniversityProfessorLookupTableDto
                {
                    Id = universityProfessor.Id,
                    DisplayName = universityProfessor.UniversityProfessorName?.ToString()
                });
            }

            return new PagedResultDto<LessonsOfUniversityProfessorUniversityProfessorLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}