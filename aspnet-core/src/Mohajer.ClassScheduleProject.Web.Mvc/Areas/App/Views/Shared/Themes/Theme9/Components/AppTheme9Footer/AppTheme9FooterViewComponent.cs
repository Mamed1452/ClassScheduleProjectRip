using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Layout;
using Mohajer.ClassScheduleProject.Web.Session;
using Mohajer.ClassScheduleProject.Web.Views;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Views.Shared.Themes.Theme9.Components.AppTheme9Footer
{
    public class AppTheme9FooterViewComponent : ClassScheduleProjectViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme9FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
