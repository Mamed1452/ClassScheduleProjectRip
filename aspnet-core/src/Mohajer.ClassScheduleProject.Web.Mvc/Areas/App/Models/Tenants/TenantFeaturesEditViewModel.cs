using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.MultiTenancy;
using Mohajer.ClassScheduleProject.MultiTenancy.Dto;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}