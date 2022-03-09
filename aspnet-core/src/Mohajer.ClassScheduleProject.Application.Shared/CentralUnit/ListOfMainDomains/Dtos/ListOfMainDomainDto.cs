using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos
{
    public class ListOfMainDomainDto : EntityDto<long>
    {
        public string ListOfMainDomainName { get; set; }

    }
}