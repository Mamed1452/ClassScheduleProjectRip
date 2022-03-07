using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters.Exporting
{
    public interface ISemestersExcelExporter
    {
        FileDto ExportToFile(List<GetSemesterForViewDto> semesters);
    }
}