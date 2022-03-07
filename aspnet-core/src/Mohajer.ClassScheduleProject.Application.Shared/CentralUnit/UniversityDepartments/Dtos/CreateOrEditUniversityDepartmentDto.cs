using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos
{
    public class CreateOrEditUniversityDepartmentDto : EntityDto<int?>
    {

        [Required]
        [StringLength(UniversityDepartmentConsts.MaxUniversityDepartmentNameLength, MinimumLength = UniversityDepartmentConsts.MinUniversityDepartmentNameLength)]
        public string UniversityDepartmentName { get; set; }

    }
}