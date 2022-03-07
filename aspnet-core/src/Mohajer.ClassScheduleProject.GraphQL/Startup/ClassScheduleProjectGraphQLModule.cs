using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Mohajer.ClassScheduleProject.Startup
{
    [DependsOn(typeof(ClassScheduleProjectCoreModule))]
    public class ClassScheduleProjectGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}