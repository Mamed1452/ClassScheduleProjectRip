using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters
{
    [Table("Semesters")]
    [Audited]
    public class Semester : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(SemesterConsts.MaxSemesterNameLength, MinimumLength = SemesterConsts.MinSemesterNameLength)]
        public virtual string SemesterName { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual long AssigningGradeToUniversityMajorId { get; set; }

        [ForeignKey("AssigningGradeToUniversityMajorId")]
        public AssigningGradeToUniversityMajor AssigningGradeToUniversityMajorFk { get; set; }

    }
}