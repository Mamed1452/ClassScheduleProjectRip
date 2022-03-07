using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Mohajer.ClassScheduleProject.EntityFrameworkCore
{
    public static class ClassScheduleProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ClassScheduleProjectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ClassScheduleProjectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}