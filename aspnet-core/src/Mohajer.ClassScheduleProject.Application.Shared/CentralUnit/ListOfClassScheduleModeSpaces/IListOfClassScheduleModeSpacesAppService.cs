using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces
{
    public interface IListOfClassScheduleModeSpacesAppService : IApplicationService
    {
        Task<PagedResultDto<GetListOfClassScheduleModeSpaceForViewDto>> GetAll(GetAllListOfClassScheduleModeSpacesInput input);

        Task<GetListOfClassScheduleModeSpaceForEditOutput> GetListOfClassScheduleModeSpaceForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditListOfClassScheduleModeSpaceDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetListOfClassScheduleModeSpacesToExcel(GetAllListOfClassScheduleModeSpacesForExcelInput input);

    }
}