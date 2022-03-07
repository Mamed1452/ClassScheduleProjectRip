using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos
{
    public class GetUniversityProfessorWorkingTimeForEditOutput
    {
        public CreateOrEditUniversityProfessorWorkingTimeDto UniversityProfessorWorkingTime { get; set; }

        public string UniversityProfessorUniversityProfessorName { get; set; }

        public string WorkTimeInDayNameWorkTimeInDay { get; set; }

    }
}