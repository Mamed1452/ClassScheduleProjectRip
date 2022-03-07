using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}