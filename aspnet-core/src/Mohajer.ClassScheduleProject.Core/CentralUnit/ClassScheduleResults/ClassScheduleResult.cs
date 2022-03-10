using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults
{
    [Table("ClassScheduleResults")]
    [Audited]
    public class ClassScheduleResult : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual long WorkTimeInDayId { get; set; }

        public virtual long LessonId { get; set; }

        public virtual int UniversityProfessorId { get; set; }

        public virtual long SemesterId { get; set; }

        public virtual int GradeId { get; set; }

        public virtual int UniversityMajorId { get; set; }

        public virtual int UniversityDepartmentId { get; set; }

        public virtual int ClassroomBuildingId { get; set; }

        public virtual long ListOfAllCalculatedResultId { get; set; }

        [ForeignKey("ListOfAllCalculatedResultId")]
        public ListOfAllCalculatedResult ListOfAllCalculatedResultFk { get; set; }

        public virtual long ClassScheduleModeSpaceId { get; set; }
        public virtual long? MainDomainID { get; set; }

        [ForeignKey("ClassScheduleModeSpaceId")]
        public ClassScheduleModeSpace ClassScheduleModeSpaceFk { get; set; }

    }
}