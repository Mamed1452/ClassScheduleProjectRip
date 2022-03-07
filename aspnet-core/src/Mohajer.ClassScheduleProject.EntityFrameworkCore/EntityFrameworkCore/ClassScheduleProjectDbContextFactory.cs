using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Mohajer.ClassScheduleProject.Configuration;
using Mohajer.ClassScheduleProject.Web;

namespace Mohajer.ClassScheduleProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ClassScheduleProjectDbContextFactory : IDesignTimeDbContextFactory<ClassScheduleProjectDbContext>
    {
        public ClassScheduleProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ClassScheduleProjectDbContext>();
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            ClassScheduleProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ClassScheduleProjectConsts.ConnectionStringName));

            return new ClassScheduleProjectDbContext(builder.Options);
        }
    }
}