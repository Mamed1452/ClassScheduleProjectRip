using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Users.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}