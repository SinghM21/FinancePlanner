using FinancePlanner.Attributes.Validation;
using FinancePlanner.Models.Investment;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.ViewModels
{
    public class InvestmentViewModel : IValidatableObject
    {
        public int ID { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [NotEnumValue(InvestmentType.None, ErrorMessage = "Please select an investment type")]
        public InvestmentType Type { get; set; }
        
        public PingItem? PingItem { get; set; }

        public int? Quantity { get; set; }

        public int Cost { get; set; }

        // Recurring-specific
        public bool Recurring { get; set; }

        public FrequencyType? Frequency { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Recurring)
            {
                if (Frequency == null || Frequency == FrequencyType.None)
                {
                    yield return new ValidationResult(
                        "Frequency is required for recurring investments.",
                        new[] { nameof(Frequency) });
                }
            }
        }
    }
}
