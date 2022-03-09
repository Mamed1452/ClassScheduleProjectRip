using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos
{
    public class GetClassScheduleModeSpaceForEditOutput
    {
        public CreateOrEditClassScheduleModeSpaceDto ClassScheduleModeSpace { get; set; }

        public string ListOfClassScheduleModeSpaceListOfClassScheduleModeSpaceName { get; set; }

        public string UniversityProfessorUniversityProfessorName { get; set; }

        public string WorkTimeInDayNameWorkTimeInDay { get; set; }

        public string LessonNameLesson { get; set; }

    }
}