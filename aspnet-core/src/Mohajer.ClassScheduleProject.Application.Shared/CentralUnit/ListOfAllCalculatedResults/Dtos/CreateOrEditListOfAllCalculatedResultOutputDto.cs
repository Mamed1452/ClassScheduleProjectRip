using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos
{
    public class CreateOrEditListOfAllCalculatedResultOutputDto : EntityDto<long?>
    {
        public bool IsCreated { get; set; }
    }
}
