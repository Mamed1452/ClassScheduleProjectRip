using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Mohajer.ClassScheduleProject.DataExporting.Excel.NPOI;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Exporting
{
    public class WorkTimeInDaysExcelExporter : NpoiExcelExporterBase, IWorkTimeInDaysExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public WorkTimeInDaysExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetWorkTimeInDayForViewDto> workTimeInDays)
        {
            return CreateExcelPackage(
                "WorkTimeInDays.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("WorkTimeInDays"));

                    AddHeader(
                        sheet,
                        L("NameWorkTimeInDay"),
                        L("DayOfWeek"),
                        L("startTime"),
                        L("EndTime"),
                        L("WhatTimeOfDay")
                        );

                    AddObjects(
                        sheet, 2, workTimeInDays,
                        _ => _.WorkTimeInDay.NameWorkTimeInDay,
                        _ => _.WorkTimeInDay.DayOfWeek,
                        _ => _.WorkTimeInDay.startTime,
                        _ => _.WorkTimeInDay.EndTime,
                        _ => _.WorkTimeInDay.WhatTimeOfDay
                        );

                });
        }
    }
}