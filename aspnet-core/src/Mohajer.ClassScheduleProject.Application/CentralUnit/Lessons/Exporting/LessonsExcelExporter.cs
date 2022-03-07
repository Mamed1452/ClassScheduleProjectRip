using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons.Exporting
{
    public class LessonsExcelExporter : NpoiExcelExporterBase, ILessonsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LessonsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLessonForViewDto> lessons)
        {
            return CreateExcelPackage(
                "Lessons.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Lessons"));

                    AddHeader(
                        sheet,
                        L("NameLesson"),
                        L("HoursPerWeek"),
                        L("LessonType"),
                        L("ClassificationLesson"),
                        L("NumberOfUnits"),
                        L("NumberOfGroups"),
                        L("IsActive"),
                        (L("ClassroomBuilding")) + L("ClassroomBuildingName")
                        );

                    AddObjects(
                        sheet, 2, lessons,
                        _ => _.Lesson.NameLesson,
                        _ => _.Lesson.HoursPerWeek,
                        _ => _.Lesson.LessonType,
                        _ => _.Lesson.ClassificationLesson,
                        _ => _.Lesson.NumberOfUnits,
                        _ => _.Lesson.NumberOfGroups,
                        _ => _.Lesson.IsActive,
                        _ => _.ClassroomBuildingClassroomBuildingName
                        );

                });
        }
    }
}