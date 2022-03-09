using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces
{
    [Table("ClassScheduleModeSpaces")]
    [Audited]
    public class ClassScheduleModeSpace : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ClassScheduleModeSpaceConsts.MaxNameClassScheduleModeSpacesLength, MinimumLength = ClassScheduleModeSpaceConsts.MinNameClassScheduleModeSpacesLength)]
        public virtual string NameClassScheduleModeSpaces { get; set; }

        public virtual bool IsLock { get; set; }

        public virtual long ListOfClassScheduleModeSpaceId { get; set; }

        [ForeignKey("ListOfClassScheduleModeSpaceId")]
        public ListOfClassScheduleModeSpace ListOfClassScheduleModeSpaceFk { get; set; }

        public virtual int UniversityProfessorId { get; set; }

        [ForeignKey("UniversityProfessorId")]
        public UniversityProfessor UniversityProfessorFk { get; set; }

        public virtual long WorkTimeInDayId { get; set; }

        [ForeignKey("WorkTimeInDayId")]
        public WorkTimeInDay WorkTimeInDayFk { get; set; }

        public virtual long LessonId { get; set; }

        [ForeignKey("LessonId")]
        public Lesson LessonFk { get; set; }

    }
}