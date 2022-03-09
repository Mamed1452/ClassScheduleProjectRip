using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Exporting
{
    public class ListOfMainDomainsExcelExporter : NpoiExcelExporterBase, IListOfMainDomainsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ListOfMainDomainsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetListOfMainDomainForViewDto> listOfMainDomains)
        {
            return CreateExcelPackage(
                "ListOfMainDomains.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("ListOfMainDomains"));

                    AddHeader(
                        sheet,
                        L("ListOfMainDomainName")
                        );

                    AddObjects(
                        sheet, 2, listOfMainDomains,
                        _ => _.ListOfMainDomain.ListOfMainDomainName
                        );

                });
        }
    }
}