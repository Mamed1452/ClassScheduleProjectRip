using Mohajer.ClassScheduleProject.CentralUnit.Lessons;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos
{
    public class CreateOrEditLessonsOfSemesterDto : EntityDto<long?>
    {

        public LessonOfSemesterTypeEnum LessonOfSemesterType { get; set; }

        [Range(LessonsOfSemesterConsts.MinNumberOfClassesToBeFormedValue, LessonsOfSemesterConsts.MaxNumberOfClassesToBeFormedValue)]
        public int NumberOfClassesToBeFormed { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string LessonsOfSemesterName { get; set; }

        public long LessonId { get; set; }

        public long SemesterId { get; set; }

    }
}