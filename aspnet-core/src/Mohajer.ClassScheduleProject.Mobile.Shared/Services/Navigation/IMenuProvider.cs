using System.Collections.Generic;
using MvvmHelpers;
using Mohajer.ClassScheduleProject.Models.NavigationMenu;

namespace Mohajer.ClassScheduleProject.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}