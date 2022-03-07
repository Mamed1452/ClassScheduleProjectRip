using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos
{
    public class CreateOrEditSemesterDto : EntityDto<long?>
    {

        [Required]
        [StringLength(SemesterConsts.MaxSemesterNameLength, MinimumLength = SemesterConsts.MinSemesterNameLength)]
        public string SemesterName { get; set; }

        public bool IsActive { get; set; }

        public long AssigningGradeToUniversityMajorId { get; set; }

    }
}