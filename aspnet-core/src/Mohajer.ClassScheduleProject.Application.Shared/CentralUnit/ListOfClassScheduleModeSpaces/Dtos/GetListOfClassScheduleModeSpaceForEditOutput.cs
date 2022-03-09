using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Dtos
{
    public class GetListOfClassScheduleModeSpaceForEditOutput
    {
        public CreateOrEditListOfClassScheduleModeSpaceDto ListOfClassScheduleModeSpace { get; set; }

    }
}