using Mohajer.ClassScheduleProject.MultiTenancy.Payments;

namespace Mohajer.ClassScheduleProject.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}