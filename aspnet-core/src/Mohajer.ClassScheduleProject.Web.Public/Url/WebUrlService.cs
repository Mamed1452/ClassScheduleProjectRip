using Abp.Dependency;
using Mohajer.ClassScheduleProject.Configuration;
using Mohajer.ClassScheduleProject.Url;
using Mohajer.ClassScheduleProject.Web.Url;

namespace Mohajer.ClassScheduleProject.Web.Public.Url
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
    {
        public WebUrlService(
            IAppConfigurationAccessor appConfigurationAccessor) :
            base(appConfigurationAccessor)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:WebSiteRootAddress";

        public override string ServerRootAddressFormatKey => "App:AdminWebSiteRootAddress";
    }
}