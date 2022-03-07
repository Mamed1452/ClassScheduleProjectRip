using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos
{
    public class GetSemesterForEditOutput
    {
        public CreateOrEditSemesterDto Semester { get; set; }

        public string AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor { get; set; }

    }
}