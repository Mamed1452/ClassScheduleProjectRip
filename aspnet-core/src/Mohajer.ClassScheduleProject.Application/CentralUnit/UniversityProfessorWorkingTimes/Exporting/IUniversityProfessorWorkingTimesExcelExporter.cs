using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Exporting
{
    public interface IUniversityProfessorWorkingTimesExcelExporter
    {
        FileDto ExportToFile(List<GetUniversityProfessorWorkingTimeForViewDto> universityProfessorWorkingTimes);
    }
}