using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains
{
    [Table("MainDomains")]
    [Audited]
    public class MainDomain : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string MainDomainName { get; set; }

        public virtual long ListOfMainDomainId { get; set; }

        [ForeignKey("ListOfMainDomainId")]
        public ListOfMainDomain ListOfMainDomainFk { get; set; }

        public virtual long LessonsOfSemesterId { get; set; }

        [ForeignKey("LessonsOfSemesterId")]
        public LessonsOfSemester LessonsOfSemesterFk { get; set; }

    }
}