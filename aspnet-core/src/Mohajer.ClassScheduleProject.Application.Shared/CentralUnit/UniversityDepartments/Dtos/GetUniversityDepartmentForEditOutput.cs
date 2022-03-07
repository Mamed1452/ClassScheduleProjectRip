using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos
{
    public class GetUniversityDepartmentForEditOutput
    {
        public CreateOrEditUniversityDepartmentDto UniversityDepartment { get; set; }

    }
}