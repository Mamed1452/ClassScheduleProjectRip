using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.Tenants.Dashboard.Dto
{
    public class GetSalesSummaryOutput
    {
        public GetSalesSummaryOutput(List<SalesSummaryData> salesSummary)
        {
            SalesSummary = salesSummary;
        }


        public long TotalFactorsNetFee { get; set; }

        public long TotalPaymentPrice { get; set; }

        public int Expenses { get; set; }

        public int Growth { get; set; }

        public List<SalesSummaryData> SalesSummary { get; set; }

    }
}