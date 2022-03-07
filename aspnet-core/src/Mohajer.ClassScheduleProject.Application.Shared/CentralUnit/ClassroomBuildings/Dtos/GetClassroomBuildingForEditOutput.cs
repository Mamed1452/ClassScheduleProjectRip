using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos
{
    public class GetClassroomBuildingForEditOutput
    {
        public CreateOrEditClassroomBuildingDto ClassroomBuilding { get; set; }

    }
}