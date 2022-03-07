using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Exporting
{
    public class LessonsOfSemestersExcelExporter : NpoiExcelExporterBase, ILessonsOfSemestersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LessonsOfSemestersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLessonsOfSemesterForViewDto> lessonsOfSemesters)
        {
            return CreateExcelPackage(
                "LessonsOfSemesters.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("LessonsOfSemesters"));

                    AddHeader(
                        sheet,
                        L("LessonOfSemesterType"),
                        L("NumberOfClassesToBeFormed"),
                        L("IsActive"),
                        (L("Lesson")) + L("NameLesson"),
                        (L("Semester")) + L("SemesterName")
                        );

                    AddObjects(
                        sheet, 2, lessonsOfSemesters,
                        _ => _.LessonsOfSemester.LessonOfSemesterType,
                        _ => _.LessonsOfSemester.NumberOfClassesToBeFormed,
                        _ => _.LessonsOfSemester.IsActive,
                        _ => _.LessonNameLesson,
                        _ => _.SemesterSemesterName
                        );

                });
        }
    }
}