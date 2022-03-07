using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades.Exporting
{
    public class GradesExcelExporter : NpoiExcelExporterBase, IGradesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public GradesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetGradeForViewDto> grades)
        {
            return CreateExcelPackage(
                "Grades.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Grades"));

                    AddHeader(
                        sheet,
                        L("GradeName")
                        );

                    AddObjects(
                        sheet, 2, grades,
                        _ => _.Grade.GradeName
                        );

                });
        }
    }
}