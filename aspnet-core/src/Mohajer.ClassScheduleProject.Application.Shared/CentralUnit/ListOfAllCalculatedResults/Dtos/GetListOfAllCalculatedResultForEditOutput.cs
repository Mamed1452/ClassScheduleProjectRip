using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos
{
    public class GetListOfAllCalculatedResultForEditOutput
    {
        public CreateOrEditListOfAllCalculatedResultDto ListOfAllCalculatedResult { get; set; }

    }
}