using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos
{
    public class GetAssigningUniversityMajorToClassroomBuildingForEditOutput
    {
        public CreateOrEditAssigningUniversityMajorToClassroomBuildingDto AssigningUniversityMajorToClassroomBuilding { get; set; }

        public string UniversityMajorUniversityMajorName { get; set; }

        public string ClassroomBuildingClassroomBuildingName { get; set; }

    }
}