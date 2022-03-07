using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters
{
    public interface ILessonsOfSemestersAppService : IApplicationService
    {
        Task<PagedResultDto<GetLessonsOfSemesterForViewDto>> GetAll(GetAllLessonsOfSemestersInput input);

        Task<GetLessonsOfSemesterForViewDto> GetLessonsOfSemesterForView(long id);

        Task<GetLessonsOfSemesterForEditOutput> GetLessonsOfSemesterForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditLessonsOfSemesterDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetLessonsOfSemestersToExcel(GetAllLessonsOfSemestersForExcelInput input);

        Task<PagedResultDto<LessonsOfSemesterLessonLookupTableDto>> GetAllLessonForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<LessonsOfSemesterSemesterLookupTableDto>> GetAllSemesterForLookupTable(GetAllForLookupTableInput input);

    }
}