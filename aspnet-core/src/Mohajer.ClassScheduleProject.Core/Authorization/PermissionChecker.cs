using Abp.Authorization;
using Mohajer.ClassScheduleProject.Authorization.Roles;
using Mohajer.ClassScheduleProject.Authorization.Users;

namespace Mohajer.ClassScheduleProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
