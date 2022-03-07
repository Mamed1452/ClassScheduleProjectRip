using Mohajer.ClassScheduleProject.EntityFrameworkCore;

namespace Mohajer.ClassScheduleProject.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly ClassScheduleProjectDbContext _context;

        public InitialHostDbBuilder(ClassScheduleProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
