using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos
{
    public class AssigningUniversityMajorToClassroomBuildingDto : EntityDto<long>
    {
        public int MaximumRestrictionsOnUsingClassroomsAtTheSameTime { get; set; }

        public int UniversityMajorId { get; set; }

        public int ClassroomBuildingId { get; set; }

    }
}