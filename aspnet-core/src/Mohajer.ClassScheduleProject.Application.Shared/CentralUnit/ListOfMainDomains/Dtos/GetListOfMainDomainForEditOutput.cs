using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos
{
    public class GetListOfMainDomainForEditOutput
    {
        public CreateOrEditListOfMainDomainDto ListOfMainDomain { get; set; }

    }
}