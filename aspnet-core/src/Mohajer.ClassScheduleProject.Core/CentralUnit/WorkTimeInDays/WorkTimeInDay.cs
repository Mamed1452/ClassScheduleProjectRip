using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleCommonEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays
{
    [Table("WorkTimeInDays")]
    [Audited]
    public class WorkTimeInDay : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxNameWorkTimeInDayLength, MinimumLength = WorkTimeInDayConsts.MinNameWorkTimeInDayLength)]
        public virtual string NameWorkTimeInDay { get; set; }

        public virtual DayOfWeekEnum DayOfWeek { get; set; }

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxstartTimeLength, MinimumLength = WorkTimeInDayConsts.MinstartTimeLength)]
        public virtual string startTime { get; set; }

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxEndTimeLength, MinimumLength = WorkTimeInDayConsts.MinEndTimeLength)]
        public virtual string EndTime { get; set; }

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxWhatTimeOfDayLength, MinimumLength = WorkTimeInDayConsts.MinWhatTimeOfDayLength)]
        public virtual string WhatTimeOfDay { get; set; }

        public virtual int? WhatTimeOfDayIndex { get; set; }

    }
}