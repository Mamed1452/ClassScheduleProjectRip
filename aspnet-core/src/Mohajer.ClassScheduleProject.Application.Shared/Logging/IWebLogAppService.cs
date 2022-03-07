using Abp.Application.Services;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.Logging.Dto;

namespace Mohajer.ClassScheduleProject.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
