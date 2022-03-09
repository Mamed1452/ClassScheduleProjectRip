using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults
{
    public interface IClassScheduleResultsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClassScheduleResultForViewDto>> GetAll(GetAllClassScheduleResultsInput input);

        Task<GetClassScheduleResultForViewDto> GetClassScheduleResultForView(long id);

        Task<GetClassScheduleResultForEditOutput> GetClassScheduleResultForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClassScheduleResultDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetClassScheduleResultsToExcel(GetAllClassScheduleResultsForExcelInput input);

        Task<PagedResultDto<ClassScheduleResultListOfAllCalculatedResultLookupTableDto>> GetAllListOfAllCalculatedResultForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<ClassScheduleResultClassScheduleModeSpaceLookupTableDto>> GetAllClassScheduleModeSpaceForLookupTable(GetAllForLookupTableInput input);
        Task<StartClassScheduleOutputDto> StartClassSchedule(StartClassScheduleInputDto input);

    }
}