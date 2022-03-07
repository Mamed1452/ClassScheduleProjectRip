using System.Collections.Generic;
using Abp;
using Mohajer.ClassScheduleProject.Chat.Dto;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
