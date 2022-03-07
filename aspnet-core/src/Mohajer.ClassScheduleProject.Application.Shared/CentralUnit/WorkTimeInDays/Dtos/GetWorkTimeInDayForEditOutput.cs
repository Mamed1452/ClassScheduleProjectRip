using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos
{
    public class GetWorkTimeInDayForEditOutput
    {
        public CreateOrEditWorkTimeInDayDto WorkTimeInDay { get; set; }

    }
}