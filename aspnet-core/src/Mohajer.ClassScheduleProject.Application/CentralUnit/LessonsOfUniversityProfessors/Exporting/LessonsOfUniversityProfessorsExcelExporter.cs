using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Exporting
{
    public class LessonsOfUniversityProfessorsExcelExporter : NpoiExcelExporterBase, ILessonsOfUniversityProfessorsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LessonsOfUniversityProfessorsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLessonsOfUniversityProfessorForViewDto> lessonsOfUniversityProfessors)
        {
            return CreateExcelPackage(
                "LessonsOfUniversityProfessors.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("LessonsOfUniversityProfessors"));

                    AddHeader(
                        sheet,
                        L("IsActive"),
                        (L("Lesson")) + L("NameLesson"),
                        (L("UniversityProfessor")) + L("UniversityProfessorName")
                        );

                    AddObjects(
                        sheet, 2, lessonsOfUniversityProfessors,
                        _ => _.LessonsOfUniversityProfessor.IsActive,
                        _ => _.LessonNameLesson,
                        _ => _.UniversityProfessorUniversityProfessorName
                        );

                });
        }
    }
}