using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults
{
    public interface IListOfAllCalculatedResultsAppService : IApplicationService
    {
        Task<PagedResultDto<GetListOfAllCalculatedResultForViewDto>> GetAll(GetAllListOfAllCalculatedResultsInput input);

        Task<GetListOfAllCalculatedResultForViewDto> GetListOfAllCalculatedResultForView(long id);

        Task<GetListOfAllCalculatedResultForEditOutput> GetListOfAllCalculatedResultForEdit(EntityDto<long> input);

        Task<CreateOrEditListOfAllCalculatedResultOutputDto> CreateOrEdit(CreateOrEditListOfAllCalculatedResultDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetListOfAllCalculatedResultsToExcel(GetAllListOfAllCalculatedResultsForExcelInput input);

      

    }
}