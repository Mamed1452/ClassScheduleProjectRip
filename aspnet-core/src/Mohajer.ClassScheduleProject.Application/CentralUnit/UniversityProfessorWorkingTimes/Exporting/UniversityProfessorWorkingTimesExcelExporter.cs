using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Exporting
{
    public class UniversityProfessorWorkingTimesExcelExporter : NpoiExcelExporterBase, IUniversityProfessorWorkingTimesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UniversityProfessorWorkingTimesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUniversityProfessorWorkingTimeForViewDto> universityProfessorWorkingTimes)
        {
            return CreateExcelPackage(
                "UniversityProfessorWorkingTimes.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("UniversityProfessorWorkingTimes"));

                    AddHeader(
                        sheet,
                        (L("UniversityProfessor")) + L("UniversityProfessorName"),
                        (L("WorkTimeInDay")) + L("NameWorkTimeInDay")
                        );

                    AddObjects(
                        sheet, 2, universityProfessorWorkingTimes,
                        _ => _.UniversityProfessorUniversityProfessorName,
                        _ => _.WorkTimeInDayNameWorkTimeInDay
                        );

                });
        }
    }
}