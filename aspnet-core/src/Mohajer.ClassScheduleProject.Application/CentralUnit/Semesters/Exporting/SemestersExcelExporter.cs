using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.Semesters.Exporting
{
    public class SemestersExcelExporter : NpoiExcelExporterBase, ISemestersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SemestersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSemesterForViewDto> semesters)
        {
            return CreateExcelPackage(
                "Semesters.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Semesters"));

                    AddHeader(
                        sheet,
                        L("SemesterName"),
                        L("IsActive"),
                        (L("AssigningGradeToUniversityMajor")) + L("NameAssignedGradeToUniversityMajor")
                        );

                    AddObjects(
                        sheet, 2, semesters,
                        _ => _.Semester.SemesterName,
                        _ => _.Semester.IsActive,
                        _ => _.AssigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor
                        );

                });
        }
    }
}