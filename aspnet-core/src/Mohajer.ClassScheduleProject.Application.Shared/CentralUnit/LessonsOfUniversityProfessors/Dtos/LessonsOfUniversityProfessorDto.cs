using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos
{
    public class LessonsOfUniversityProfessorDto : EntityDto<long>
    {
        public bool IsActive { get; set; }

        public long LessonId { get; set; }

        public int UniversityProfessorId { get; set; }

    }
}