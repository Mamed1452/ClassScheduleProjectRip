using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Exporting
{
    public class ClassScheduleResultsExcelExporter : NpoiExcelExporterBase, IClassScheduleResultsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClassScheduleResultsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClassScheduleResultForViewDto> classScheduleResults)
        {
            return CreateExcelPackage(
                "ClassScheduleResults.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("ClassScheduleResults"));

                    AddHeader(
                        sheet,
                        L("WorkTimeInDayId"),
                        L("LessonId"),
                        L("UniversityProfessorId"),
                        L("SemesterId"),
                        L("GradeId"),
                        L("UniversityMajorId"),
                        L("UniversityDepartmentId"),
                        L("ClassroomBuildingId"),
                        (L("ListOfAllCalculatedResult")) + L("NameCalculatedResult"),
                        (L("ClassScheduleModeSpace")) + L("NameClassScheduleModeSpaces")
                        );

                    AddObjects(
                        sheet, 2, classScheduleResults,
                        _ => _.ClassScheduleResult.WorkTimeInDayId,
                        _ => _.ClassScheduleResult.LessonId,
                        _ => _.ClassScheduleResult.UniversityProfessorId,
                        _ => _.ClassScheduleResult.SemesterId,
                        _ => _.ClassScheduleResult.GradeId,
                        _ => _.ClassScheduleResult.UniversityMajorId,
                        _ => _.ClassScheduleResult.UniversityDepartmentId,
                        _ => _.ClassScheduleResult.ClassroomBuildingId,
                        _ => _.ListOfAllCalculatedResultNameCalculatedResult,
                        _ => _.ClassScheduleModeSpaceNameClassScheduleModeSpaces
                        );

                });
        }
    }
}