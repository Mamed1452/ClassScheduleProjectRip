using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Exporting
{
    public interface IUniversityMajorsExcelExporter
    {
        FileDto ExportToFile(List<GetUniversityMajorForViewDto> universityMajors);
    }
}