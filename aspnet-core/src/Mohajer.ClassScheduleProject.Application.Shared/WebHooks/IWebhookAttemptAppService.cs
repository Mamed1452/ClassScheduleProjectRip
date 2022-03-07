using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.WebHooks.Dto;

namespace Mohajer.ClassScheduleProject.WebHooks
{
    public interface IWebhookAttemptAppService
    {
        Task<PagedResultDto<GetAllSendAttemptsOutput>> GetAllSendAttempts(GetAllSendAttemptsInput input);

        Task<ListResultDto<GetAllSendAttemptsOfWebhookEventOutput>> GetAllSendAttemptsOfWebhookEvent(GetAllSendAttemptsOfWebhookEventInput input);
    }
}
