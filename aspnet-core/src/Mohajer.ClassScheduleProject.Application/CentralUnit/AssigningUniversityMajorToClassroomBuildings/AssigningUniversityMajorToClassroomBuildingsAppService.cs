using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings
{
    [AbpAuthorize(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings)]
    public class AssigningUniversityMajorToClassroomBuildingsAppService : ClassScheduleProjectAppServiceBase, IAssigningUniversityMajorToClassroomBuildingsAppService
    {
        private readonly IRepository<AssigningUniversityMajorToClassroomBuilding, long> _assigningUniversityMajorToClassroomBuildingRepository;
        private readonly IAssigningUniversityMajorToClassroomBuildingsExcelExporter _assigningUniversityMajorToClassroomBuildingsExcelExporter;
        private readonly IRepository<UniversityMajor, int> _lookup_universityMajorRepository;
        private readonly IRepository<ClassroomBuilding, int> _lookup_classroomBuildingRepository;

        public AssigningUniversityMajorToClassroomBuildingsAppService(IRepository<AssigningUniversityMajorToClassroomBuilding, long> assigningUniversityMajorToClassroomBuildingRepository, IAssigningUniversityMajorToClassroomBuildingsExcelExporter assigningUniversityMajorToClassroomBuildingsExcelExporter, IRepository<UniversityMajor, int> lookup_universityMajorRepository, IRepository<ClassroomBuilding, int> lookup_classroomBuildingRepository)
        {
            _assigningUniversityMajorToClassroomBuildingRepository = assigningUniversityMajorToClassroomBuildingRepository;
            _assigningUniversityMajorToClassroomBuildingsExcelExporter = assigningUniversityMajorToClassroomBuildingsExcelExporter;
            _lookup_universityMajorRepository = lookup_universityMajorRepository;
            _lookup_classroomBuildingRepository = lookup_classroomBuildingRepository;

        }

        public async Task<PagedResultDto<GetAssigningUniversityMajorToClassroomBuildingForViewDto>> GetAll(GetAllAssigningUniversityMajorToClassroomBuildingsInput input)
        {

            var filteredAssigningUniversityMajorToClassroomBuildings = _assigningUniversityMajorToClassroomBuildingRepository.GetAll()
                        .Include(e => e.UniversityMajorFk)
                        .Include(e => e.ClassroomBuildingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter != null, e => e.MaximumRestrictionsOnUsingClassroomsAtTheSameTime >= input.MinMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter)
                        .WhereIf(input.MaxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter != null, e => e.MaximumRestrictionsOnUsingClassroomsAtTheSameTime <= input.MaxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityMajorUniversityMajorNameFilter), e => e.UniversityMajorFk != null && e.UniversityMajorFk.UniversityMajorName == input.UniversityMajorUniversityMajorNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassroomBuildingClassroomBuildingNameFilter), e => e.ClassroomBuildingFk != null && e.ClassroomBuildingFk.ClassroomBuildingName == input.ClassroomBuildingClassroomBuildingNameFilter);

            var pagedAndFilteredAssigningUniversityMajorToClassroomBuildings = filteredAssigningUniversityMajorToClassroomBuildings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var assigningUniversityMajorToClassroomBuildings = from o in pagedAndFilteredAssigningUniversityMajorToClassroomBuildings
                                                               join o1 in _lookup_universityMajorRepository.GetAll() on o.UniversityMajorId equals o1.Id into j1
                                                               from s1 in j1.DefaultIfEmpty()

                                                               join o2 in _lookup_classroomBuildingRepository.GetAll() on o.ClassroomBuildingId equals o2.Id into j2
                                                               from s2 in j2.DefaultIfEmpty()

                                                               select new
                                                               {

                                                                   o.MaximumRestrictionsOnUsingClassroomsAtTheSameTime,
                                                                   Id = o.Id,
                                                                   UniversityMajorUniversityMajorName = s1 == null || s1.UniversityMajorName == null ? "" : s1.UniversityMajorName.ToString(),
                                                                   ClassroomBuildingClassroomBuildingName = s2 == null || s2.ClassroomBuildingName == null ? "" : s2.ClassroomBuildingName.ToString()
                                                               };

            var totalCount = await filteredAssigningUniversityMajorToClassroomBuildings.CountAsync();

            var dbList = await assigningUniversityMajorToClassroomBuildings.ToListAsync();
            var results = new List<GetAssigningUniversityMajorToClassroomBuildingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAssigningUniversityMajorToClassroomBuildingForViewDto()
                {
                    AssigningUniversityMajorToClassroomBuilding = new AssigningUniversityMajorToClassroomBuildingDto
                    {

                        MaximumRestrictionsOnUsingClassroomsAtTheSameTime = o.MaximumRestrictionsOnUsingClassroomsAtTheSameTime,
                        Id = o.Id,
                    },
                    UniversityMajorUniversityMajorName = o.UniversityMajorUniversityMajorName,
                    ClassroomBuildingClassroomBuildingName = o.ClassroomBuildingClassroomBuildingName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAssigningUniversityMajorToClassroomBuildingForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetAssigningUniversityMajorToClassroomBuildingForViewDto> GetAssigningUniversityMajorToClassroomBuildingForView(long id)
        {
            var assigningUniversityMajorToClassroomBuilding = await _assigningUniversityMajorToClassroomBuildingRepository.GetAsync(id);

            var output = new GetAssigningUniversityMajorToClassroomBuildingForViewDto { AssigningUniversityMajorToClassroomBuilding = ObjectMapper.Map<AssigningUniversityMajorToClassroomBuildingDto>(assigningUniversityMajorToClassroomBuilding) };

            if (output.AssigningUniversityMajorToClassroomBuilding.UniversityMajorId != null)
            {
                var _lookupUniversityMajor = await _lookup_universityMajorRepository.FirstOrDefaultAsync((int)output.AssigningUniversityMajorToClassroomBuilding.UniversityMajorId);
                output.UniversityMajorUniversityMajorName = _lookupUniversityMajor?.UniversityMajorName?.ToString();
            }

            if (output.AssigningUniversityMajorToClassroomBuilding.ClassroomBuildingId != null)
            {
                var _lookupClassroomBuilding = await _lookup_classroomBuildingRepository.FirstOrDefaultAsync((int)output.AssigningUniversityMajorToClassroomBuilding.ClassroomBuildingId);
                output.ClassroomBuildingClassroomBuildingName = _lookupClassroomBuilding?.ClassroomBuildingName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings_Edit)]
        public async Task<GetAssigningUniversityMajorToClassroomBuildingForEditOutput> GetAssigningUniversityMajorToClassroomBuildingForEdit(EntityDto<long> input)
        {
            var assigningUniversityMajorToClassroomBuilding = await _assigningUniversityMajorToClassroomBuildingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAssigningUniversityMajorToClassroomBuildingForEditOutput { AssigningUniversityMajorToClassroomBuilding = ObjectMapper.Map<CreateOrEditAssigningUniversityMajorToClassroomBuildingDto>(assigningUniversityMajorToClassroomBuilding) };

            if (output.AssigningUniversityMajorToClassroomBuilding.UniversityMajorId != null)
            {
                var _lookupUniversityMajor = await _lookup_universityMajorRepository.FirstOrDefaultAsync((int)output.AssigningUniversityMajorToClassroomBuilding.UniversityMajorId);
                output.UniversityMajorUniversityMajorName = _lookupUniversityMajor?.UniversityMajorName?.ToString();
            }

            if (output.AssigningUniversityMajorToClassroomBuilding.ClassroomBuildingId != null)
            {
                var _lookupClassroomBuilding = await _lookup_classroomBuildingRepository.FirstOrDefaultAsync((int)output.AssigningUniversityMajorToClassroomBuilding.ClassroomBuildingId);
                output.ClassroomBuildingClassroomBuildingName = _lookupClassroomBuilding?.ClassroomBuildingName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAssigningUniversityMajorToClassroomBuildingDto input)
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

        [AbpAuthorize(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings_Create)]
        protected virtual async Task Create(CreateOrEditAssigningUniversityMajorToClassroomBuildingDto input)
        {
            var esistAssigningUniversityMajorToClassroomBuilding = await _assigningUniversityMajorToClassroomBuildingRepository
                .FirstOrDefaultAsync(record => record.ClassroomBuildingId == input.ClassroomBuildingId && record.UniversityMajorId == input.UniversityMajorId);
            if (esistAssigningUniversityMajorToClassroomBuilding != null)
            {
                throw new UserFriendlyException(L("AssinedBeforAssigningUniversityMajorToClassroomBuildingSelected"));
            }
            var assigningUniversityMajorToClassroomBuilding = ObjectMapper.Map<AssigningUniversityMajorToClassroomBuilding>(input);

            if (AbpSession.TenantId != null)
            {
                assigningUniversityMajorToClassroomBuilding.TenantId = (int)AbpSession.TenantId;
            }

            await _assigningUniversityMajorToClassroomBuildingRepository.InsertAsync(assigningUniversityMajorToClassroomBuilding);

        }

        [AbpAuthorize(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings_Edit)]
        protected virtual async Task Update(CreateOrEditAssigningUniversityMajorToClassroomBuildingDto input)
        {
            var assigningUniversityMajorToClassroomBuilding = await _assigningUniversityMajorToClassroomBuildingRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, assigningUniversityMajorToClassroomBuilding);

        }

        [AbpAuthorize(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _assigningUniversityMajorToClassroomBuildingRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetAssigningUniversityMajorToClassroomBuildingsToExcel(GetAllAssigningUniversityMajorToClassroomBuildingsForExcelInput input)
        {

            var filteredAssigningUniversityMajorToClassroomBuildings = _assigningUniversityMajorToClassroomBuildingRepository.GetAll()
                        .Include(e => e.UniversityMajorFk)
                        .Include(e => e.ClassroomBuildingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter != null, e => e.MaximumRestrictionsOnUsingClassroomsAtTheSameTime >= input.MinMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter)
                        .WhereIf(input.MaxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter != null, e => e.MaximumRestrictionsOnUsingClassroomsAtTheSameTime <= input.MaxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityMajorUniversityMajorNameFilter), e => e.UniversityMajorFk != null && e.UniversityMajorFk.UniversityMajorName == input.UniversityMajorUniversityMajorNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassroomBuildingClassroomBuildingNameFilter), e => e.ClassroomBuildingFk != null && e.ClassroomBuildingFk.ClassroomBuildingName == input.ClassroomBuildingClassroomBuildingNameFilter);

            var query = (from o in filteredAssigningUniversityMajorToClassroomBuildings
                         join o1 in _lookup_universityMajorRepository.GetAll() on o.UniversityMajorId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_classroomBuildingRepository.GetAll() on o.ClassroomBuildingId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetAssigningUniversityMajorToClassroomBuildingForViewDto()
                         {
                             AssigningUniversityMajorToClassroomBuilding = new AssigningUniversityMajorToClassroomBuildingDto
                             {
                                 MaximumRestrictionsOnUsingClassroomsAtTheSameTime = o.MaximumRestrictionsOnUsingClassroomsAtTheSameTime,
                                 Id = o.Id
                             },
                             UniversityMajorUniversityMajorName = s1 == null || s1.UniversityMajorName == null ? "" : s1.UniversityMajorName.ToString(),
                             ClassroomBuildingClassroomBuildingName = s2 == null || s2.ClassroomBuildingName == null ? "" : s2.ClassroomBuildingName.ToString()
                         });

            var assigningUniversityMajorToClassroomBuildingListDtos = await query.ToListAsync();

            return _assigningUniversityMajorToClassroomBuildingsExcelExporter.ExportToFile(assigningUniversityMajorToClassroomBuildingListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings)]
        public async Task<PagedResultDto<AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableDto>> GetAllUniversityMajorForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_universityMajorRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.UniversityMajorName != null && e.UniversityMajorName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var universityMajorList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableDto>();
            foreach (var universityMajor in universityMajorList)
            {
                lookupTableDtoList.Add(new AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableDto
                {
                    Id = universityMajor.Id,
                    DisplayName = universityMajor.UniversityMajorName?.ToString()
                });
            }

            return new PagedResultDto<AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings)]
        public async Task<PagedResultDto<AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableDto>> GetAllClassroomBuildingForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_classroomBuildingRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.ClassroomBuildingName != null && e.ClassroomBuildingName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var classroomBuildingList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableDto>();
            foreach (var classroomBuilding in classroomBuildingList)
            {
                lookupTableDtoList.Add(new AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableDto
                {
                    Id = classroomBuilding.Id,
                    DisplayName = classroomBuilding.ClassroomBuildingName?.ToString()
                });
            }

            return new PagedResultDto<AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}