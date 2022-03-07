using System;
using System.Collections.Generic;
using System.Text;

namespace Mohajer.ClassScheduleProject.Tenants.Dashboard.Dto
{
  public  class TenantDashboardYearFilterInputDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public DateTime? StartContractExChangeDate { get; set; }
        public DateTime? EndContractExChangeDate { get; set; }
        public DateTime? StartContractSupportDate { get; set; }
        public DateTime? EndContractSupportDate { get; set; }
        public DateTime? StartContractEndDate { get; set; }
        public DateTime? EndContractEndDate { get; set; }
    }
}
