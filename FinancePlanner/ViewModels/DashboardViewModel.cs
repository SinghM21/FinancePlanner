using FinancePlanner.Helpers;
using FinancePlanner.Models.Dashboard;

namespace FinancePlanner.ViewModels
{
    public class DashboardViewModel
    {
        public int CurrentIncome { get; }
        public int CurrentOutcome { get; }
        public int NetIncome
        {
            get
            {
                return CurrentIncome - CurrentOutcome;
            }
        }
        public decimal[]? YearlyIncome { get; }
        public decimal[]? YearlyOutcome { get; }
        public Dictionary<string, decimal>? ExpensePercentagesByType { get; }

        public DashboardViewModel(int currentIncome, int currentOutcome, Dictionary<string, decimal> expensePercentagesByType)
        {
            this.CurrentIncome = currentIncome;
            this.CurrentOutcome = currentOutcome;
            this.ExpensePercentagesByType = expensePercentagesByType;
            this.YearlyIncome = ProjectionCalculator.CalculateYearlyProjection(currentIncome, null);
            this.YearlyOutcome = ProjectionCalculator.CalculateYearlyProjection(currentOutcome, null);
        }

        public DashboardViewModel(int currentIncome, int currentOutcome, DashboardFormValues dashboardFormValues)
        {
            this.CurrentIncome = currentIncome;
            this.CurrentOutcome = currentOutcome;
            this.YearlyIncome = ProjectionCalculator.CalculateYearlyProjection(currentIncome, ProjectionCalculator.ConvertNumberToDecimal(dashboardFormValues.IncomeProjectionIncreaseNumber));
            this.YearlyOutcome = ProjectionCalculator.CalculateYearlyProjection(currentOutcome, ProjectionCalculator.ConvertNumberToDecimal(dashboardFormValues.OutcomeProjectionIncreaseNumber));
        }
    }
}
