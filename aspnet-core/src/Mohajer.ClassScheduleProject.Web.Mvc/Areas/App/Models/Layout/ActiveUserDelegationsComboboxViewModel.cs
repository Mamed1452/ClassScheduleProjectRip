using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Delegation;
using Mohajer.ClassScheduleProject.Authorization.Users.Delegation.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }
        
        public List<UserDelegationDto> UserDelegations { get; set; }
    }
}
