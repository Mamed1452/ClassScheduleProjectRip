using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades.Exporting
{
    public interface IGradesExcelExporter
    {
        FileDto ExportToFile(List<GetGradeForViewDto> grades);
    }
}