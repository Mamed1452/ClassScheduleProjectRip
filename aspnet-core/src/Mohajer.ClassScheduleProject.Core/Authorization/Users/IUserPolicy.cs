using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Mohajer.ClassScheduleProject.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
