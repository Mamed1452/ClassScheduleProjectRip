using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Mohajer.ClassScheduleProject.Configuration;

namespace Mohajer.ClassScheduleProject.Test.Base.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(ClassScheduleProjectTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
