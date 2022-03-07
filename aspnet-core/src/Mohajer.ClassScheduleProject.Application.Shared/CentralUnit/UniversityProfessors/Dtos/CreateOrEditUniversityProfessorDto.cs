using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos
{
    public class CreateOrEditUniversityProfessorDto : EntityDto<int?>
    {

        [Required]
        [StringLength(UniversityProfessorConsts.MaxUniversityProfessorNameLength, MinimumLength = UniversityProfessorConsts.MinUniversityProfessorNameLength)]
        public string UniversityProfessorName { get; set; }

        public bool IsActive { get; set; }

    }
}