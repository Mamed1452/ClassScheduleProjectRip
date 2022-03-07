using Mohajer.ClassScheduleProject.CentralUnit.Lessons;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos
{
    public class CreateOrEditLessonDto : EntityDto<long?>
    {

        [Required]
        [StringLength(LessonConsts.MaxNameLessonLength, MinimumLength = LessonConsts.MinNameLessonLength)]
        public string NameLesson { get; set; }

        [Range(LessonConsts.MinHoursPerWeekValue, LessonConsts.MaxHoursPerWeekValue)]
        public int HoursPerWeek { get; set; }

        public LessonTypeEnum LessonType { get; set; }

        public ClassificationLessonEnum ClassificationLesson { get; set; }

        [Range(LessonConsts.MinNumberOfUnitsValue, LessonConsts.MaxNumberOfUnitsValue)]
        public int NumberOfUnits { get; set; }

        [Range(LessonConsts.MinNumberOfGroupsValue, LessonConsts.MaxNumberOfGroupsValue)]
        public int NumberOfGroups { get; set; }

        public bool IsActive { get; set; }

        public int ClassroomBuildingId { get; set; }

    }
}