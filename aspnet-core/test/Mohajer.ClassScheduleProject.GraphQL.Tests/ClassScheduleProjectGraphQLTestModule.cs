using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Mohajer.ClassScheduleProject.Configure;
using Mohajer.ClassScheduleProject.Startup;
using Mohajer.ClassScheduleProject.Test.Base;

namespace Mohajer.ClassScheduleProject.GraphQL.Tests
{
    [DependsOn(
        typeof(ClassScheduleProjectGraphQLModule),
        typeof(ClassScheduleProjectTestBaseModule))]
    public class ClassScheduleProjectGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectGraphQLTestModule).GetAssembly());
        }
    }
}