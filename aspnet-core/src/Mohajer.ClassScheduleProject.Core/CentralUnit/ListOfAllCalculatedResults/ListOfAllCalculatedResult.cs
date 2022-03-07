using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults
{
    [Table("ListOfAllCalculatedResults")]
    [Audited]
    public class ListOfAllCalculatedResult : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ListOfAllCalculatedResultConsts.MaxNameCalculatedResultLength, MinimumLength = ListOfAllCalculatedResultConsts.MinNameCalculatedResultLength)]
        public virtual string NameCalculatedResult { get; set; }

        [Range(ListOfAllCalculatedResultConsts.MinPriceValue, ListOfAllCalculatedResultConsts.MaxPriceValue)]
        public virtual int Price { get; set; }

    }
}