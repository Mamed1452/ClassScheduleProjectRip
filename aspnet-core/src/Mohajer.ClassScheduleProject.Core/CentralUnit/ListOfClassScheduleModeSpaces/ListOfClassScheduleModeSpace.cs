using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces
{
    [Table("ListOfClassScheduleModeSpaces")]
    public class ListOfClassScheduleModeSpace : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public virtual string ListOfClassScheduleModeSpaceName { get; set; }

    }
}