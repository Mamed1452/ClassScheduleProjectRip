using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors
{
    [Table("LessonsOfUniversityProfessors")]
    [Audited]
    public class LessonsOfUniversityProfessor : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual long LessonId { get; set; }

        [ForeignKey("LessonId")]
        public Lesson LessonFk { get; set; }

        public virtual int UniversityProfessorId { get; set; }

        [ForeignKey("UniversityProfessorId")]
        public UniversityProfessor UniversityProfessorFk { get; set; }

    }
}