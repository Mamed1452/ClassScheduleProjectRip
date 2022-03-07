using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors
{
    public interface IUniversityProfessorsAppService : IApplicationService
    {
        Task<PagedResultDto<GetUniversityProfessorForViewDto>> GetAll(GetAllUniversityProfessorsInput input);

        Task<GetUniversityProfessorForViewDto> GetUniversityProfessorForView(int id);

        Task<GetUniversityProfessorForEditOutput> GetUniversityProfessorForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditUniversityProfessorDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetUniversityProfessorsToExcel(GetAllUniversityProfessorsForExcelInput input);

    }
}