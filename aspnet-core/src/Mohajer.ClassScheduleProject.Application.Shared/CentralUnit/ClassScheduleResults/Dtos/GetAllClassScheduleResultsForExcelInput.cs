﻿using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos
{
    public class GetAllClassScheduleResultsForExcelInput
    {
        public string Filter { get; set; }

        public long? MaxWorkTimeInDayIdFilter { get; set; }
        public long? MinWorkTimeInDayIdFilter { get; set; }

        public int? MaxLessonIdFilter { get; set; }
        public int? MinLessonIdFilter { get; set; }

        public int? MaxUniversityProfessorIdFilter { get; set; }
        public int? MinUniversityProfessorIdFilter { get; set; }

        public long? MaxSemesterIdFilter { get; set; }
        public long? MinSemesterIdFilter { get; set; }

        public int? MaxGradeIdFilter { get; set; }
        public int? MinGradeIdFilter { get; set; }

        public int? MaxUniversityMajorIdFilter { get; set; }
        public int? MinUniversityMajorIdFilter { get; set; }

        public int? MaxUniversityDepartmentIdFilter { get; set; }
        public int? MinUniversityDepartmentIdFilter { get; set; }

        public string ListOfAllCalculatedResultNameCalculatedResultFilter { get; set; }

        public string ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter { get; set; }

        public long? ListOfAllCalculatedResultIdFilter { get; set; }
    }
}