using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.Authorization.Users;
using Mohajer.ClassScheduleProject.Authorization.Users.Dto;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}