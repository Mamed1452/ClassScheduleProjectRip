using System.Threading.Tasks;

namespace Mohajer.ClassScheduleProject.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
