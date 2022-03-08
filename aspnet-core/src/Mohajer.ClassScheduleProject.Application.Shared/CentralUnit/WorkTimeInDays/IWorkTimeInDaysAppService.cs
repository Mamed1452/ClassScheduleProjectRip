using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays
{
    public interface IWorkTimeInDaysAppService : IApplicationService
    {
        Task<PagedResultDto<GetWorkTimeInDayForViewDto>> GetAll(GetAllWorkTimeInDaysInput input);

        Task<GetWorkTimeInDayForViewDto> GetWorkTimeInDayForView(long id);

        Task<GetWorkTimeInDayForEditOutput> GetWorkTimeInDayForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditWorkTimeInDayDto input);

        Task CreateAllWorkTimeInDay();

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetWorkTimeInDaysToExcel(GetAllWorkTimeInDaysForExcelInput input);

    }
}