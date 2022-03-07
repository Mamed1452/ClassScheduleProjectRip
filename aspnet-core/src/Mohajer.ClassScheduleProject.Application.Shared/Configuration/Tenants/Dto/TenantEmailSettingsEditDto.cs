using Abp.Auditing;
using Mohajer.ClassScheduleProject.Configuration.Dto;

namespace Mohajer.ClassScheduleProject.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}