using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Editions;
using Mohajer.ClassScheduleProject.Editions.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments.Dto;

namespace Mohajer.ClassScheduleProject.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
