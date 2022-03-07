using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.Web.Common
{
    public static class WebConsts
    {
        public const string SwaggerUiEndPoint = "/swagger";
        public const string HangfireDashboardEndPoint = "/hangfire";

        public static bool SwaggerUiEnabled = true;
        public static bool HangfireDashboardEnabled = true;
        public static bool DistributedSqlServerCache = false;

        public static List<string> ReCaptchaIgnoreWhiteList = new List<string>
        {
            ClassScheduleProjectConsts.AbpApiClientUserAgent
        };

        public static class GraphQL
        {
            public const string PlaygroundEndPoint = "/ui/playground";
            public const string EndPoint = "/graphql";

            public static bool PlaygroundEnabled = true;
            public static bool Enabled = true;
        }
    }
}
