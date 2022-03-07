using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos
{
    public class GetAllUniversityMajorsForExcelInput
    {
        public string Filter { get; set; }

        public string UniversityMajorNameFilter { get; set; }

        public int? UniversityMajorTypeFilter { get; set; }

        public string UniversityDepartmentUniversityDepartmentNameFilter { get; set; }

        public int? UniversityDepartmentIdFilter { get; set; }
    }
}