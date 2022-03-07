using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos
{
    public class GetUniversityMajorForEditOutput
    {
        public CreateOrEditUniversityMajorDto UniversityMajor { get; set; }

        public string UniversityDepartmentUniversityDepartmentName { get; set; }

    }
}