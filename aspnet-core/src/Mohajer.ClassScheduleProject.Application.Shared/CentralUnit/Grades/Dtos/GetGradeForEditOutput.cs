using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos
{
    public class GetGradeForEditOutput
    {
        public CreateOrEditGradeDto Grade { get; set; }

    }
}