using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos
{
    public class MainDomainDto : EntityDto<long>
    {
        public string MainDomainName { get; set; }

        public long ListOfMainDomainId { get; set; }

        public long LessonsOfSemesterId { get; set; }

    }
}