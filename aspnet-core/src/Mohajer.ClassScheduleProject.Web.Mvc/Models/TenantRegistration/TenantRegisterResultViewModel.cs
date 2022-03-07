using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.MultiTenancy.Dto;

namespace Mohajer.ClassScheduleProject.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}