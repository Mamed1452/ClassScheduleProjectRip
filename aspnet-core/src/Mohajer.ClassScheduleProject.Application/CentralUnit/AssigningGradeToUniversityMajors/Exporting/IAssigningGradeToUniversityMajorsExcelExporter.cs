using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Exporting
{
    public interface IAssigningGradeToUniversityMajorsExcelExporter
    {
        FileDto ExportToFile(List<GetAssigningGradeToUniversityMajorForViewDto> assigningGradeToUniversityMajors);
    }
}