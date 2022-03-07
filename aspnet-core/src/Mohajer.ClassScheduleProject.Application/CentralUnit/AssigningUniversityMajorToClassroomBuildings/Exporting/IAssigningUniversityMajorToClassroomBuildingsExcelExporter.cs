using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Exporting
{
    public interface IAssigningUniversityMajorToClassroomBuildingsExcelExporter
    {
        FileDto ExportToFile(List<GetAssigningUniversityMajorToClassroomBuildingForViewDto> assigningUniversityMajorToClassroomBuildings);
    }
}