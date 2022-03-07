using System.Threading.Tasks;
using Mohajer.ClassScheduleProject.ApiClient.Models;

namespace Mohajer.ClassScheduleProject.Services.Account
{
    public interface IAccountService
    {
        AbpAuthenticateModel AbpAuthenticateModel { get; set; }
        
        AbpAuthenticateResultModel AuthenticateResultModel { get; set; }
        
        Task LoginUserAsync();

        Task LogoutAsync();
    }
}
