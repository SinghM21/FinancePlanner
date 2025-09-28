using FinancePlanner.Attributes.Validation;
using FinancePlanner.Models.Income;

namespace FinancePlanner.Models.Investment
{
    public class Investment
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        [NotEnumValue(InvestmentType.None, ErrorMessage = "Please select an investment type")]
        public InvestmentType Type { get; set; }

        public int? Quantity { get; set; }

        public int Cost { get; set; }

        public required bool Reoccuring { get; set; }
    }
}
