using Mohajer.ClassScheduleProject.CentralUnit.Grades;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors
{
    [AbpAuthorize(AppPermissions.Pages_AssigningGradeToUniversityMajors)]
    public class AssigningGradeToUniversityMajorsAppService : ClassScheduleProjectAppServiceBase, IAssigningGradeToUniversityMajorsAppService
    {
        private readonly IRepository<AssigningGradeToUniversityMajor, long> _assigningGradeToUniversityMajorRepository;
        private readonly IAssigningGradeToUniversityMajorsExcelExporter _assigningGradeToUniversityMajorsExcelExporter;
        private readonly IRepository<Grade, int> _lookup_gradeRepository;
        private readonly IRepository<UniversityMajor, int> _lookup_universityMajorRepository;

        public AssigningGradeToUniversityMajorsAppService(IRepository<AssigningGradeToUniversityMajor, long> assigningGradeToUniversityMajorRepository, IAssigningGradeToUniversityMajorsExcelExporter assigningGradeToUniversityMajorsExcelExporter, IRepository<Grade, int> lookup_gradeRepository, IRepository<UniversityMajor, int> lookup_universityMajorRepository)
        {
            _assigningGradeToUniversityMajorRepository = assigningGradeToUniversityMajorRepository;
            _assigningGradeToUniversityMajorsExcelExporter = assigningGradeToUniversityMajorsExcelExporter;
            _lookup_gradeRepository = lookup_gradeRepository;
            _lookup_universityMajorRepository = lookup_universityMajorRepository;

        }

        public async Task<PagedResultDto<GetAssigningGradeToUniversityMajorForViewDto>> GetAll(GetAllAssigningGradeToUniversityMajorsInput input)
        {

            var filteredAssigningGradeToUniversityMajors = _assigningGradeToUniversityMajorRepository.GetAll()
                        .Include(e => e.GradeFk)
                        .Include(e => e.UniversityMajorFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameAssignedGradeToUniversityMajor.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameAssignedGradeToUniversityMajorFilter), e => e.NameAssignedGradeToUniversityMajor == input.NameAssignedGradeToUniversityMajorFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GradeGradeNameFilter), e => e.GradeFk != null && e.GradeFk.GradeName == input.GradeGradeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityMajorUniversityMajorNameFilter), e => e.UniversityMajorFk != null && e.UniversityMajorFk.UniversityMajorName == input.UniversityMajorUniversityMajorNameFilter);

            var pagedAndFilteredAssigningGradeToUniversityMajors = filteredAssigningGradeToUniversityMajors
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var assigningGradeToUniversityMajors = from o in pagedAndFilteredAssigningGradeToUniversityMajors
                                                   join o1 in _lookup_gradeRepository.GetAll() on o.GradeId equals o1.Id into j1
                                                   from s1 in j1.DefaultIfEmpty()

                                                   join o2 in _lookup_universityMajorRepository.GetAll() on o.UniversityMajorId equals o2.Id into j2
                                                   from s2 in j2.DefaultIfEmpty()

                                                   select new
                                                   {

                                                       o.NameAssignedGradeToUniversityMajor,
                                                       Id = o.Id,
                                                       GradeGradeName = s1 == null || s1.GradeName == null ? "" : s1.GradeName.ToString(),
                                                       UniversityMajorUniversityMajorName = s2 == null || s2.UniversityMajorName == null ? "" : s2.UniversityMajorName.ToString()
                                                   };

            var totalCount = await filteredAssigningGradeToUniversityMajors.CountAsync();

            var dbList = await assigningGradeToUniversityMajors.ToListAsync();
            var results = new List<GetAssigningGradeToUniversityMajorForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAssigningGradeToUniversityMajorForViewDto()
                {
                    AssigningGradeToUniversityMajor = new AssigningGradeToUniversityMajorDto
                    {

                        NameAssignedGradeToUniversityMajor = o.NameAssignedGradeToUniversityMajor,
                        Id = o.Id,
                    },
                    GradeGradeName = o.GradeGradeName,
                    UniversityMajorUniversityMajorName = o.UniversityMajorUniversityMajorName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAssigningGradeToUniversityMajorForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetAssigningGradeToUniversityMajorForViewDto> GetAssigningGradeToUniversityMajorForView(long id)
        {
            var assigningGradeToUniversityMajor = await _assigningGradeToUniversityMajorRepository.GetAsync(id);

            var output = new GetAssigningGradeToUniversityMajorForViewDto { AssigningGradeToUniversityMajor = ObjectMapper.Map<AssigningGradeToUniversityMajorDto>(assigningGradeToUniversityMajor) };

            if (output.AssigningGradeToUniversityMajor.GradeId != null)
            {
                var _lookupGrade = await _lookup_gradeRepository.FirstOrDefaultAsync((int)output.AssigningGradeToUniversityMajor.GradeId);
                output.GradeGradeName = _lookupGrade?.GradeName?.ToString();
            }

            if (output.AssigningGradeToUniversityMajor.UniversityMajorId != null)
            {
                var _lookupUniversityMajor = await _lookup_universityMajorRepository.FirstOrDefaultAsync((int)output.AssigningGradeToUniversityMajor.UniversityMajorId);
                output.UniversityMajorUniversityMajorName = _lookupUniversityMajor?.UniversityMajorName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_AssigningGradeToUniversityMajors_Edit)]
        public async Task<GetAssigningGradeToUniversityMajorForEditOutput> GetAssigningGradeToUniversityMajorForEdit(EntityDto<long> input)
        {
            var assigningGradeToUniversityMajor = await _assigningGradeToUniversityMajorRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAssigningGradeToUniversityMajorForEditOutput { AssigningGradeToUniversityMajor = ObjectMapper.Map<CreateOrEditAssigningGradeToUniversityMajorDto>(assigningGradeToUniversityMajor) };

            if (output.AssigningGradeToUniversityMajor.GradeId != null)
            {
                var _lookupGrade = await _lookup_gradeRepository.FirstOrDefaultAsync((int)output.AssigningGradeToUniversityMajor.GradeId);
                output.GradeGradeName = _lookupGrade?.GradeName?.ToString();
            }

            if (output.AssigningGradeToUniversityMajor.UniversityMajorId != null)
            {
                var _lookupUniversityMajor = await _lookup_universityMajorRepository.FirstOrDefaultAsync((int)output.AssigningGradeToUniversityMajor.UniversityMajorId);
                output.UniversityMajorUniversityMajorName = _lookupUniversityMajor?.UniversityMajorName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAssigningGradeToUniversityMajorDto input)
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

        [AbpAuthorize(AppPermissions.Pages_AssigningGradeToUniversityMajors_Create)]
        protected virtual async Task Create(CreateOrEditAssigningGradeToUniversityMajorDto input)
        {
            var esistAssigningGradeToUniversityMajor = await _assigningGradeToUniversityMajorRepository
               .FirstOrDefaultAsync(record => record.UniversityMajorId == input.UniversityMajorId && record.GradeId == input.GradeId);
            if (esistAssigningGradeToUniversityMajor != null)
            {
                throw new UserFriendlyException(L("AssinedBeforAssigningGradeToUniversityMajorSelected"));
            }
            var assigningGradeToUniversityMajor = ObjectMapper.Map<AssigningGradeToUniversityMajor>(input);

            if (AbpSession.TenantId != null)
            {
                assigningGradeToUniversityMajor.TenantId = (int)AbpSession.TenantId;
            }

            await _assigningGradeToUniversityMajorRepository.InsertAsync(assigningGradeToUniversityMajor);

        }

        [AbpAuthorize(AppPermissions.Pages_AssigningGradeToUniversityMajors_Edit)]
        protected virtual async Task Update(CreateOrEditAssigningGradeToUniversityMajorDto input)
        {
            var esistAssigningGradeToUniversityMajor = await _assigningGradeToUniversityMajorRepository
               .FirstOrDefaultAsync(record => record.Id != input.Id && record.UniversityMajorId == input.UniversityMajorId && record.GradeId == input.GradeId);
            if (esistAssigningGradeToUniversityMajor != null)
            {
                throw new UserFriendlyException(L("AssinedBeforAssigningGradeToUniversityMajorSelected"));
            }
            var assigningGradeToUniversityMajor = await _assigningGradeToUniversityMajorRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, assigningGradeToUniversityMajor);

        }

        [AbpAuthorize(AppPermissions.Pages_AssigningGradeToUniversityMajors_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _assigningGradeToUniversityMajorRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetAssigningGradeToUniversityMajorsToExcel(GetAllAssigningGradeToUniversityMajorsForExcelInput input)
        {

            var filteredAssigningGradeToUniversityMajors = _assigningGradeToUniversityMajorRepository.GetAll()
                        .Include(e => e.GradeFk)
                        .Include(e => e.UniversityMajorFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameAssignedGradeToUniversityMajor.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameAssignedGradeToUniversityMajorFilter), e => e.NameAssignedGradeToUniversityMajor == input.NameAssignedGradeToUniversityMajorFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GradeGradeNameFilter), e => e.GradeFk != null && e.GradeFk.GradeName == input.GradeGradeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityMajorUniversityMajorNameFilter), e => e.UniversityMajorFk != null && e.UniversityMajorFk.UniversityMajorName == input.UniversityMajorUniversityMajorNameFilter);

            var query = (from o in filteredAssigningGradeToUniversityMajors
                         join o1 in _lookup_gradeRepository.GetAll() on o.GradeId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_universityMajorRepository.GetAll() on o.UniversityMajorId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetAssigningGradeToUniversityMajorForViewDto()
                         {
                             AssigningGradeToUniversityMajor = new AssigningGradeToUniversityMajorDto
                             {
                                 NameAssignedGradeToUniversityMajor = o.NameAssignedGradeToUniversityMajor,
                                 Id = o.Id
                             },
                             GradeGradeName = s1 == null || s1.GradeName == null ? "" : s1.GradeName.ToString(),
                             UniversityMajorUniversityMajorName = s2 == null || s2.UniversityMajorName == null ? "" : s2.UniversityMajorName.ToString()
                         });

            var assigningGradeToUniversityMajorListDtos = await query.ToListAsync();

            return _assigningGradeToUniversityMajorsExcelExporter.ExportToFile(assigningGradeToUniversityMajorListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_AssigningGradeToUniversityMajors)]
        public async Task<PagedResultDto<AssigningGradeToUniversityMajorGradeLookupTableDto>> GetAllGradeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_gradeRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.GradeName != null && e.GradeName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var gradeList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<AssigningGradeToUniversityMajorGradeLookupTableDto>();
            foreach (var grade in gradeList)
            {
                lookupTableDtoList.Add(new AssigningGradeToUniversityMajorGradeLookupTableDto
                {
                    Id = grade.Id,
                    DisplayName = grade.GradeName?.ToString()
                });
            }

            return new PagedResultDto<AssigningGradeToUniversityMajorGradeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_AssigningGradeToUniversityMajors)]
        public async Task<PagedResultDto<AssigningGradeToUniversityMajorUniversityMajorLookupTableDto>> GetAllUniversityMajorForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_universityMajorRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.UniversityMajorName != null && e.UniversityMajorName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var universityMajorList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<AssigningGradeToUniversityMajorUniversityMajorLookupTableDto>();
            foreach (var universityMajor in universityMajorList)
            {
                lookupTableDtoList.Add(new AssigningGradeToUniversityMajorUniversityMajorLookupTableDto
                {
                    Id = universityMajor.Id,
                    DisplayName = universityMajor.UniversityMajorName?.ToString()
                });
            }

            return new PagedResultDto<AssigningGradeToUniversityMajorUniversityMajorLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}