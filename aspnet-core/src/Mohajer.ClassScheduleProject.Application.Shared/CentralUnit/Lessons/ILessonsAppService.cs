using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons
{
    public interface ILessonsAppService : IApplicationService
    {
        Task<PagedResultDto<GetLessonForViewDto>> GetAll(GetAllLessonsInput input);

        Task<GetLessonForViewDto> GetLessonForView(long id);

        Task<GetLessonForEditOutput> GetLessonForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditLessonDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetLessonsToExcel(GetAllLessonsForExcelInput input);

        Task<PagedResultDto<LessonClassroomBuildingLookupTableDto>> GetAllClassroomBuildingForLookupTable(GetAllForLookupTableInput input);

    }
}