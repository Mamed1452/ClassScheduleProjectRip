using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization.Permissions.Dto;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}