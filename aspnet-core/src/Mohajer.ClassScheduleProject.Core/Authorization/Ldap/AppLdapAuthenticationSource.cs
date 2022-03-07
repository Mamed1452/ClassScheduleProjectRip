using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Mohajer.ClassScheduleProject.Authorization.Users;
using Mohajer.ClassScheduleProject.MultiTenancy;

namespace Mohajer.ClassScheduleProject.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}