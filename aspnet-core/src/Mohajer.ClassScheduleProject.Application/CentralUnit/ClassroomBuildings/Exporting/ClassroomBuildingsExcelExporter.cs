using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Exporting
{
    public class ClassroomBuildingsExcelExporter : NpoiExcelExporterBase, IClassroomBuildingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClassroomBuildingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClassroomBuildingForViewDto> classroomBuildings)
        {
            return CreateExcelPackage(
                "ClassroomBuildings.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("ClassroomBuildings"));

                    AddHeader(
                        sheet,
                        L("ClassroomBuildingName"),
                        L("NumberOfClassrooms"),
                        L("ClassificationClassroomBuilding")
                        );

                    AddObjects(
                        sheet, 2, classroomBuildings,
                        _ => _.ClassroomBuilding.ClassroomBuildingName,
                        _ => _.ClassroomBuilding.NumberOfClassrooms,
                        _ => _.ClassroomBuilding.ClassificationClassroomBuilding
                        );

                });
        }
    }
}