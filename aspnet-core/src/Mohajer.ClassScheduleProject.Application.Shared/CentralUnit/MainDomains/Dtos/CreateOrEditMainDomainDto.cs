using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos
{
    public class CreateOrEditMainDomainDto : EntityDto<long?>
    {

        public string MainDomainName { get; set; }

        public long ListOfMainDomainId { get; set; }

        public long LessonsOfSemesterId { get; set; }

    }
}