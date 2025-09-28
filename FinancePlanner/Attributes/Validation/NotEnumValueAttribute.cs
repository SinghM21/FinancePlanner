using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Attributes.Validation
{
    public class NotEnumValueAttribute : ValidationAttribute
    {
        private readonly object _invalidValue;
        public NotEnumValueAttribute(object invalidValue)
        {
            _invalidValue = invalidValue;
        }
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true; // Consider null as valid. Use [Required] for null checks.
            }
            return !value.Equals(_invalidValue);
        }
    }
}
