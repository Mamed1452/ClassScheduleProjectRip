using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters
{
    public interface ISemestersAppService : IApplicationService
    {
        Task<PagedResultDto<GetSemesterForViewDto>> GetAll(GetAllSemestersInput input);

        Task<GetSemesterForViewDto> GetSemesterForView(long id);

        Task<GetSemesterForEditOutput> GetSemesterForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditSemesterDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetSemestersToExcel(GetAllSemestersForExcelInput input);

        Task<PagedResultDto<SemesterAssigningGradeToUniversityMajorLookupTableDto>> GetAllAssigningGradeToUniversityMajorForLookupTable(GetAllForLookupTableInput input);

    }
}