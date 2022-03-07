using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos
{
    public class GetAllClassScheduleModeSpacesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameClassScheduleModeSpacesFilter { get; set; }

        public int? IsLockFilter { get; set; }

        public string UniversityProfessorUniversityProfessorNameFilter { get; set; }

        public string WorkTimeInDayNameWorkTimeInDayFilter { get; set; }

        public string LessonNameLessonFilter { get; set; }

    }
}