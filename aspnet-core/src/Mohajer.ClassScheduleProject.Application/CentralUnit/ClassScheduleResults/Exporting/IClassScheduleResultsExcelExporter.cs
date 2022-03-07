using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Exporting
{
    public interface IClassScheduleResultsExcelExporter
    {
        FileDto ExportToFile(List<GetClassScheduleResultForViewDto> classScheduleResults);
    }
}