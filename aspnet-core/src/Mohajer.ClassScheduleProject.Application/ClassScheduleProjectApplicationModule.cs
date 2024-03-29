﻿using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Mohajer.ClassScheduleProject.Authorization;

namespace Mohajer.ClassScheduleProject
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(ClassScheduleProjectApplicationSharedModule),
        typeof(ClassScheduleProjectCoreModule)
        )]
    public class ClassScheduleProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ClassScheduleProjectApplicationModule).GetAssembly());
        }
    }
}