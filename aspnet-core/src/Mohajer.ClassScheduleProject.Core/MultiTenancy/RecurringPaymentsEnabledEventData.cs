using Abp.Events.Bus;

namespace Mohajer.ClassScheduleProject.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}