using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos
{
    public class GetAllLessonsOfSemestersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? LessonOfSemesterTypeFilter { get; set; }

        public int? MaxNumberOfClassesToBeFormedFilter { get; set; }
        public int? MinNumberOfClassesToBeFormedFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string LessonsOfSemesterNameFilter { get; set; }

        public string LessonNameLessonFilter { get; set; }

        public string SemesterSemesterNameFilter { get; set; }

    }
}