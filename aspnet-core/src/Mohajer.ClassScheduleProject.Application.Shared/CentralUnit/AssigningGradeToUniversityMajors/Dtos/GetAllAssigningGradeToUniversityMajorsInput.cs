using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos
{
    public class GetAllAssigningGradeToUniversityMajorsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameAssignedGradeToUniversityMajorFilter { get; set; }

        public string GradeGradeNameFilter { get; set; }

        public string UniversityMajorUniversityMajorNameFilter { get; set; }

    }
}