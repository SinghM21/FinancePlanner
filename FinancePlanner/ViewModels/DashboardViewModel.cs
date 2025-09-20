using FinancePlanner.Helpers;
using FinancePlanner.Models.Dashboard;

namespace FinancePlanner.ViewModels
{
    public class DashboardViewModel
    {
        public int currentIncome { get; set; }
        public int currentOutcome { get; set; }
        public decimal[]? yearlyIncome { get; set; }
        public decimal[]? yearlyOutcome { get; set; }

        public DashboardViewModel(int currentIncome, int currentOutcome)
        {
            this.currentIncome = currentIncome;
            this.currentOutcome = currentOutcome;
            this.yearlyIncome = ProjectionCalculator.CalculateYearlyProjection(currentIncome, null);
            this.yearlyOutcome = ProjectionCalculator.CalculateYearlyProjection(currentOutcome, null);
        }

        public DashboardViewModel(int currentIncome, int currentOutcome, DashboardFormValues dashboardFormValues)
        {
            this.currentIncome = currentIncome;
            this.currentOutcome = currentOutcome;
            this.yearlyIncome = ProjectionCalculator.CalculateYearlyProjection(currentIncome, ProjectionCalculator.ConvertNumberToDecimal(dashboardFormValues.IncomeProjectionIncreaseNumber));
            this.yearlyOutcome = ProjectionCalculator.CalculateYearlyProjection(currentOutcome, ProjectionCalculator.ConvertNumberToDecimal(dashboardFormValues.OutcomeProjectionIncreaseNumber));
        }
    }
}
