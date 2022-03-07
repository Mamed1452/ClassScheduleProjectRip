using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Microsoft.AspNetCore.Identity;
using Mohajer.ClassScheduleProject.Authorization.Users;
using Mohajer.ClassScheduleProject.Localization;
using Mohajer.ClassScheduleProject.ClassScheduleProjectAddition.Localization;
using Mohajer.ClassScheduleProject.MultiTenancy;

namespace Mohajer.ClassScheduleProject
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ClassScheduleProjectAppServiceBase : ApplicationService
    {
        private readonly IToGeorgianCultureInfoConverter _toGeorgianCultureInfoConverter;

        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected ClassScheduleProjectAppServiceBase()
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

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual User GetCurrentUser()
        {
            return AsyncHelper.RunSync(GetCurrentUserAsync);
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}