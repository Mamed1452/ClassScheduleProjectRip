using System.Threading.Tasks;
using Abp.Application.Services;
using Mohajer.ClassScheduleProject.Configuration.Host.Dto;

namespace Mohajer.ClassScheduleProject.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
