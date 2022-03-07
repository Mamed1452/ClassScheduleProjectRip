using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors
{
    [Table("UniversityProfessors")]
    [Audited]
    public class UniversityProfessor : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(UniversityProfessorConsts.MaxUniversityProfessorNameLength, MinimumLength = UniversityProfessorConsts.MinUniversityProfessorNameLength)]
        public virtual string UniversityProfessorName { get; set; }

        public virtual bool IsActive { get; set; }

    }
}