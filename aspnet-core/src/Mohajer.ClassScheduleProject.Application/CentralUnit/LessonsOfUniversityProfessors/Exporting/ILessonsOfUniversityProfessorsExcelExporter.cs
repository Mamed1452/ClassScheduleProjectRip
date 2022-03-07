using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Exporting
{
    public interface ILessonsOfUniversityProfessorsExcelExporter
    {
        FileDto ExportToFile(List<GetLessonsOfUniversityProfessorForViewDto> lessonsOfUniversityProfessors);
    }
}