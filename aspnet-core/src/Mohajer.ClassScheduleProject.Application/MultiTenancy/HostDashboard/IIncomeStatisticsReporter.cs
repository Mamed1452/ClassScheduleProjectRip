using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mohajer.ClassScheduleProject.MultiTenancy.HostDashboard.Dto;

namespace Mohajer.ClassScheduleProject.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}