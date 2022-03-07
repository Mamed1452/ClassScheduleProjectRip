using System.Threading.Tasks;
using Abp.Webhooks;

namespace Mohajer.ClassScheduleProject.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
