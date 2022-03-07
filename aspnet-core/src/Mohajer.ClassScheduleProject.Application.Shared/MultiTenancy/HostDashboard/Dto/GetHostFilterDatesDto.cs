using System;
using System.Collections.Generic;
using System.Text;

namespace Mohajer.ClassScheduleProject.MultiTenancy.HostDashboard.Dto
{
    public class GetHostFilterDatesDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
