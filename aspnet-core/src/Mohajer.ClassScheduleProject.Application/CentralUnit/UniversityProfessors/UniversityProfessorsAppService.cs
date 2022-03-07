using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors
{
    [AbpAuthorize(AppPermissions.Pages_UniversityProfessors)]
    public class UniversityProfessorsAppService : ClassScheduleProjectAppServiceBase, IUniversityProfessorsAppService
    {
        private readonly IRepository<UniversityProfessor> _universityProfessorRepository;
        private readonly IUniversityProfessorsExcelExporter _universityProfessorsExcelExporter;

        public UniversityProfessorsAppService(IRepository<UniversityProfessor> universityProfessorRepository, IUniversityProfessorsExcelExporter universityProfessorsExcelExporter)
        {
            _universityProfessorRepository = universityProfessorRepository;
            _universityProfessorsExcelExporter = universityProfessorsExcelExporter;

        }

        public async Task<PagedResultDto<GetUniversityProfessorForViewDto>> GetAll(GetAllUniversityProfessorsInput input)
        {

            var filteredUniversityProfessors = _universityProfessorRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UniversityProfessorName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorNameFilter), e => e.UniversityProfessorName == input.UniversityProfessorNameFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive));

            var pagedAndFilteredUniversityProfessors = filteredUniversityProfessors
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var universityProfessors = from o in pagedAndFilteredUniversityProfessors
                                       select new
                                       {

                                           o.UniversityProfessorName,
                                           o.IsActive,
                                           Id = o.Id
                                       };

            var totalCount = await filteredUniversityProfessors.CountAsync();

            var dbList = await universityProfessors.ToListAsync();
            var results = new List<GetUniversityProfessorForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetUniversityProfessorForViewDto()
                {
                    UniversityProfessor = new UniversityProfessorDto
                    {

                        UniversityProfessorName = o.UniversityProfessorName,
                        IsActive = o.IsActive,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetUniversityProfessorForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetUniversityProfessorForViewDto> GetUniversityProfessorForView(int id)
        {
            var universityProfessor = await _universityProfessorRepository.GetAsync(id);

            var output = new GetUniversityProfessorForViewDto { UniversityProfessor = ObjectMapper.Map<UniversityProfessorDto>(universityProfessor) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessors_Edit)]
        public async Task<GetUniversityProfessorForEditOutput> GetUniversityProfessorForEdit(EntityDto input)
        {
            var universityProfessor = await _universityProfessorRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetUniversityProfessorForEditOutput { UniversityProfessor = ObjectMapper.Map<CreateOrEditUniversityProfessorDto>(universityProfessor) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditUniversityProfessorDto input)
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

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessors_Create)]
        protected virtual async Task Create(CreateOrEditUniversityProfessorDto input)
        {
            var universityProfessor = ObjectMapper.Map<UniversityProfessor>(input);

            if (AbpSession.TenantId != null)
            {
                universityProfessor.TenantId = (int)AbpSession.TenantId;
            }

            await _universityProfessorRepository.InsertAsync(universityProfessor);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessors_Edit)]
        protected virtual async Task Update(CreateOrEditUniversityProfessorDto input)
        {
            var universityProfessor = await _universityProfessorRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, universityProfessor);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessors_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _universityProfessorRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetUniversityProfessorsToExcel(GetAllUniversityProfessorsForExcelInput input)
        {

            var filteredUniversityProfessors = _universityProfessorRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UniversityProfessorName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorNameFilter), e => e.UniversityProfessorName == input.UniversityProfessorNameFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive));

            var query = (from o in filteredUniversityProfessors
                         select new GetUniversityProfessorForViewDto()
                         {
                             UniversityProfessor = new UniversityProfessorDto
                             {
                                 UniversityProfessorName = o.UniversityProfessorName,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             }
                         });

            var universityProfessorListDtos = await query.ToListAsync();

            return _universityProfessorsExcelExporter.ExportToFile(universityProfessorListDtos);
        }

    }
}