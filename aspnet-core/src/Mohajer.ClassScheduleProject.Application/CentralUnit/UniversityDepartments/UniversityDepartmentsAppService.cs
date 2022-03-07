using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments
{
    [AbpAuthorize(AppPermissions.Pages_UniversityDepartments)]
    public class UniversityDepartmentsAppService : ClassScheduleProjectAppServiceBase, IUniversityDepartmentsAppService
    {
        private readonly IRepository<UniversityDepartment> _universityDepartmentRepository;
        private readonly IUniversityDepartmentsExcelExporter _universityDepartmentsExcelExporter;

        public UniversityDepartmentsAppService(IRepository<UniversityDepartment> universityDepartmentRepository, IUniversityDepartmentsExcelExporter universityDepartmentsExcelExporter)
        {
            _universityDepartmentRepository = universityDepartmentRepository;
            _universityDepartmentsExcelExporter = universityDepartmentsExcelExporter;

        }

        public async Task<PagedResultDto<GetUniversityDepartmentForViewDto>> GetAll(GetAllUniversityDepartmentsInput input)
        {

            var filteredUniversityDepartments = _universityDepartmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UniversityDepartmentName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityDepartmentNameFilter), e => e.UniversityDepartmentName == input.UniversityDepartmentNameFilter);

            var pagedAndFilteredUniversityDepartments = filteredUniversityDepartments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var universityDepartments = from o in pagedAndFilteredUniversityDepartments
                                        select new
                                        {

                                            o.UniversityDepartmentName,
                                            Id = o.Id
                                        };

            var totalCount = await filteredUniversityDepartments.CountAsync();

            var dbList = await universityDepartments.ToListAsync();
            var results = new List<GetUniversityDepartmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetUniversityDepartmentForViewDto()
                {
                    UniversityDepartment = new UniversityDepartmentDto
                    {

                        UniversityDepartmentName = o.UniversityDepartmentName,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetUniversityDepartmentForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetUniversityDepartmentForViewDto> GetUniversityDepartmentForView(int id)
        {
            var universityDepartment = await _universityDepartmentRepository.GetAsync(id);

            var output = new GetUniversityDepartmentForViewDto { UniversityDepartment = ObjectMapper.Map<UniversityDepartmentDto>(universityDepartment) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_UniversityDepartments_Edit)]
        public async Task<GetUniversityDepartmentForEditOutput> GetUniversityDepartmentForEdit(EntityDto input)
        {
            var universityDepartment = await _universityDepartmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetUniversityDepartmentForEditOutput { UniversityDepartment = ObjectMapper.Map<CreateOrEditUniversityDepartmentDto>(universityDepartment) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditUniversityDepartmentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_UniversityDepartments_Create)]
        protected virtual async Task Create(CreateOrEditUniversityDepartmentDto input)
        {
            var universityDepartment = ObjectMapper.Map<UniversityDepartment>(input);

            if (AbpSession.TenantId != null)
            {
                universityDepartment.TenantId = (int)AbpSession.TenantId;
            }

            await _universityDepartmentRepository.InsertAsync(universityDepartment);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityDepartments_Edit)]
        protected virtual async Task Update(CreateOrEditUniversityDepartmentDto input)
        {
            var universityDepartment = await _universityDepartmentRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, universityDepartment);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityDepartments_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _universityDepartmentRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetUniversityDepartmentsToExcel(GetAllUniversityDepartmentsForExcelInput input)
        {

            var filteredUniversityDepartments = _universityDepartmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UniversityDepartmentName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityDepartmentNameFilter), e => e.UniversityDepartmentName == input.UniversityDepartmentNameFilter);

            var query = (from o in filteredUniversityDepartments
                         select new GetUniversityDepartmentForViewDto()
                         {
                             UniversityDepartment = new UniversityDepartmentDto
                             {
                                 UniversityDepartmentName = o.UniversityDepartmentName,
                                 Id = o.Id
                             }
                         });

            var universityDepartmentListDtos = await query.ToListAsync();

            return _universityDepartmentsExcelExporter.ExportToFile(universityDepartmentListDtos);
        }

    }
}