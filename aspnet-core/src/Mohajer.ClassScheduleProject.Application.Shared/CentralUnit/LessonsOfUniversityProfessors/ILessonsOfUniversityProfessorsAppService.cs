using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors
{
    public interface ILessonsOfUniversityProfessorsAppService : IApplicationService
    {
        Task<PagedResultDto<GetLessonsOfUniversityProfessorForViewDto>> GetAll(GetAllLessonsOfUniversityProfessorsInput input);

        Task<GetLessonsOfUniversityProfessorForViewDto> GetLessonsOfUniversityProfessorForView(long id);

        Task<GetLessonsOfUniversityProfessorForEditOutput> GetLessonsOfUniversityProfessorForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditLessonsOfUniversityProfessorDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetLessonsOfUniversityProfessorsToExcel(GetAllLessonsOfUniversityProfessorsForExcelInput input);

        Task<PagedResultDto<LessonsOfUniversityProfessorLessonLookupTableDto>> GetAllLessonForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<LessonsOfUniversityProfessorUniversityProfessorLookupTableDto>> GetAllUniversityProfessorForLookupTable(GetAllForLookupTableInput input);

    }
}