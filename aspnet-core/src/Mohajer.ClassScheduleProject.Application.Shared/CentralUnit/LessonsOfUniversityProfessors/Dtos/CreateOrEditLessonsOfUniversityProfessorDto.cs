using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos
{
    public class CreateOrEditLessonsOfUniversityProfessorDto : EntityDto<long?>
    {

        public bool IsActive { get; set; }

        public long LessonId { get; set; }

        public int UniversityProfessorId { get; set; }

    }
}