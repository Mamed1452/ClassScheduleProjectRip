using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos
{
    public class GetUniversityProfessorForEditOutput
    {
        public CreateOrEditUniversityProfessorDto UniversityProfessor { get; set; }

    }
}