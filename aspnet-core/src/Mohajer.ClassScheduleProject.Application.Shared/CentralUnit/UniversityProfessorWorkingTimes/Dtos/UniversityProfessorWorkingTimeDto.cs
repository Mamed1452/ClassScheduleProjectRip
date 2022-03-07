using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos
{
    public class UniversityProfessorWorkingTimeDto : EntityDto<long>
    {

        public int UniversityProfessorId { get; set; }

        public long WorkTimeInDayId { get; set; }

    }
}