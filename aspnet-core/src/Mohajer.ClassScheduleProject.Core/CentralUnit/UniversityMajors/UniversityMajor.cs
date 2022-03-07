using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors
{
    [Table("UniversityMajors")]
    [Audited]
    public class UniversityMajor : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(UniversityMajorConsts.MaxUniversityMajorNameLength, MinimumLength = UniversityMajorConsts.MinUniversityMajorNameLength)]
        public virtual string UniversityMajorName { get; set; }

        public virtual UniversityMajorTypeEnum UniversityMajorType { get; set; }

        public virtual int UniversityDepartmentId { get; set; }

        [ForeignKey("UniversityDepartmentId")]
        public UniversityDepartment UniversityDepartmentFk { get; set; }

    }
}