using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos
{
    public class GetAllAssigningUniversityMajorToClassroomBuildingsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? MaxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter { get; set; }
        public int? MinMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter { get; set; }

        public string UniversityMajorUniversityMajorNameFilter { get; set; }

        public string ClassroomBuildingClassroomBuildingNameFilter { get; set; }

    }
}