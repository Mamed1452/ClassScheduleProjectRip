using Abp.Application.Services.Dto;
using Abp.Webhooks;
using Mohajer.ClassScheduleProject.WebHooks.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
