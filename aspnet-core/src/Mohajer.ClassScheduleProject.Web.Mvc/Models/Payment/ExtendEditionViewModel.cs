using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Editions.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments;

namespace Mohajer.ClassScheduleProject.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}