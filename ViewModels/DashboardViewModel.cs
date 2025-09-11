namespace FinancePlanner.ViewModels
{
    public class DashboardViewModel
    {
        public int monthlyIncome { get; set; }
        public int monthlyOutcome { get; set; }
        public int[]? yearlyIncome { get; set; }
        public int[]? yearlyOutcome { get; set; }
    }
}
