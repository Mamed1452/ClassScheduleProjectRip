using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains
{
    [Table("ListOfMainDomains")]
    [Audited]
    public class ListOfMainDomain : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string ListOfMainDomainName { get; set; }

    }
}