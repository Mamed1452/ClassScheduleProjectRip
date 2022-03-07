using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments;

using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors
{
    [AbpAuthorize(AppPermissions.Pages_UniversityMajors)]
    public class UniversityMajorsAppService : ClassScheduleProjectAppServiceBase, IUniversityMajorsAppService
    {
        private readonly IRepository<UniversityMajor> _universityMajorRepository;
        private readonly IUniversityMajorsExcelExporter _universityMajorsExcelExporter;
        private readonly IRepository<UniversityDepartment, int> _lookup_universityDepartmentRepository;

        public UniversityMajorsAppService(IRepository<UniversityMajor> universityMajorRepository, IUniversityMajorsExcelExporter universityMajorsExcelExporter, IRepository<UniversityDepartment, int> lookup_universityDepartmentRepository)
        {
            _universityMajorRepository = universityMajorRepository;
            _universityMajorsExcelExporter = universityMajorsExcelExporter;
            _lookup_universityDepartmentRepository = lookup_universityDepartmentRepository;

        }

        public async Task<PagedResultDto<GetUniversityMajorForViewDto>> GetAll(GetAllUniversityMajorsInput input)
        {
            var universityMajorTypeFilter = input.UniversityMajorTypeFilter.HasValue
                        ? (UniversityMajorTypeEnum)input.UniversityMajorTypeFilter
                        : default(UniversityMajorTypeEnum);

            var filteredUniversityMajors = _universityMajorRepository.GetAll()
                        .Include(e => e.UniversityDepartmentFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UniversityMajorName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityMajorNameFilter), e => e.UniversityMajorName == input.UniversityMajorNameFilter)
                        .WhereIf(input.UniversityMajorTypeFilter.HasValue && input.UniversityMajorTypeFilter > -1, e => e.UniversityMajorType == universityMajorTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityDepartmentUniversityDepartmentNameFilter), e => e.UniversityDepartmentFk != null && e.UniversityDepartmentFk.UniversityDepartmentName == input.UniversityDepartmentUniversityDepartmentNameFilter);

            var pagedAndFilteredUniversityMajors = filteredUniversityMajors
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var universityMajors = from o in pagedAndFilteredUniversityMajors
                                   join o1 in _lookup_universityDepartmentRepository.GetAll() on o.UniversityDepartmentId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   select new
                                   {

                                       o.UniversityMajorName,
                                       o.UniversityMajorType,
                                       Id = o.Id,
                                       UniversityDepartmentUniversityDepartmentName = s1 == null || s1.UniversityDepartmentName == null ? "" : s1.UniversityDepartmentName.ToString()
                                   };

            var totalCount = await filteredUniversityMajors.CountAsync();

            var dbList = await universityMajors.ToListAsync();
            var results = new List<GetUniversityMajorForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetUniversityMajorForViewDto()
                {
                    UniversityMajor = new UniversityMajorDto
                    {

                        UniversityMajorName = o.UniversityMajorName,
                        UniversityMajorType = o.UniversityMajorType,
                        Id = o.Id,
                    },
                    UniversityDepartmentUniversityDepartmentName = o.UniversityDepartmentUniversityDepartmentName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetUniversityMajorForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetUniversityMajorForViewDto> GetUniversityMajorForView(int id)
        {
            var universityMajor = await _universityMajorRepository.GetAsync(id);

            var output = new GetUniversityMajorForViewDto { UniversityMajor = ObjectMapper.Map<UniversityMajorDto>(universityMajor) };

            if (output.UniversityMajor.UniversityDepartmentId != null)
            {
                var _lookupUniversityDepartment = await _lookup_universityDepartmentRepository.FirstOrDefaultAsync((int)output.UniversityMajor.UniversityDepartmentId);
                output.UniversityDepartmentUniversityDepartmentName = _lookupUniversityDepartment?.UniversityDepartmentName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_UniversityMajors_Edit)]
        public async Task<GetUniversityMajorForEditOutput> GetUniversityMajorForEdit(EntityDto input)
        {
            var universityMajor = await _universityMajorRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetUniversityMajorForEditOutput { UniversityMajor = ObjectMapper.Map<CreateOrEditUniversityMajorDto>(universityMajor) };

            if (output.UniversityMajor.UniversityDepartmentId != null)
            {
                var _lookupUniversityDepartment = await _lookup_universityDepartmentRepository.FirstOrDefaultAsync((int)output.UniversityMajor.UniversityDepartmentId);
                output.UniversityDepartmentUniversityDepartmentName = _lookupUniversityDepartment?.UniversityDepartmentName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditUniversityMajorDto input)
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

        [AbpAuthorize(AppPermissions.Pages_UniversityMajors_Create)]
        protected virtual async Task Create(CreateOrEditUniversityMajorDto input)
        {
            var universityMajor = ObjectMapper.Map<UniversityMajor>(input);

            if (AbpSession.TenantId != null)
            {
                universityMajor.TenantId = (int)AbpSession.TenantId;
            }

            await _universityMajorRepository.InsertAsync(universityMajor);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityMajors_Edit)]
        protected virtual async Task Update(CreateOrEditUniversityMajorDto input)
        {
            var universityMajor = await _universityMajorRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, universityMajor);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityMajors_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _universityMajorRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetUniversityMajorsToExcel(GetAllUniversityMajorsForExcelInput input)
        {
            var universityMajorTypeFilter = input.UniversityMajorTypeFilter.HasValue
                        ? (UniversityMajorTypeEnum)input.UniversityMajorTypeFilter
                        : default;

            var filteredUniversityMajors = _universityMajorRepository.GetAll()
                        .Include(e => e.UniversityDepartmentFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UniversityMajorName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityMajorNameFilter), e => e.UniversityMajorName == input.UniversityMajorNameFilter)
                        .WhereIf(input.UniversityMajorTypeFilter.HasValue && input.UniversityMajorTypeFilter > -1, e => e.UniversityMajorType == universityMajorTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityDepartmentUniversityDepartmentNameFilter), e => e.UniversityDepartmentFk != null && e.UniversityDepartmentFk.UniversityDepartmentName == input.UniversityDepartmentUniversityDepartmentNameFilter);

            var query = (from o in filteredUniversityMajors
                         join o1 in _lookup_universityDepartmentRepository.GetAll() on o.UniversityDepartmentId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetUniversityMajorForViewDto()
                         {
                             UniversityMajor = new UniversityMajorDto
                             {
                                 UniversityMajorName = o.UniversityMajorName,
                                 UniversityMajorType = o.UniversityMajorType,
                                 Id = o.Id
                             },
                             UniversityDepartmentUniversityDepartmentName = s1 == null || s1.UniversityDepartmentName == null ? "" : s1.UniversityDepartmentName.ToString()
                         });

            var universityMajorListDtos = await query.ToListAsync();

            return _universityMajorsExcelExporter.ExportToFile(universityMajorListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_UniversityMajors)]
        public async Task<PagedResultDto<UniversityMajorUniversityDepartmentLookupTableDto>> GetAllUniversityDepartmentForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_universityDepartmentRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.UniversityDepartmentName != null && e.UniversityDepartmentName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var universityDepartmentList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<UniversityMajorUniversityDepartmentLookupTableDto>();
            foreach (var universityDepartment in universityDepartmentList)
            {
                lookupTableDtoList.Add(new UniversityMajorUniversityDepartmentLookupTableDto
                {
                    Id = universityDepartment.Id,
                    DisplayName = universityDepartment.UniversityDepartmentName?.ToString()
                });
            }

            return new PagedResultDto<UniversityMajorUniversityDepartmentLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}