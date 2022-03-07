using System;
using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.Tenants.Dashboard.Dto
{
    public class GetDailySalesOutput
    {
        public List<DailySalesDto> DailySalesWithDate { get; set; }

    }
    public class DailySalesDto
    {
        public long DailySale { get; set; }
        public DateTime DailySaleTime { get; set; }
    }
}