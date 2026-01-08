using FinancePlanner.Attributes.Validation;

namespace FinancePlannerTests.UnitTests.Attributes.Validation
{
    public class NotEnumValueAttributeTests
    {
        [Fact]
        public void IsValid_ShouldReturnFalse_WhenValueIsInvalidEnumValue()
        {
            // Arrange
            var attribute = new NotEnumValueAttribute(DayOfWeek.Monday);

            // Act
            var result = attribute.IsValid(DayOfWeek.Monday);

            // Assert
            Assert.False(result);
        }
    }
}
