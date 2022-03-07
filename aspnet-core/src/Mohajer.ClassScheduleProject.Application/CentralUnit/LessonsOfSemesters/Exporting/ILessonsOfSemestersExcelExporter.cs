using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Exporting
{
    public interface ILessonsOfSemestersExcelExporter
    {
        FileDto ExportToFile(List<GetLessonsOfSemesterForViewDto> lessonsOfSemesters);
    }
}