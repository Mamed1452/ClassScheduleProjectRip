using Abp.Application.Services;
using Mohajer.ClassScheduleProject.Tenants.Dashboard.Dto;
using System.Threading.Tasks;

namespace Mohajer.ClassScheduleProject.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();

        GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input);

        Task<GetDailySalesOutput> GetDailySales();

        GetProfitShareOutput GetProfitShare();

        Task<GetSalesSummaryOutput> GetSalesSummary(GetSalesSummaryInput input);

        Task<GetTopStatsOutput> GetTopStats(TenantDashboardYearFilterInputDto filter=null);

        Task<GetContractTypeStateOutput> GetContractTypeState(TenantDashboardYearFilterInputDto filter = null);

        GetGeneralStatsOutput GetGeneralStats();
        Task<GetFilterDatesOutputDto> GetFilterDates();
    }
}
