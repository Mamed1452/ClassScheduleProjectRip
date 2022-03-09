using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos
{
    public class GetAllMainDomainsForExcelInput
    {
        public string Filter { get; set; }

        public string MainDomainNameFilter { get; set; }

        public string ListOfMainDomainListOfMainDomainNameFilter { get; set; }

        public string LessonsOfSemesterLessonsOfSemesterNameFilter { get; set; }

        public long? ListOfMainDomainIdFilter { get; set; }
    }
}