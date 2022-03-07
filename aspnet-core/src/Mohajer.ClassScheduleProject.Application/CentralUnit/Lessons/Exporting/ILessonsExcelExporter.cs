using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons.Exporting
{
    public interface ILessonsExcelExporter
    {
        FileDto ExportToFile(List<GetLessonForViewDto> lessons);
    }
}