﻿using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos
{
    public class GetAllUniversityProfessorWorkingTimesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string UniversityProfessorUniversityProfessorNameFilter { get; set; }

        public string WorkTimeInDayNameWorkTimeInDayFilter { get; set; }

    }
}