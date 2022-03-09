using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Exporting
{
    public class MainDomainsExcelExporter : NpoiExcelExporterBase, IMainDomainsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MainDomainsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMainDomainForViewDto> mainDomains)
        {
            return CreateExcelPackage(
                "MainDomains.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("MainDomains"));

                    AddHeader(
                        sheet,
                        L("MainDomainName"),
                        (L("ListOfMainDomain")) + L("ListOfMainDomainName"),
                        (L("LessonsOfSemester")) + L("LessonsOfSemesterName")
                        );

                    AddObjects(
                        sheet, 2, mainDomains,
                        _ => _.MainDomain.MainDomainName,
                        _ => _.ListOfMainDomainListOfMainDomainName,
                        _ => _.LessonsOfSemesterLessonsOfSemesterName
                        );

                });
        }
    }
}