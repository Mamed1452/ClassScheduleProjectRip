using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos
{
    public class CreateOrEditAssigningGradeToUniversityMajorDto : EntityDto<long?>
    {

        [Required]
        [StringLength(AssigningGradeToUniversityMajorConsts.MaxNameAssignedGradeToUniversityMajorLength, MinimumLength = AssigningGradeToUniversityMajorConsts.MinNameAssignedGradeToUniversityMajorLength)]
        public string NameAssignedGradeToUniversityMajor { get; set; }

        public int GradeId { get; set; }

        public int UniversityMajorId { get; set; }

    }
}