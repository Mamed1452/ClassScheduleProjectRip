using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;

using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos
{
    public class ClassroomBuildingDto : EntityDto
    {
        public string ClassroomBuildingName { get; set; }

        public int NumberOfClassrooms { get; set; }

        public ClassificationClassroomBuildingEnum ClassificationClassroomBuilding { get; set; }

    }
}