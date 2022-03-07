using System.Threading.Tasks;
using Abp.Application.Services;
using Mohajer.ClassScheduleProject.Editions.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.Dto;

namespace Mohajer.ClassScheduleProject.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}