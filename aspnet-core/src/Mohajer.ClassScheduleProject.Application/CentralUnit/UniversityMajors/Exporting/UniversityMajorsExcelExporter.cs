using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Exporting
{
    public class UniversityMajorsExcelExporter : NpoiExcelExporterBase, IUniversityMajorsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UniversityMajorsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUniversityMajorForViewDto> universityMajors)
        {
            return CreateExcelPackage(
                "UniversityMajors.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("UniversityMajors"));

                    AddHeader(
                        sheet,
                        L("UniversityMajorName"),
                        L("UniversityMajorType"),
                        (L("UniversityDepartment")) + L("UniversityDepartmentName")
                        );

                    AddObjects(
                        sheet, 2, universityMajors,
                        _ => _.UniversityMajor.UniversityMajorName,
                        _ => _.UniversityMajor.UniversityMajorType,
                        _ => _.UniversityDepartmentUniversityDepartmentName
                        );

                });
        }
    }
}