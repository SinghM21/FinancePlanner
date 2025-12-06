using FinancePlanner.Models.Investment;

namespace FinancePlanner.DTOs
{
    public class InvestmentDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public InvestmentType Type { get; set; }
        public int? Quantity { get; set; }
        public int Cost { get; set; }

        // Recurring-specific
        public bool Recurring { get; set; }
        public FrequencyType? Frequency { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
