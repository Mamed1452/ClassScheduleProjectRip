using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleCommonEnums;

using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos
{
    public class WorkTimeInDayDto : EntityDto<long>
    {
        public string NameWorkTimeInDay { get; set; }

        public DayOfWeekEnum DayOfWeek { get; set; }

        public string startTime { get; set; }

        public string EndTime { get; set; }

        public string WhatTimeOfDay { get; set; }

    }
}