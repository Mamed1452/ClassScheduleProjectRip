using System.Threading.Tasks;
using Abp.Application.Services;

namespace Mohajer.ClassScheduleProject.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
