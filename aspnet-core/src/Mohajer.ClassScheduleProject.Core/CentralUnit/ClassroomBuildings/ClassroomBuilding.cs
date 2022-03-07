using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings
{
    [Table("ClassroomBuildings")]
    [Audited]
    public class ClassroomBuilding : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ClassroomBuildingConsts.MaxClassroomBuildingNameLength, MinimumLength = ClassroomBuildingConsts.MinClassroomBuildingNameLength)]
        public virtual string ClassroomBuildingName { get; set; }

        [Range(ClassroomBuildingConsts.MinNumberOfClassroomsValue, ClassroomBuildingConsts.MaxNumberOfClassroomsValue)]
        public virtual int NumberOfClassrooms { get; set; }

        public virtual ClassificationClassroomBuildingEnum ClassificationClassroomBuilding { get; set; }

    }
}