using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos
{
    public class ListOfAllCalculatedResultDto : EntityDto<long>
    {
        public string NameCalculatedResult { get; set; }

        public int Price { get; set; }

    }
}