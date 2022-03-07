using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors
{
    public interface IUniversityMajorsAppService : IApplicationService
    {
        Task<PagedResultDto<GetUniversityMajorForViewDto>> GetAll(GetAllUniversityMajorsInput input);

        Task<GetUniversityMajorForViewDto> GetUniversityMajorForView(int id);

        Task<GetUniversityMajorForEditOutput> GetUniversityMajorForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditUniversityMajorDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetUniversityMajorsToExcel(GetAllUniversityMajorsForExcelInput input);

        Task<PagedResultDto<UniversityMajorUniversityDepartmentLookupTableDto>> GetAllUniversityDepartmentForLookupTable(GetAllForLookupTableInput input);

    }
}