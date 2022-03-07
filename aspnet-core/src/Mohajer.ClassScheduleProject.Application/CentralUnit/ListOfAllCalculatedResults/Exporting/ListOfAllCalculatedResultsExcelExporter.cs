using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Exporting
{
    public class ListOfAllCalculatedResultsExcelExporter : NpoiExcelExporterBase, IListOfAllCalculatedResultsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ListOfAllCalculatedResultsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetListOfAllCalculatedResultForViewDto> listOfAllCalculatedResults)
        {
            return CreateExcelPackage(
                "ListOfAllCalculatedResults.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("ListOfAllCalculatedResults"));

                    AddHeader(
                        sheet,
                        L("NameCalculatedResult"),
                        L("Price")
                        );

                    AddObjects(
                        sheet, 2, listOfAllCalculatedResults,
                        _ => _.ListOfAllCalculatedResult.NameCalculatedResult,
                        _ => _.ListOfAllCalculatedResult.Price
                        );

                });
        }
    }
}