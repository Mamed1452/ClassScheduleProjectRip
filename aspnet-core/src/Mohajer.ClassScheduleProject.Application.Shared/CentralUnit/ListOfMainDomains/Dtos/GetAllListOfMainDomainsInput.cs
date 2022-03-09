using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos
{
    public class GetAllListOfMainDomainsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string ListOfMainDomainNameFilter { get; set; }

    }
}