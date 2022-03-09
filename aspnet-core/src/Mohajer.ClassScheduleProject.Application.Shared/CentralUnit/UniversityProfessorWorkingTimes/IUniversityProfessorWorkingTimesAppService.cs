using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes
{
    public interface IUniversityProfessorWorkingTimesAppService : IApplicationService
    {
        Task<PagedResultDto<GetUniversityProfessorWorkingTimeForViewDto>> GetAll(GetAllUniversityProfessorWorkingTimesInput input);

        Task<GetUniversityProfessorWorkingTimeForViewDto> GetUniversityProfessorWorkingTimeForView(long id);

        Task<GetUniversityProfessorWorkingTimeForEditOutput> GetUniversityProfessorWorkingTimeForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditUniversityProfessorWorkingTimeDto input);
        Task CreateGroup(CreateGroupInputDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetUniversityProfessorWorkingTimesToExcel(GetAllUniversityProfessorWorkingTimesForExcelInput input);

        Task<PagedResultDto<UniversityProfessorWorkingTimeUniversityProfessorLookupTableDto>> GetAllUniversityProfessorForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<UniversityProfessorWorkingTimeWorkTimeInDayLookupTableDto>> GetAllWorkTimeInDayForLookupTable(GetAllForLookupTableInput input);

    }
}