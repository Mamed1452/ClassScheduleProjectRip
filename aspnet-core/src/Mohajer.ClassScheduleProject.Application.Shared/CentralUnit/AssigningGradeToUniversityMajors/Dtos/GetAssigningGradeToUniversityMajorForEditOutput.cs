using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos
{
    public class GetAssigningGradeToUniversityMajorForEditOutput
    {
        public CreateOrEditAssigningGradeToUniversityMajorDto AssigningGradeToUniversityMajor { get; set; }

        public string GradeGradeName { get; set; }

        public string UniversityMajorUniversityMajorName { get; set; }

    }
}