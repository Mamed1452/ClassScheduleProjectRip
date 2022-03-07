using Microsoft.Extensions.DependencyInjection;
using Mohajer.ClassScheduleProject.HealthChecks;

namespace Mohajer.ClassScheduleProject.Web.HealthCheck
{
    public static class AbpZeroHealthCheck
    {
        public static IHealthChecksBuilder AddAbpZeroHealthCheck(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            builder.AddCheck<ClassScheduleProjectDbContextHealthCheck>("Database Connection");
            builder.AddCheck<ClassScheduleProjectDbContextUsersHealthCheck>("Database Connection with user check");
            builder.AddCheck<CacheHealthCheck>("Cache");

            // add your custom health checks here
            // builder.AddCheck<MyCustomHealthCheck>("my health check");

            return builder;
        }
    }
}
