using Microsoft.Extensions.Configuration;

namespace Mohajer.ClassScheduleProject.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
