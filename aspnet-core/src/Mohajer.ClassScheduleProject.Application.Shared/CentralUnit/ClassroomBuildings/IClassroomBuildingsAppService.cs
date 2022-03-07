using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings
{
    public interface IClassroomBuildingsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClassroomBuildingForViewDto>> GetAll(GetAllClassroomBuildingsInput input);

        Task<GetClassroomBuildingForViewDto> GetClassroomBuildingForView(int id);

        Task<GetClassroomBuildingForEditOutput> GetClassroomBuildingForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditClassroomBuildingDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetClassroomBuildingsToExcel(GetAllClassroomBuildingsForExcelInput input);

    }
}