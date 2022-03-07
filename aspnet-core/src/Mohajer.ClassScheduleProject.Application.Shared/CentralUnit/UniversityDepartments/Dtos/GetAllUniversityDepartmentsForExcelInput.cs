using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos
{
    public class GetAllUniversityDepartmentsForExcelInput
    {
        public string Filter { get; set; }

        public string UniversityDepartmentNameFilter { get; set; }

    }
}