using Mohajer.ClassScheduleProject.CentralUnit.Grades;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors
{
    [Table("AssigningGradeToUniversityMajors")]
    [Audited]
    public class AssigningGradeToUniversityMajor : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(AssigningGradeToUniversityMajorConsts.MaxNameAssignedGradeToUniversityMajorLength, MinimumLength = AssigningGradeToUniversityMajorConsts.MinNameAssignedGradeToUniversityMajorLength)]
        public virtual string NameAssignedGradeToUniversityMajor { get; set; }

        public virtual int GradeId { get; set; }

        [ForeignKey("GradeId")]
        public Grade GradeFk { get; set; }

        public virtual int UniversityMajorId { get; set; }

        [ForeignKey("UniversityMajorId")]
        public UniversityMajor UniversityMajorFk { get; set; }

    }
}