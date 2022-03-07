using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.MultiTenancy.Payments
{
    public interface IPaymentGatewayStore
    {
        List<PaymentGatewayModel> GetActiveGateways();
    }
}
