using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos
{
    public class GetMainDomainForEditOutput
    {
        public CreateOrEditMainDomainDto MainDomain { get; set; }

        public string ListOfMainDomainListOfMainDomainName { get; set; }

        public string LessonsOfSemesterLessonsOfSemesterName { get; set; }

    }
}