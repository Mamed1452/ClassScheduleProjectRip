using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos
{
    public class CreateOrEditListOfMainDomainDto : EntityDto<long?>
    {

        public string ListOfMainDomainName { get; set; }

    }
}