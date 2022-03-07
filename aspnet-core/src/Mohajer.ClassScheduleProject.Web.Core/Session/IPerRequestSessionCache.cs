using System.Threading.Tasks;
using Mohajer.ClassScheduleProject.Sessions.Dto;

namespace Mohajer.ClassScheduleProject.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
