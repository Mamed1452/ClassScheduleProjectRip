using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos
{
    public class GetAllClassScheduleResultsForExcelInput
    {
        public string Filter { get; set; }

        public string WorkTimeInDayFilter { get; set; }

        public string LessonFilter { get; set; }

        public string UniversityProfessorFilter { get; set; }

        public string SemesterFilter { get; set; }

        public string GradeFilter { get; set; }

        public string UniversityMajorFilter { get; set; }

        public string UniversityDepartmentFilter { get; set; }

        public string ClassroomBuildingFilter { get; set; }

        public string ListOfAllCalculatedResultNameCalculatedResultFilter { get; set; }

        public string ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter { get; set; }

        public long? ListOfAllCalculatedResultNameCalculatedResultIdFilter { get; set; }
        public long? ClassScheduleModeSpaceNameClassScheduleModeSpacesIdFilter { get; set; }
        public bool IsAlocated { get; set; }

    }
}