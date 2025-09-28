using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePlanner.Attributes.Validation;

namespace FinancePlannerTests.Attributes.Validation
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
