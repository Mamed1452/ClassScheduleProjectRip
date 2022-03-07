using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos
{
    public class GetAllLessonsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameLessonFilter { get; set; }

        public int? MaxHoursPerWeekFilter { get; set; }
        public int? MinHoursPerWeekFilter { get; set; }

        public int? LessonTypeFilter { get; set; }

        public int? ClassificationLessonFilter { get; set; }

        public int? MaxNumberOfUnitsFilter { get; set; }
        public int? MinNumberOfUnitsFilter { get; set; }

        public int? MaxNumberOfGroupsFilter { get; set; }
        public int? MinNumberOfGroupsFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string ClassroomBuildingClassroomBuildingNameFilter { get; set; }

    }
}