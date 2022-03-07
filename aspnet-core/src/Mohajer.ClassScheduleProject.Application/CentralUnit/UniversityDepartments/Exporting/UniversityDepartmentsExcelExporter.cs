using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Exporting
{
    public class UniversityDepartmentsExcelExporter : NpoiExcelExporterBase, IUniversityDepartmentsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UniversityDepartmentsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUniversityDepartmentForViewDto> universityDepartments)
        {
            return CreateExcelPackage(
                "UniversityDepartments.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("UniversityDepartments"));

                    AddHeader(
                        sheet,
                        L("UniversityDepartmentName")
                        );

                    AddObjects(
                        sheet, 2, universityDepartments,
                        _ => _.UniversityDepartment.UniversityDepartmentName
                        );

                });
        }
    }
}