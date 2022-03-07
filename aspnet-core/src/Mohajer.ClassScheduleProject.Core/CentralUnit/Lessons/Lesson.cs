using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons
{
    [Table("Lessons")]
    [Audited]
    public class Lesson : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(LessonConsts.MaxNameLessonLength, MinimumLength = LessonConsts.MinNameLessonLength)]
        public virtual string NameLesson { get; set; }

        [Range(LessonConsts.MinHoursPerWeekValue, LessonConsts.MaxHoursPerWeekValue)]
        public virtual int HoursPerWeek { get; set; }

        public virtual LessonTypeEnum LessonType { get; set; }

        public virtual ClassificationLessonEnum ClassificationLesson { get; set; }

        [Range(LessonConsts.MinNumberOfUnitsValue, LessonConsts.MaxNumberOfUnitsValue)]
        public virtual int NumberOfUnits { get; set; }

        [Range(LessonConsts.MinNumberOfGroupsValue, LessonConsts.MaxNumberOfGroupsValue)]
        public virtual int NumberOfGroups { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual int ClassroomBuildingId { get; set; }

        [ForeignKey("ClassroomBuildingId")]
        public ClassroomBuilding ClassroomBuildingFk { get; set; }

    }
}