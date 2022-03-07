using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes
{
    [Table("UniversityProfessorWorkingTimes")]
    [Audited]
    public class UniversityProfessorWorkingTime : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual int UniversityProfessorId { get; set; }

        [ForeignKey("UniversityProfessorId")]
        public UniversityProfessor UniversityProfessorFk { get; set; }

        public virtual long WorkTimeInDayId { get; set; }

        [ForeignKey("WorkTimeInDayId")]
        public WorkTimeInDay WorkTimeInDayFk { get; set; }

    }
}