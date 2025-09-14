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
            this.yearlyIncome = CalculateYearlyProjection(currentIncome, null);
            this.yearlyOutcome = CalculateYearlyProjection(currentOutcome, null);
        }

        public DashboardViewModel(int currentIncome, int currentOutcome, DashboardFormValues dashboardFormValues)
        {
            this.currentIncome = currentIncome;
            this.currentOutcome = currentOutcome;
            this.yearlyIncome = CalculateYearlyProjection(currentIncome, ConvertNumberToDecimal(dashboardFormValues.IncomeProjectionIncreaseNumber));
            this.yearlyOutcome = CalculateYearlyProjection(currentOutcome, ConvertNumberToDecimal(dashboardFormValues.OutcomeProjectionIncreaseNumber));
        }

        private static decimal[] CalculateYearlyProjection(int baseValue, decimal? expectedIncreasePercentage)
        {
            List<decimal> yearlyProjections = new() { baseValue };
            if (expectedIncreasePercentage != null)
            {
                for (int years = 1; years <= 10; years++)
                {
                    decimal nextIncomeIncrease = (decimal)(yearlyProjections.ElementAt(years - 1) * (expectedIncreasePercentage + 1));
                    yearlyProjections.Add(nextIncomeIncrease);
                }
            }
            else
            {
                for (int years = 1; years <= 10; years++)
                {
                    yearlyProjections.Add(baseValue);
                }
            }

            return yearlyProjections.ToArray();
        }

        private static decimal ConvertNumberToDecimal(int number)
        {
            return (decimal)number / 100;
        }

    }
}
