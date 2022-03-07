using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Exporting
{
    public class AssigningGradeToUniversityMajorsExcelExporter : NpoiExcelExporterBase, IAssigningGradeToUniversityMajorsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AssigningGradeToUniversityMajorsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAssigningGradeToUniversityMajorForViewDto> assigningGradeToUniversityMajors)
        {
            return CreateExcelPackage(
                "AssigningGradeToUniversityMajors.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("AssigningGradeToUniversityMajors"));

                    AddHeader(
                        sheet,
                        L("NameAssignedGradeToUniversityMajor"),
                        (L("Grade")) + L("GradeName"),
                        (L("UniversityMajor")) + L("UniversityMajorName")
                        );

                    AddObjects(
                        sheet, 2, assigningGradeToUniversityMajors,
                        _ => _.AssigningGradeToUniversityMajor.NameAssignedGradeToUniversityMajor,
                        _ => _.GradeGradeName,
                        _ => _.UniversityMajorUniversityMajorName
                        );

                });
        }
    }
}