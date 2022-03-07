using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Exporting
{
    public interface IWorkTimeInDaysExcelExporter
    {
        FileDto ExportToFile(List<GetWorkTimeInDayForViewDto> workTimeInDays);
    }
}