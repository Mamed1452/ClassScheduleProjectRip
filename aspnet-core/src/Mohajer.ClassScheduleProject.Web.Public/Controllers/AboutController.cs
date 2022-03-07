using Microsoft.AspNetCore.Mvc;
using Mohajer.ClassScheduleProject.Web.Controllers;

namespace Mohajer.ClassScheduleProject.Web.Public.Controllers
{
    public class AboutController : ClassScheduleProjectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}