using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Mohajer.ClassScheduleProject.Authentication.TwoFactor.Google;
using Mohajer.ClassScheduleProject.Authorization;
using Mohajer.ClassScheduleProject.Authorization.Roles;
using Mohajer.ClassScheduleProject.Authorization.Users;
using Mohajer.ClassScheduleProject.Editions;
using Mohajer.ClassScheduleProject.MultiTenancy;

namespace Mohajer.ClassScheduleProject.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>(options =>
                {
                    options.Tokens.ProviderMap[GoogleAuthenticatorProvider.Name] = new TokenProviderDescriptor(typeof(GoogleAuthenticatorProvider));
                })
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
