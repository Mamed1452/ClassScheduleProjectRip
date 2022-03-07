using System;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.IdentityFramework;
using Mohajer.ClassScheduleProject.Localization;
using Mohajer.ClassScheduleProject.ClassScheduleProjectAddition.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Mohajer.ClassScheduleProject.Web.Controllers
{
    public abstract class ClassScheduleProjectControllerBase : AbpController
    {
        private readonly IToGeorgianCultureInfoConverter _toGeorgianCultureInfoConverter;

        protected ClassScheduleProjectControllerBase()
        {
            LocalizationSourceName = ClassScheduleProjectConsts.LocalizationSourceName;

            //TODO: duplicate code, check if you can remove the one in here
            #if DEBUG
            if (!IocManager.Instance.IsRegistered(typeof(IToGeorgianCultureInfoConverter)))
                _toGeorgianCultureInfoConverter = new ToGeorgianCultureInfoConverter();
            else
            {
                _toGeorgianCultureInfoConverter = IocManager.Instance.Resolve<IToGeorgianCultureInfoConverter>();
            }

            #else
                _toGeorgianCultureInfoConverter = IocManager.Instance.Resolve<IToGeorgianCultureInfoConverter>();
            #endif

            _toGeorgianCultureInfoConverter.SetTargetCalenderOnCurrentThread();
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected void SetTenantIdCookie(int? tenantId)
        {
            var multiTenancyConfig = HttpContext.RequestServices.GetRequiredService<IMultiTenancyConfig>();
            Response.Cookies.Append(
                multiTenancyConfig.TenantIdResolveKey,
                tenantId?.ToString() ?? string.Empty,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddYears(5),
                    Path = "/"
                }
            );
        }
    }
}