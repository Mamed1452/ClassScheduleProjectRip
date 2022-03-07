using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.Sessions.Dto;

namespace Mohajer.ClassScheduleProject.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}