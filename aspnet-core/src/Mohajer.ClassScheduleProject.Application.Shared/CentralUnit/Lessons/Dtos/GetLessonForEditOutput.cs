using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos
{
    public class GetLessonForEditOutput
    {
        public CreateOrEditLessonDto Lesson { get; set; }

        public string ClassroomBuildingClassroomBuildingName { get; set; }

    }
}