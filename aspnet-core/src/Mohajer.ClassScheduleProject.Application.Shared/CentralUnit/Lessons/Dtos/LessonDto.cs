using Mohajer.ClassScheduleProject.CentralUnit.Lessons;

using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos
{
    public class LessonDto : EntityDto<long>
    {
        public string NameLesson { get; set; }

        public int HoursPerWeek { get; set; }

        public LessonTypeEnum LessonType { get; set; }

        public ClassificationLessonEnum ClassificationLesson { get; set; }

        public int NumberOfUnits { get; set; }

        public int NumberOfGroups { get; set; }

        public bool IsActive { get; set; }

        public int ClassroomBuildingId { get; set; }

    }
}