using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces
{
    public interface IClassScheduleModeSpacesAppService : IApplicationService
    {
        Task<PagedResultDto<GetClassScheduleModeSpaceForViewDto>> GetAll(GetAllClassScheduleModeSpacesInput input);

        Task<GetClassScheduleModeSpaceForViewDto> GetClassScheduleModeSpaceForView(long id);

        Task<GetClassScheduleModeSpaceForEditOutput> GetClassScheduleModeSpaceForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClassScheduleModeSpaceDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetClassScheduleModeSpacesToExcel(GetAllClassScheduleModeSpacesForExcelInput input);

        Task<PagedResultDto<ClassScheduleModeSpaceListOfClassScheduleModeSpaceLookupTableDto>> GetAllListOfClassScheduleModeSpaceForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<ClassScheduleModeSpaceUniversityProfessorLookupTableDto>> GetAllUniversityProfessorForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<ClassScheduleModeSpaceWorkTimeInDayLookupTableDto>> GetAllWorkTimeInDayForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<ClassScheduleModeSpaceLessonLookupTableDto>> GetAllLessonForLookupTable(GetAllForLookupTableInput input);

    }
}