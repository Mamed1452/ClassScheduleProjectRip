using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Exporting
{
    public class ListOfClassScheduleModeSpacesExcelExporter : NpoiExcelExporterBase, IListOfClassScheduleModeSpacesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ListOfClassScheduleModeSpacesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetListOfClassScheduleModeSpaceForViewDto> listOfClassScheduleModeSpaces)
        {
            return CreateExcelPackage(
                "ListOfClassScheduleModeSpaces.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("ListOfClassScheduleModeSpaces"));

                    AddHeader(
                        sheet
                        );

                    AddObjects(
                        sheet, 2, listOfClassScheduleModeSpaces
                        );

                });
        }
    }
}