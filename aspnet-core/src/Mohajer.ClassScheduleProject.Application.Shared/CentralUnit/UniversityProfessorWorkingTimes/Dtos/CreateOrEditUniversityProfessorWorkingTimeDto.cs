using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos
{
    public class CreateOrEditUniversityProfessorWorkingTimeDto : EntityDto<long?>
    {
        public int UniversityProfessorId { get; set; }

        public long WorkTimeInDayId { get; set; }


    }
}