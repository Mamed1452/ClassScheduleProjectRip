using Microsoft.AspNetCore.Antiforgery;

namespace Mohajer.ClassScheduleProject.Web.Controllers
{
    public class AntiForgeryController : ClassScheduleProjectControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
