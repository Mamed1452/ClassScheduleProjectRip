using System.Threading.Tasks;
using Abp.Application.Services;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments.PayPal.Dto;

namespace Mohajer.ClassScheduleProject.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
