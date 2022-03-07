using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}