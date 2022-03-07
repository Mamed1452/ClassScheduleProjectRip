using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos
{
    public class AssigningGradeToUniversityMajorDto : EntityDto<long>
    {
        public string NameAssignedGradeToUniversityMajor { get; set; }

        public int GradeId { get; set; }

        public int UniversityMajorId { get; set; }

    }
}