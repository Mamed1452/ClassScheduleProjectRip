using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mohajer.ClassScheduleProject.Authorization;
using Mohajer.ClassScheduleProject.Caching;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Maintenance;
using Mohajer.ClassScheduleProject.Web.Controllers;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Host_Maintenance)]
    public class MaintenanceController : ClassScheduleProjectControllerBase
    {
        private readonly ICachingAppService _cachingAppService;

        public MaintenanceController(ICachingAppService cachingAppService)
        {
            _cachingAppService = cachingAppService;
        }

        public ActionResult Index()
        {
            var model = new MaintenanceViewModel
            {
                Caches = _cachingAppService.GetAllCaches().Items
            };

            return View(model);
        }
    }
}