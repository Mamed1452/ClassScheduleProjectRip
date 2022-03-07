using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Exporting
{
    public class ClassScheduleModeSpacesExcelExporter : NpoiExcelExporterBase, IClassScheduleModeSpacesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClassScheduleModeSpacesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClassScheduleModeSpaceForViewDto> classScheduleModeSpaces)
        {
            return CreateExcelPackage(
                "ClassScheduleModeSpaces.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("ClassScheduleModeSpaces"));

                    AddHeader(
                        sheet,
                        L("NameClassScheduleModeSpaces"),
                        L("IsLock"),
                        (L("UniversityProfessor")) + L("UniversityProfessorName"),
                        (L("WorkTimeInDay")) + L("NameWorkTimeInDay"),
                        (L("Lesson")) + L("NameLesson")
                        );

                    AddObjects(
                        sheet, 2, classScheduleModeSpaces,
                        _ => _.ClassScheduleModeSpace.NameClassScheduleModeSpaces,
                        _ => _.ClassScheduleModeSpace.IsLock,
                        _ => _.UniversityProfessorUniversityProfessorName,
                        _ => _.WorkTimeInDayNameWorkTimeInDay,
                        _ => _.LessonNameLesson
                        );

                });
        }
    }
}