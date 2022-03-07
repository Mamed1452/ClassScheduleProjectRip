using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos
{
    public class GetAllSemestersForExcelInput
    {
        public string Filter { get; set; }

        public string SemesterNameFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajorFilter { get; set; }

        public long? AssigningGradeToUniversityMajorIdFilter { get; set; }
    }
}