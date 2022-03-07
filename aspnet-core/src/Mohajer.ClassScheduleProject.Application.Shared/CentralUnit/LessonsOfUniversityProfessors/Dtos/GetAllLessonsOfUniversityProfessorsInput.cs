using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos
{
    public class GetAllLessonsOfUniversityProfessorsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string LessonNameLessonFilter { get; set; }

        public string UniversityProfessorUniversityProfessorNameFilter { get; set; }

    }
}