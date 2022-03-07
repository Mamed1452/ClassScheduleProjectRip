using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Permissions.Dto;

namespace Mohajer.ClassScheduleProject.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}