namespace FinancePlanner.Helpers
{
    public static class ProjectionCalculator
    {
        public static decimal[] CalculateYearlyProjection(int baseValue, decimal? expectedIncreasePercentage)
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

        public static decimal ConvertNumberToDecimal(int number)
        {
            return (decimal)number / 100;
        }
    }
}
