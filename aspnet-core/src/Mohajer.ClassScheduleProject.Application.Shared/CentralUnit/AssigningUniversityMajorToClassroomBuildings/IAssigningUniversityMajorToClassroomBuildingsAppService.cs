using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings
{
    public interface IAssigningUniversityMajorToClassroomBuildingsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAssigningUniversityMajorToClassroomBuildingForViewDto>> GetAll(GetAllAssigningUniversityMajorToClassroomBuildingsInput input);

        Task<GetAssigningUniversityMajorToClassroomBuildingForViewDto> GetAssigningUniversityMajorToClassroomBuildingForView(long id);

        Task<GetAssigningUniversityMajorToClassroomBuildingForEditOutput> GetAssigningUniversityMajorToClassroomBuildingForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditAssigningUniversityMajorToClassroomBuildingDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetAssigningUniversityMajorToClassroomBuildingsToExcel(GetAllAssigningUniversityMajorToClassroomBuildingsForExcelInput input);

        Task<PagedResultDto<AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableDto>> GetAllUniversityMajorForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableDto>> GetAllClassroomBuildingForLookupTable(GetAllForLookupTableInput input);

    }
}