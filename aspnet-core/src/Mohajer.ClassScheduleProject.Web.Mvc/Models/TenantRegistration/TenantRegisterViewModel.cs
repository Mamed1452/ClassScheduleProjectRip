using Mohajer.ClassScheduleProject.Editions;
using Mohajer.ClassScheduleProject.Editions.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments;
using Mohajer.ClassScheduleProject.Security;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments.Dto;

namespace Mohajer.ClassScheduleProject.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
