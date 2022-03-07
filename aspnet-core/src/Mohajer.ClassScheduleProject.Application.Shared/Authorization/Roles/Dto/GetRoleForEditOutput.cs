using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Permissions.Dto;

namespace Mohajer.ClassScheduleProject.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}