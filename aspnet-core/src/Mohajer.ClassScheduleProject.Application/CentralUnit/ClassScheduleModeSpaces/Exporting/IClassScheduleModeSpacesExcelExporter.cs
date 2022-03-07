using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Exporting
{
    public interface IClassScheduleModeSpacesExcelExporter
    {
        FileDto ExportToFile(List<GetClassScheduleModeSpaceForViewDto> classScheduleModeSpaces);
    }
}