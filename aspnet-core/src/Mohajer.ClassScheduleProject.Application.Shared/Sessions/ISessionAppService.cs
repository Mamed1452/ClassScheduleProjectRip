using System.Threading.Tasks;
using Abp.Application.Services;
using Mohajer.ClassScheduleProject.Sessions.Dto;

namespace Mohajer.ClassScheduleProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
