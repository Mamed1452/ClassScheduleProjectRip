namespace Mohajer.ClassScheduleProject.Tenants.Dashboard.Dto
{
    public class SalesSummaryData
    {
        public string Period { get; set; }
        public long Sales { get; set; }
        public long Payments { get; set; }

        public SalesSummaryData(string period, long sales, long payments)
        {
            Period = period;
            Sales = sales;
            Payments = payments;
        }
    }
}