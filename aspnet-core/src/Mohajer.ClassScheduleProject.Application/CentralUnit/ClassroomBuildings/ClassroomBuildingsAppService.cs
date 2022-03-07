using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings
{
    [AbpAuthorize(AppPermissions.Pages_ClassroomBuildings)]
    public class ClassroomBuildingsAppService : ClassScheduleProjectAppServiceBase, IClassroomBuildingsAppService
    {
        private readonly IRepository<ClassroomBuilding> _classroomBuildingRepository;
        private readonly IClassroomBuildingsExcelExporter _classroomBuildingsExcelExporter;

        public ClassroomBuildingsAppService(IRepository<ClassroomBuilding> classroomBuildingRepository, IClassroomBuildingsExcelExporter classroomBuildingsExcelExporter)
        {
            _classroomBuildingRepository = classroomBuildingRepository;
            _classroomBuildingsExcelExporter = classroomBuildingsExcelExporter;

        }

        public async Task<PagedResultDto<GetClassroomBuildingForViewDto>> GetAll(GetAllClassroomBuildingsInput input)
        {
            var classificationClassroomBuildingFilter = input.ClassificationClassroomBuildingFilter.HasValue
                        ? (ClassificationClassroomBuildingEnum)input.ClassificationClassroomBuildingFilter
                        : default;

            var filteredClassroomBuildings = _classroomBuildingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ClassroomBuildingName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassroomBuildingNameFilter), e => e.ClassroomBuildingName == input.ClassroomBuildingNameFilter)
                        .WhereIf(input.MinNumberOfClassroomsFilter != null, e => e.NumberOfClassrooms >= input.MinNumberOfClassroomsFilter)
                        .WhereIf(input.MaxNumberOfClassroomsFilter != null, e => e.NumberOfClassrooms <= input.MaxNumberOfClassroomsFilter)
                        .WhereIf(input.ClassificationClassroomBuildingFilter.HasValue && input.ClassificationClassroomBuildingFilter > -1, e => e.ClassificationClassroomBuilding == classificationClassroomBuildingFilter);

            var pagedAndFilteredClassroomBuildings = filteredClassroomBuildings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var classroomBuildings = from o in pagedAndFilteredClassroomBuildings
                                     select new
                                     {

                                         o.ClassroomBuildingName,
                                         o.NumberOfClassrooms,
                                         o.ClassificationClassroomBuilding,
                                         Id = o.Id
                                     };

            var totalCount = await filteredClassroomBuildings.CountAsync();

            var dbList = await classroomBuildings.ToListAsync();
            var results = new List<GetClassroomBuildingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClassroomBuildingForViewDto()
                {
                    ClassroomBuilding = new ClassroomBuildingDto
                    {

                        ClassroomBuildingName = o.ClassroomBuildingName,
                        NumberOfClassrooms = o.NumberOfClassrooms,
                        ClassificationClassroomBuilding = o.ClassificationClassroomBuilding,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClassroomBuildingForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClassroomBuildingForViewDto> GetClassroomBuildingForView(int id)
        {
            var classroomBuilding = await _classroomBuildingRepository.GetAsync(id);

            var output = new GetClassroomBuildingForViewDto { ClassroomBuilding = ObjectMapper.Map<ClassroomBuildingDto>(classroomBuilding) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClassroomBuildings_Edit)]
        public async Task<GetClassroomBuildingForEditOutput> GetClassroomBuildingForEdit(EntityDto input)
        {
            var classroomBuilding = await _classroomBuildingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClassroomBuildingForEditOutput { ClassroomBuilding = ObjectMapper.Map<CreateOrEditClassroomBuildingDto>(classroomBuilding) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClassroomBuildingDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClassroomBuildings_Create)]
        protected virtual async Task Create(CreateOrEditClassroomBuildingDto input)
        {
            var classroomBuilding = ObjectMapper.Map<ClassroomBuilding>(input);

            if (AbpSession.TenantId != null)
            {
                classroomBuilding.TenantId = (int)AbpSession.TenantId;
            }

            await _classroomBuildingRepository.InsertAsync(classroomBuilding);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassroomBuildings_Edit)]
        protected virtual async Task Update(CreateOrEditClassroomBuildingDto input)
        {
            var classroomBuilding = await _classroomBuildingRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, classroomBuilding);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassroomBuildings_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _classroomBuildingRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetClassroomBuildingsToExcel(GetAllClassroomBuildingsForExcelInput input)
        {
            var classificationClassroomBuildingFilter = input.ClassificationClassroomBuildingFilter.HasValue
                        ? (ClassificationClassroomBuildingEnum)input.ClassificationClassroomBuildingFilter
                        : default;

            var filteredClassroomBuildings = _classroomBuildingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ClassroomBuildingName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassroomBuildingNameFilter), e => e.ClassroomBuildingName == input.ClassroomBuildingNameFilter)
                        .WhereIf(input.MinNumberOfClassroomsFilter != null, e => e.NumberOfClassrooms >= input.MinNumberOfClassroomsFilter)
                        .WhereIf(input.MaxNumberOfClassroomsFilter != null, e => e.NumberOfClassrooms <= input.MaxNumberOfClassroomsFilter)
                        .WhereIf(input.ClassificationClassroomBuildingFilter.HasValue && input.ClassificationClassroomBuildingFilter > -1, e => e.ClassificationClassroomBuilding == classificationClassroomBuildingFilter);

            var query = (from o in filteredClassroomBuildings
                         select new GetClassroomBuildingForViewDto()
                         {
                             ClassroomBuilding = new ClassroomBuildingDto
                             {
                                 ClassroomBuildingName = o.ClassroomBuildingName,
                                 NumberOfClassrooms = o.NumberOfClassrooms,
                                 ClassificationClassroomBuilding = o.ClassificationClassroomBuilding,
                                 Id = o.Id
                             }
                         });

            var classroomBuildingListDtos = await query.ToListAsync();

            return _classroomBuildingsExcelExporter.ExportToFile(classroomBuildingListDtos);
        }

    }
}