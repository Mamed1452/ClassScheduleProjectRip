using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades
{
    [Table("Grades")]
    [Audited]
    public class Grade : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(GradeConsts.MaxGradeNameLength, MinimumLength = GradeConsts.MinGradeNameLength)]
        public virtual string GradeName { get; set; }

    }
}