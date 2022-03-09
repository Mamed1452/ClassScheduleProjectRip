using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Dtos
{
    public class GetAllListOfClassScheduleModeSpacesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}