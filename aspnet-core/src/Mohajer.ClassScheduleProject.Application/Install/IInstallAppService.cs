using System.Threading.Tasks;
using Abp.Application.Services;
using Mohajer.ClassScheduleProject.Install.Dto;

namespace Mohajer.ClassScheduleProject.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}