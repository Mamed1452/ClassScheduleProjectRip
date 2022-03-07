using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.Notifications.Dto
{
    public class GetNotificationSettingsOutput
    {
        public bool ReceiveNotifications { get; set; }

        public List<NotificationSubscriptionWithDisplayNameDto> Notifications { get; set; }
    }
}