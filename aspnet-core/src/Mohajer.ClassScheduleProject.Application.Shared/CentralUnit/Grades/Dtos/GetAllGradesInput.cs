using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos
{
    public class GetAllGradesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string GradeNameFilter { get; set; }

    }
}