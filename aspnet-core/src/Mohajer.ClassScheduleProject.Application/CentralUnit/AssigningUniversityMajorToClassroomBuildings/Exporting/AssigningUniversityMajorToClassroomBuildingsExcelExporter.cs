using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Exporting
{
    public class AssigningUniversityMajorToClassroomBuildingsExcelExporter : NpoiExcelExporterBase, IAssigningUniversityMajorToClassroomBuildingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AssigningUniversityMajorToClassroomBuildingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAssigningUniversityMajorToClassroomBuildingForViewDto> assigningUniversityMajorToClassroomBuildings)
        {
            return CreateExcelPackage(
                "AssigningUniversityMajorToClassroomBuildings.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("AssigningUniversityMajorToClassroomBuildings"));

                    AddHeader(
                        sheet,
                        L("MaximumRestrictionsOnUsingClassroomsAtTheSameTime"),
                        (L("UniversityMajor")) + L("UniversityMajorName"),
                        (L("ClassroomBuilding")) + L("ClassroomBuildingName")
                        );

                    AddObjects(
                        sheet, 2, assigningUniversityMajorToClassroomBuildings,
                        _ => _.AssigningUniversityMajorToClassroomBuilding.MaximumRestrictionsOnUsingClassroomsAtTheSameTime,
                        _ => _.UniversityMajorUniversityMajorName,
                        _ => _.ClassroomBuildingClassroomBuildingName
                        );

                });
        }
    }
}