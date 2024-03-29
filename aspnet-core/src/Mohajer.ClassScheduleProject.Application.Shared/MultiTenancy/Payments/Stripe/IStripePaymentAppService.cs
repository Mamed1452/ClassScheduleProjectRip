﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments.Stripe.Dto;

namespace Mohajer.ClassScheduleProject.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}