using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos
{
    public class GetLessonsOfUniversityProfessorForEditOutput
    {
        public CreateOrEditLessonsOfUniversityProfessorDto LessonsOfUniversityProfessor { get; set; }

        public string LessonNameLesson { get; set; }

        public string UniversityProfessorUniversityProfessorName { get; set; }

    }
}