using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.MultiTenancy.Dto;

namespace Mohajer.ClassScheduleProject.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
