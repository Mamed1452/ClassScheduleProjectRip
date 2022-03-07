using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;

using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos
{
    public class UniversityMajorDto : EntityDto
    {
        public string UniversityMajorName { get; set; }

        public UniversityMajorTypeEnum UniversityMajorType { get; set; }

        public int UniversityDepartmentId { get; set; }

    }
}