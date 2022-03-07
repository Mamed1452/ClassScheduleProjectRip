using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos
{
    public class CreateOrEditClassroomBuildingDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ClassroomBuildingConsts.MaxClassroomBuildingNameLength, MinimumLength = ClassroomBuildingConsts.MinClassroomBuildingNameLength)]
        public string ClassroomBuildingName { get; set; }

        [Range(ClassroomBuildingConsts.MinNumberOfClassroomsValue, ClassroomBuildingConsts.MaxNumberOfClassroomsValue)]
        public int NumberOfClassrooms { get; set; }

        public ClassificationClassroomBuildingEnum ClassificationClassroomBuilding { get; set; }

    }
}