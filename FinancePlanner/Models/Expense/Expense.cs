using FinancePlanner.Attributes.Validation;
using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace FinancePlanner.Models.Expense
{
    public class Expense
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        [NotEnumValue(ExpenseType.None, ErrorMessage = "Please select an expense type")]
        public ExpenseType Type { get; set; }

        public int? Quantity { get; set; }

        public int Cost { get; set; }
    }
}
