using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos
{
    public class GetAllUniversityProfessorsForExcelInput
    {
        public string Filter { get; set; }

        public string UniversityProfessorNameFilter { get; set; }

        public int? IsActiveFilter { get; set; }

    }
}