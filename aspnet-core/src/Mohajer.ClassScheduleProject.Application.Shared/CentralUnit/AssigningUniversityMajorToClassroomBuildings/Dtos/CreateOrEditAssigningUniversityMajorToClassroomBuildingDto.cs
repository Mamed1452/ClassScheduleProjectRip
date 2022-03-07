using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos
{
    public class CreateOrEditAssigningUniversityMajorToClassroomBuildingDto : EntityDto<long?>
    {

        [Range(AssigningUniversityMajorToClassroomBuildingConsts.MinMaximumRestrictionsOnUsingClassroomsAtTheSameTimeValue, AssigningUniversityMajorToClassroomBuildingConsts.MaxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeValue)]
        public int MaximumRestrictionsOnUsingClassroomsAtTheSameTime { get; set; }

        public int UniversityMajorId { get; set; }

        public int ClassroomBuildingId { get; set; }

    }
}