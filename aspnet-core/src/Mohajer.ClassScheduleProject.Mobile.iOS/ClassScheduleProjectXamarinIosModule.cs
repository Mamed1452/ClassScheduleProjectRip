using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Mohajer.ClassScheduleProject
{
    [DependsOn(typeof(ClassScheduleProjectXamarinSharedModule))]
    public class ClassScheduleProjectXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectXamarinIosModule).GetAssembly());
        }
    }
}