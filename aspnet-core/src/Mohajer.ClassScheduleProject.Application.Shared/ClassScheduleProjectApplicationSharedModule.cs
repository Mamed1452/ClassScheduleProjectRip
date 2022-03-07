using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Mohajer.ClassScheduleProject
{
    [DependsOn(typeof(ClassScheduleProjectCoreSharedModule))]
    public class ClassScheduleProjectApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectApplicationSharedModule).GetAssembly());
        }
    }
}