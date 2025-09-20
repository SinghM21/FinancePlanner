using FinancePlanner.Helpers;

namespace FinancePlannerTests.Helpers
{
    public class ProjectionCalculatorTests
    {
        [Fact]
        public void CalculateYearlyProjectionTest()
        {
            decimal[] result = ProjectionCalculator.CalculateYearlyProjection(2, (decimal)0.02);
            Assert.Equal((decimal)2.04, result[1]);
        }

        [Fact]
        public void ConvertNumberToDecimalTest()
        {
            decimal result = ProjectionCalculator.ConvertNumberToDecimal(20);
            Assert.Equal((decimal)0.20, result);
        }
    }
}
