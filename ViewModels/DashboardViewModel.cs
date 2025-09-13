namespace FinancePlanner.ViewModels
{
    public class DashboardViewModel
    {
        public int monthlyIncome { get; set; }
        public int monthlyOutcome { get; set; }
        public decimal[]? yearlyIncome { get; set; }
        public decimal[]? yearlyOutcome { get; set; }
    }
}
