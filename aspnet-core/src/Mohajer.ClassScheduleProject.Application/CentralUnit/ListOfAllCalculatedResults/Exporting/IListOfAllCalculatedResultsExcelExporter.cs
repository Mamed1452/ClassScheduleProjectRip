using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Exporting
{
    public interface IListOfAllCalculatedResultsExcelExporter
    {
        FileDto ExportToFile(List<GetListOfAllCalculatedResultForViewDto> listOfAllCalculatedResults);
    }
}