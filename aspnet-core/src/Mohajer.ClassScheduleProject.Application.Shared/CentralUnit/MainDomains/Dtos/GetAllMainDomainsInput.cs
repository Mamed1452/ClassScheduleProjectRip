using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos
{
    public class GetAllMainDomainsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MainDomainNameFilter { get; set; }

        public string ListOfMainDomainListOfMainDomainNameFilter { get; set; }

        public string LessonsOfSemesterLessonsOfSemesterNameFilter { get; set; }

        public long? ListOfMainDomainIdFilter { get; set; }
    }
}