using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Permissions.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}