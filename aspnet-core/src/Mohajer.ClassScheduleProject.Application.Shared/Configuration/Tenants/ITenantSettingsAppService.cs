using System.Threading.Tasks;
using Abp.Application.Services;
using Mohajer.ClassScheduleProject.Configuration.Tenants.Dto;

namespace Mohajer.ClassScheduleProject.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
