using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Mohajer.ClassScheduleProject
{
    [DependsOn(typeof(ClassScheduleProjectClientModule), typeof(AbpAutoMapperModule))]
    public class ClassScheduleProjectXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectXamarinSharedModule).GetAssembly());
        }
    }
}