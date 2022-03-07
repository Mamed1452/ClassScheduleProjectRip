using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Auditing.Dto;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
