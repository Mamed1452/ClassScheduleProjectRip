using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Mohajer.ClassScheduleProject.Authorization;
using Mohajer.ClassScheduleProject.Tenants.Dashboard.Dto;
using Abp.Auditing;
using Abp.Authorization;
using System.Globalization;
using Abp.Notifications;
using Mohajer.ClassScheduleProject.CentralUnit.Dashbord;

namespace Mohajer.ClassScheduleProject.Tenants.Dashboard
{
    [DisableAuditing]
    [AbpAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class TenantDashboardAppService : ClassScheduleProjectAppServiceBase, ITenantDashboardAppService
    {

        private DateTime? StartDate { get; set; }
        private DateTime? EndDate { get; set; }
        

        private const string DateFormat = "yyyy-MM-dd";

        public TenantDashboardAppService()
        {         
            FillFilterDates();
        }
        public GetMemberActivityOutput GetMemberActivity()
        {
            return new GetMemberActivityOutput
            (
                DashboardRandomDataGenerator.GenerateMemberActivities()
            );
        }

        public GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input)
        {
            var output = new GetDashboardDataOutput
            {
                TotalProfit = DashboardRandomDataGenerator.GetRandomInt(5000, 9000),
                NewFeedbacks = DashboardRandomDataGenerator.GetRandomInt(1000, 5000),
                NewOrders = DashboardRandomDataGenerator.GetRandomInt(100, 900),
                NewUsers = DashboardRandomDataGenerator.GetRandomInt(50, 500),
                SalesSummary = DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod),
                Expenses = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Growth = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Revenue = DashboardRandomDataGenerator.GetRandomInt(1000, 9000),
                TotalSales = DashboardRandomDataGenerator.GetRandomInt(10000, 90000),
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                DailySales = DashboardRandomDataGenerator.GetRandomArray(30, 10, 50),
                ProfitShares = DashboardRandomDataGenerator.GetRandomPercentageArray(3)
            };

            return output;
        }

        public async Task<GetTopStatsOutput> GetTopStats(TenantDashboardYearFilterInputDto filter = null)
        {
            FillFilterDates(filter);

            return new GetTopStatsOutput();
        }
        private DateTime ClaulateFirstPersianYear(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(persianCalendar.GetYear(date), 1, 1, 0, 0, 0, 1);
        }
        private DateTime ClaulateNowPersianDate(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(persianCalendar.GetYear(date), persianCalendar.GetMonth(date), persianCalendar.GetDayOfMonth(date), persianCalendar.GetHour(date), persianCalendar.GetMinute(date), persianCalendar.GetSecond(date), (int)persianCalendar.GetMilliseconds(date));
        }
        public GetProfitShareOutput GetProfitShare()
        {
            return new GetProfitShareOutput
            {
                ProfitShares = DashboardRandomDataGenerator.GetRandomPercentageArray(3)
            };
        }

        public async Task<GetDailySalesOutput> GetDailySales()
        {
            
            return new GetDailySalesOutput
            {
            };
        }
        private async Task<string> CovertToPersianCalendar(DateTime date, CovertToPersianCalendarTypeEnum type = CovertToPersianCalendarTypeEnum.Comlate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string result = "";

            if (type == CovertToPersianCalendarTypeEnum.Comlate)
            {
                result += persianCalendar.ToFourDigitYear(persianCalendar.GetYear(date)).ToString() + "-";
                result += persianCalendar.GetMonth(date).ToString() + "-";
                result += persianCalendar.GetDayOfMonth(date).ToString() + "-";
                return result;
            }
            else if (type == CovertToPersianCalendarTypeEnum.OnlyYear)
            {
                result += persianCalendar.ToFourDigitYear(persianCalendar.GetYear(date)).ToString();
                return result;
            }
            else if (type == CovertToPersianCalendarTypeEnum.OnlyYearMonth)
            {
                result += persianCalendar.ToFourDigitYear(persianCalendar.GetYear(date)).ToString() + "-";
                result += persianCalendar.GetMonth(date).ToString();
                return result;
            }
            else
            {
                return result;
            }


        }
        private async Task<DateTime> toFirstHour(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1);
        }
        private async Task<string> CalWeekOfMonth(DateTime date)
        {
            string result = "";
            PersianCalendar persianCalendar = new PersianCalendar();
            var day = persianCalendar.GetDayOfMonth(date);
            if (day >= 1 && day <= 7)
            {
                result = L("W1");
            }
            else if (day >= 8 && day <= 14)
            {
                result = L("W2");
            }
            else if (day >= 15 && day <= 21)
            {
                result = L("W3");
            }
            else if (day >= 22 && day <= 28)
            {
                result = L("W4");
            }
            else if (day >= 29 && day <= 31)
            {
                result = L("W5");
            }

            return result;
        }
        public async Task<GetSalesSummaryOutput> GetSalesSummary(GetSalesSummaryInput input)
        {
           
            return new GetSalesSummaryOutput(new List<SalesSummaryData>())
            {             
            };
        }

        public async Task<GetContractTypeStateOutput> GetContractTypeState(TenantDashboardYearFilterInputDto filter = null)
        {
            FillFilterDates(filter);       
            return new GetContractTypeStateOutput();

        }
        private void FillFilterDates(TenantDashboardYearFilterInputDto filter = null)
        {
            if (filter != null)
            {
                if (!filter.StartDate.HasValue)
                {
                    this.StartDate = ClaulateFirstPersianYear(DateTime.UtcNow);
                }
                else
                {
                    this.StartDate = filter.StartDate;
                }
                if (!filter.EndDate.HasValue)
                {
                    this.EndDate = ClaulateNowPersianDate(DateTime.UtcNow);
                }
                else
                {
                    this.EndDate = filter.EndDate;
                }
            }
            else
            {
                this.StartDate = ClaulateFirstPersianYear(DateTime.UtcNow);
                this.EndDate = ClaulateNowPersianDate(DateTime.UtcNow);
            }

        }
      
        public GetGeneralStatsOutput GetGeneralStats()
        {
            return new GetGeneralStatsOutput
            {
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100)
            };
        }
        public async Task<GetFilterDatesOutputDto> GetFilterDates()
        {
            GetFilterDatesOutputDto result = new GetFilterDatesOutputDto();
            result.StartDate = this.StartDate;
            result.EndDate = this.EndDate;          
            return result;
        }
    }
}