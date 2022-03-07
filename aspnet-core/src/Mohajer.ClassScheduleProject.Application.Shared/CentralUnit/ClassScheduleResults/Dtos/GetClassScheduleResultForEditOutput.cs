using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos
{
    public class GetClassScheduleResultForEditOutput
    {
        public CreateOrEditClassScheduleResultDto ClassScheduleResult { get; set; }

        public string ListOfAllCalculatedResultNameCalculatedResult { get; set; }

        public string ClassScheduleModeSpaceNameClassScheduleModeSpaces { get; set; }

    }
}