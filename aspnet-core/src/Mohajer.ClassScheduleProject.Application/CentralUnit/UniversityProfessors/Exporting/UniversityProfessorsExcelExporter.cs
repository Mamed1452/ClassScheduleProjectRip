using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Exporting
{
    public class UniversityProfessorsExcelExporter : NpoiExcelExporterBase, IUniversityProfessorsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UniversityProfessorsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUniversityProfessorForViewDto> universityProfessors)
        {
            return CreateExcelPackage(
                "UniversityProfessors.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("UniversityProfessors"));

                    AddHeader(
                        sheet,
                        L("UniversityProfessorName"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, universityProfessors,
                        _ => _.UniversityProfessor.UniversityProfessorName,
                        _ => _.UniversityProfessor.IsActive
                        );

                });
        }
    }
}