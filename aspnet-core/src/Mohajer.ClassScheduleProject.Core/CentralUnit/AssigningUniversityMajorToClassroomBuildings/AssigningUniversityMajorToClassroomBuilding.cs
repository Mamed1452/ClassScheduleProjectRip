using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings
{
    [Table("AssigningUniversityMajorToClassroomBuildings")]
    [Audited]
    public class AssigningUniversityMajorToClassroomBuilding : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Range(AssigningUniversityMajorToClassroomBuildingConsts.MinMaximumRestrictionsOnUsingClassroomsAtTheSameTimeValue, AssigningUniversityMajorToClassroomBuildingConsts.MaxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeValue)]
        public virtual int MaximumRestrictionsOnUsingClassroomsAtTheSameTime { get; set; }

        public virtual int UniversityMajorId { get; set; }

        [ForeignKey("UniversityMajorId")]
        public UniversityMajor UniversityMajorFk { get; set; }

        public virtual int ClassroomBuildingId { get; set; }

        [ForeignKey("ClassroomBuildingId")]
        public ClassroomBuilding ClassroomBuildingFk { get; set; }

    }
}