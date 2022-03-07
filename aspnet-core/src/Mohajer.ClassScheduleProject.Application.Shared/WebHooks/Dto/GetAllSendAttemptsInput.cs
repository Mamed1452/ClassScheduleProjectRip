using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
