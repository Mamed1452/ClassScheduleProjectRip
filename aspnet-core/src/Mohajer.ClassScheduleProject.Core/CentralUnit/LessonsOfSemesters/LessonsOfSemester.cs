using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters
{
    [Table("LessonsOfSemesters")]
    [Audited]
    public class LessonsOfSemester : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual LessonOfSemesterTypeEnum LessonOfSemesterType { get; set; }

        [Range(LessonsOfSemesterConsts.MinNumberOfClassesToBeFormedValue, LessonsOfSemesterConsts.MaxNumberOfClassesToBeFormedValue)]
        public virtual int NumberOfClassesToBeFormed { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual long LessonId { get; set; }

        [ForeignKey("LessonId")]
        public Lesson LessonFk { get; set; }

        public virtual long SemesterId { get; set; }

        [ForeignKey("SemesterId")]
        public Semester SemesterFk { get; set; }

    }
}