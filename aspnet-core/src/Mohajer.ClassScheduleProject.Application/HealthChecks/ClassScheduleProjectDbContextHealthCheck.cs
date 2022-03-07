using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mohajer.ClassScheduleProject.EntityFrameworkCore;

namespace Mohajer.ClassScheduleProject.HealthChecks
{
    public class ClassScheduleProjectDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public ClassScheduleProjectDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("ClassScheduleProjectDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("ClassScheduleProjectDbContext could not connect to database"));
        }
    }
}
