using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Exporting
{
    public interface IClassroomBuildingsExcelExporter
    {
        FileDto ExportToFile(List<GetClassroomBuildingForViewDto> classroomBuildings);
    }
}