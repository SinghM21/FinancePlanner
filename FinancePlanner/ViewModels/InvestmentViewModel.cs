using FinancePlanner.Attributes.Validation;
using FinancePlanner.Models.Investment;

namespace FinancePlanner.ViewModels
{
    public class InvestmentViewModel
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        [NotEnumValue(InvestmentType.None, ErrorMessage = "Please select an investment type")]
        public InvestmentType Type { get; set; }

        public int? Quantity { get; set; }

        public int Cost { get; set; }

        // Recurring-specific
        public bool Recurring { get; set; }

        public FrequencyType? Frequency { get; set; }

        public int? FrequencyInDays { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
