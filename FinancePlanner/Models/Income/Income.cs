using FinancePlanner.Attributes.Validation;
using FinancePlanner.Models.Expense;

namespace FinancePlanner.Models.Income
{
    public class Income
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        [NotEnumValue(IncomeType.None, ErrorMessage = "Please select an income type")]
        public IncomeType Type { get; set; }

        public int Amount { get; set; }
    }
}
