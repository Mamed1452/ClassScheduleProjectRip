using Mohajer.ClassScheduleProject.CentralUnit.Lessons;

using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos
{
    public class LessonsOfSemesterDto : EntityDto<long>
    {
        public LessonOfSemesterTypeEnum LessonOfSemesterType { get; set; }

        public int NumberOfClassesToBeFormed { get; set; }

        public bool IsActive { get; set; }

        public string LessonsOfSemesterName { get; set; }

        public long LessonId { get; set; }

        public long SemesterId { get; set; }

    }
}