using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Layout;
using Mohajer.ClassScheduleProject.Web.Session;
using Mohajer.ClassScheduleProject.Web.Views;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Views.Shared.Themes.Theme11.Components.AppTheme11Brand
{
    public class AppTheme11BrandViewComponent : ClassScheduleProjectViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme11BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
