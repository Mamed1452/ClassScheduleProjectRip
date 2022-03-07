using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.Chat.Dto
{
    public class ChatUserWithMessagesDto : ChatUserDto
    {
        public List<ChatMessageDto> Messages { get; set; }
    }
}