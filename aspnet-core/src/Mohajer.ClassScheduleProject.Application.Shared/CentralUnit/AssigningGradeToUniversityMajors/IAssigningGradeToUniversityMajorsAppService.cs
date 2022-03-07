using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors
{
    public interface IAssigningGradeToUniversityMajorsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAssigningGradeToUniversityMajorForViewDto>> GetAll(GetAllAssigningGradeToUniversityMajorsInput input);

        Task<GetAssigningGradeToUniversityMajorForViewDto> GetAssigningGradeToUniversityMajorForView(long id);

        Task<GetAssigningGradeToUniversityMajorForEditOutput> GetAssigningGradeToUniversityMajorForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditAssigningGradeToUniversityMajorDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetAssigningGradeToUniversityMajorsToExcel(GetAllAssigningGradeToUniversityMajorsForExcelInput input);

        Task<PagedResultDto<AssigningGradeToUniversityMajorGradeLookupTableDto>> GetAllGradeForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<AssigningGradeToUniversityMajorUniversityMajorLookupTableDto>> GetAllUniversityMajorForLookupTable(GetAllForLookupTableInput input);

    }
}