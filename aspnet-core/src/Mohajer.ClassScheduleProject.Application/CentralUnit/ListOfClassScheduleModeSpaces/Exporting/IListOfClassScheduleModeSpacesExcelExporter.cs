using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Exporting
{
    public interface IListOfClassScheduleModeSpacesExcelExporter
    {
        FileDto ExportToFile(List<GetListOfClassScheduleModeSpaceForViewDto> listOfClassScheduleModeSpaces);
    }
}