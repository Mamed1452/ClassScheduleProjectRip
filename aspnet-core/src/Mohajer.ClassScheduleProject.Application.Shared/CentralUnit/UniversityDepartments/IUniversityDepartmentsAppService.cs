using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments
{
    public interface IUniversityDepartmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetUniversityDepartmentForViewDto>> GetAll(GetAllUniversityDepartmentsInput input);

        Task<GetUniversityDepartmentForViewDto> GetUniversityDepartmentForView(int id);

        Task<GetUniversityDepartmentForEditOutput> GetUniversityDepartmentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditUniversityDepartmentDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetUniversityDepartmentsToExcel(GetAllUniversityDepartmentsForExcelInput input);

    }
}