using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos
{
    public class GetAllClassroomBuildingsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string ClassroomBuildingNameFilter { get; set; }

        public int? MaxNumberOfClassroomsFilter { get; set; }
        public int? MinNumberOfClassroomsFilter { get; set; }

        public int? ClassificationClassroomBuildingFilter { get; set; }

    }
}