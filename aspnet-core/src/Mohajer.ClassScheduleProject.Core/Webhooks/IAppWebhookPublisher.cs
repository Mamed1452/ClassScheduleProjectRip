using System.Threading.Tasks;

namespace Mohajer.ClassScheduleProject.Webhooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
