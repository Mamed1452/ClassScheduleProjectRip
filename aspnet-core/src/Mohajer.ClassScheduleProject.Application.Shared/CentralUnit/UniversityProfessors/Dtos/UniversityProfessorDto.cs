using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos
{
    public class UniversityProfessorDto : EntityDto
    {
        public string UniversityProfessorName { get; set; }

        public bool IsActive { get; set; }

    }
}