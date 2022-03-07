using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos
{
    public class CreateOrEditUniversityMajorDto : EntityDto<int?>
    {

        [Required]
        [StringLength(UniversityMajorConsts.MaxUniversityMajorNameLength, MinimumLength = UniversityMajorConsts.MinUniversityMajorNameLength)]
        public string UniversityMajorName { get; set; }

        public UniversityMajorTypeEnum UniversityMajorType { get; set; }

        public int UniversityDepartmentId { get; set; }

    }
}