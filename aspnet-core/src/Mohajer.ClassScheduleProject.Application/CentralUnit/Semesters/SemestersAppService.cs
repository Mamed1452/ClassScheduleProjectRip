using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters
{
    [AbpAuthorize(AppPermissions.Pages_Semesters)]
    public class SemestersAppService : ClassScheduleProjectAppServiceBase, ISemestersAppService
    {
        private readonly IRepository<Semester, long> _semesterRepository;
        private readonly ISemestersExcelExporter _semestersExcelExporter;
        private readonly IRepository<AssigningGradeToUniversityMajor, long> _lookup_assigningGradeToUniversityMajorRepository;

        public SemestersAppService(IRepository<Semester, long> semesterRepository, ISemestersExcelExporter semestersExcelExporter, IRepository<AssigningGradeToUniversityMajor, long> lookup_assigningGradeToUniversityMajorRepository)
        {
            _semesterRepository = semesterRepository;
            _semestersExcelExporter = semestersExcelExporter;
            _lookup_assigningGradeToUniversityMajorRepository = lookup_assigningGradeToUniversityMajorRepository;

        }

        public async Task<PagedResultDto<GetSemesterForViewDto>> GetAll(GetAllSemestersInput input)
        {

            var filteredSemesters = _semesterRepository.GetAll()
                        .Include(e => e.AssigningGradeToUniversityMajorFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.SemesterName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SemesterNameFilter), e => e.SemesterName == input.SemesterNameFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajorFilter), e => e.AssigningGradeToUniversityMajorFk != null && e.AssigningGradeToUniversityMajorFk.NameAssignedGradeToUniversityMajor == input.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajorFilter)
                        .WhereIf(input.AssigningGradeToUniversityMajorIdFilter.HasValue, e => false || e.AssigningGradeToUniversityMajorId == input.AssigningGradeToUniversityMajorIdFilter.Value);

            var pagedAndFilteredSemesters = filteredSemesters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var semesters = from o in pagedAndFilteredSemesters
                            join o1 in _lookup_assigningGradeToUniversityMajorRepository.GetAll() on o.AssigningGradeToUniversityMajorId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new
                            {

                                o.SemesterName,
                                o.IsActive,
                                Id = o.Id,
                                AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = s1 == null || s1.NameAssignedGradeToUniversityMajor == null ? "" : s1.NameAssignedGradeToUniversityMajor.ToString()
                            };

            var totalCount = await filteredSemesters.CountAsync();

            var dbList = await semesters.ToListAsync();
            var results = new List<GetSemesterForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetSemesterForViewDto()
                {
                    Semester = new SemesterDto
                    {

                        SemesterName = o.SemesterName,
                        IsActive = o.IsActive,
                        Id = o.Id,
                    },
                    AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = o.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor
                };

                results.Add(res);
            }

            return new PagedResultDto<GetSemesterForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetSemesterForViewDto> GetSemesterForView(long id)
        {
            var semester = await _semesterRepository.GetAsync(id);

            var output = new GetSemesterForViewDto { Semester = ObjectMapper.Map<SemesterDto>(semester) };

            if (output.Semester.AssigningGradeToUniversityMajorId != null)
            {
                var _lookupAssigningGradeToUniversityMajor = await _lookup_assigningGradeToUniversityMajorRepository.FirstOrDefaultAsync((long)output.Semester.AssigningGradeToUniversityMajorId);
                output.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = _lookupAssigningGradeToUniversityMajor?.NameAssignedGradeToUniversityMajor?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Semesters_Edit)]
        public async Task<GetSemesterForEditOutput> GetSemesterForEdit(EntityDto<long> input)
        {
            var semester = await _semesterRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSemesterForEditOutput { Semester = ObjectMapper.Map<CreateOrEditSemesterDto>(semester) };

            if (output.Semester.AssigningGradeToUniversityMajorId != null)
            {
                var _lookupAssigningGradeToUniversityMajor = await _lookup_assigningGradeToUniversityMajorRepository.FirstOrDefaultAsync((long)output.Semester.AssigningGradeToUniversityMajorId);
                output.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = _lookupAssigningGradeToUniversityMajor?.NameAssignedGradeToUniversityMajor?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSemesterDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Semesters_Create)]
        protected virtual async Task Create(CreateOrEditSemesterDto input)
        {
            var semester = ObjectMapper.Map<Semester>(input);

            if (AbpSession.TenantId != null)
            {
                semester.TenantId = (int)AbpSession.TenantId;
            }

            await _semesterRepository.InsertAsync(semester);

        }

        [AbpAuthorize(AppPermissions.Pages_Semesters_Edit)]
        protected virtual async Task Update(CreateOrEditSemesterDto input)
        {
            var semester = await _semesterRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, semester);

        }

        [AbpAuthorize(AppPermissions.Pages_Semesters_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _semesterRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetSemestersToExcel(GetAllSemestersForExcelInput input)
        {

            var filteredSemesters = _semesterRepository.GetAll()
                        .Include(e => e.AssigningGradeToUniversityMajorFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.SemesterName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SemesterNameFilter), e => e.SemesterName == input.SemesterNameFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajorFilter), e => e.AssigningGradeToUniversityMajorFk != null && e.AssigningGradeToUniversityMajorFk.NameAssignedGradeToUniversityMajor == input.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajorFilter);

            var query = (from o in filteredSemesters
                         join o1 in _lookup_assigningGradeToUniversityMajorRepository.GetAll() on o.AssigningGradeToUniversityMajorId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetSemesterForViewDto()
                         {
                             Semester = new SemesterDto
                             {
                                 SemesterName = o.SemesterName,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             },
                             AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = s1 == null || s1.NameAssignedGradeToUniversityMajor == null ? "" : s1.NameAssignedGradeToUniversityMajor.ToString()
                         });

            var semesterListDtos = await query.ToListAsync();

            return _semestersExcelExporter.ExportToFile(semesterListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Semesters)]
        public async Task<PagedResultDto<SemesterAssigningGradeToUniversityMajorLookupTableDto>> GetAllAssigningGradeToUniversityMajorForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_assigningGradeToUniversityMajorRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameAssignedGradeToUniversityMajor != null && e.NameAssignedGradeToUniversityMajor.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var assigningGradeToUniversityMajorList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<SemesterAssigningGradeToUniversityMajorLookupTableDto>();
            foreach (var assigningGradeToUniversityMajor in assigningGradeToUniversityMajorList)
            {
                lookupTableDtoList.Add(new SemesterAssigningGradeToUniversityMajorLookupTableDto
                {
                    Id = assigningGradeToUniversityMajor.Id,
                    DisplayName = assigningGradeToUniversityMajor.NameAssignedGradeToUniversityMajor?.ToString()
                });
            }

            return new PagedResultDto<SemesterAssigningGradeToUniversityMajorLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}