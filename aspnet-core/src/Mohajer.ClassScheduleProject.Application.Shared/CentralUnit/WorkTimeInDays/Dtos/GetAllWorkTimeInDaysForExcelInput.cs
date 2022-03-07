using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos
{
    public class GetAllWorkTimeInDaysForExcelInput
    {
        public string Filter { get; set; }

        public string NameWorkTimeInDayFilter { get; set; }

        public int? DayOfWeekFilter { get; set; }

        public string startTimeFilter { get; set; }

        public string EndTimeFilter { get; set; }

        public string WhatTimeOfDayFilter { get; set; }

    }
}