using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos
{
    public class ClassScheduleModeSpaceDto : EntityDto<long>
    {
        public string NameClassScheduleModeSpaces { get; set; }

        public bool IsLock { get; set; }

        public int UniversityProfessorId { get; set; }

        public long WorkTimeInDayId { get; set; }

        public long LessonId { get; set; }

    }
}