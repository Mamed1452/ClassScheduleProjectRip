using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.Authorization.Roles.Dto;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}