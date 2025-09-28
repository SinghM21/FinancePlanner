using FinancePlanner.Helpers;

namespace FinancePlannerTests.Helpers
{
    public class ProjectionCalculatorTests
    {
        [Fact]
        public void CalculateYearlyProjection_ReturnsCorrectSecondYearValue()
        {
            // Arrange
            int initialValue = 2;
            decimal growthRate = 0.02m;

            // Act
            decimal[] projection = ProjectionCalculator.CalculateYearlyProjection(initialValue, growthRate);

            // Assert
            decimal expectedSecondYearValue = 2.04m;
            Assert.Equal(expectedSecondYearValue, projection[1]);
        }

        [Fact]
        public void ConvertNumberToDecimal_ConvertsIntToDecimal()
        {
            // Arrange
            int input = 20;

            // Act
            decimal result = ProjectionCalculator.ConvertNumberToDecimal(input);

            // Assert
            decimal expected = 0.20m;
            Assert.Equal(expected, result);
        }
    }
}
