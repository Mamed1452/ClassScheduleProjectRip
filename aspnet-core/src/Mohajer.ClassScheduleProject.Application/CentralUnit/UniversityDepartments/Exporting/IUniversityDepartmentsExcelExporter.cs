using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Exporting
{
    public interface IUniversityDepartmentsExcelExporter
    {
        FileDto ExportToFile(List<GetUniversityDepartmentForViewDto> universityDepartments);
    }
}