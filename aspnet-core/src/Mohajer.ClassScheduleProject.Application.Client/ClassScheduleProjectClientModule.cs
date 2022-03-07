using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Mohajer.ClassScheduleProject
{
    public class ClassScheduleProjectClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectClientModule).GetAssembly());
        }
    }
}
