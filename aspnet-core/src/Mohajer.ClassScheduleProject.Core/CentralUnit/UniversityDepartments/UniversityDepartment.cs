using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments
{
    [Table("UniversityDepartments")]
    [Audited]
    public class UniversityDepartment : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(UniversityDepartmentConsts.MaxUniversityDepartmentNameLength, MinimumLength = UniversityDepartmentConsts.MinUniversityDepartmentNameLength)]
        public virtual string UniversityDepartmentName { get; set; }

    }
}