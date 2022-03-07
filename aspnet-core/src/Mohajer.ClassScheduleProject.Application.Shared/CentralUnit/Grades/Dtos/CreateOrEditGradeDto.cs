using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos
{
    public class CreateOrEditGradeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(GradeConsts.MaxGradeNameLength, MinimumLength = GradeConsts.MinGradeNameLength)]
        public string GradeName { get; set; }

    }
}