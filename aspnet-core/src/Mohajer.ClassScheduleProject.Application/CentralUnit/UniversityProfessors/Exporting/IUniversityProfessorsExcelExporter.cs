using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Exporting
{
    public interface IUniversityProfessorsExcelExporter
    {
        FileDto ExportToFile(List<GetUniversityProfessorForViewDto> universityProfessors);
    }
}