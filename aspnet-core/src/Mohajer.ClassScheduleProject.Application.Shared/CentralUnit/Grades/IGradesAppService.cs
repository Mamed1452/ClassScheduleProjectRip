using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades
{
    public interface IGradesAppService : IApplicationService
    {
        Task<PagedResultDto<GetGradeForViewDto>> GetAll(GetAllGradesInput input);

        Task<GetGradeForViewDto> GetGradeForView(int id);

        Task<GetGradeForEditOutput> GetGradeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditGradeDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetGradesToExcel(GetAllGradesForExcelInput input);

    }
}