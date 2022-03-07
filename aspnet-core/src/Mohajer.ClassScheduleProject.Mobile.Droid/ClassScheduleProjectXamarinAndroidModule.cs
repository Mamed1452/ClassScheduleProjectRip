using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Mohajer.ClassScheduleProject
{
    [DependsOn(typeof(ClassScheduleProjectXamarinSharedModule))]
    public class ClassScheduleProjectXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectXamarinAndroidModule).GetAssembly());
        }
    }
}