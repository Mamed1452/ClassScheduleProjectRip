using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleCommonEnums;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos
{
    public class CreateOrEditWorkTimeInDayDto : EntityDto<long?>
    {

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxNameWorkTimeInDayLength, MinimumLength = WorkTimeInDayConsts.MinNameWorkTimeInDayLength)]
        public string NameWorkTimeInDay { get; set; }

        public DayOfWeekEnum DayOfWeek { get; set; }

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxstartTimeLength, MinimumLength = WorkTimeInDayConsts.MinstartTimeLength)]
        public string startTime { get; set; }

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxEndTimeLength, MinimumLength = WorkTimeInDayConsts.MinEndTimeLength)]
        public string EndTime { get; set; }

        [Required]
        [StringLength(WorkTimeInDayConsts.MaxWhatTimeOfDayLength, MinimumLength = WorkTimeInDayConsts.MinWhatTimeOfDayLength)]
        public string WhatTimeOfDay { get; set; }

    }
}