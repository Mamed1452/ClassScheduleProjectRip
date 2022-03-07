using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos
{
    public class SemesterDto : EntityDto<long>
    {
        public string SemesterName { get; set; }

        public bool IsActive { get; set; }

        public long AssigningGradeToUniversityMajorId { get; set; }

    }
}