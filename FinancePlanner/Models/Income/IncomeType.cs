using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.Income
{
    public enum IncomeType
    {
        [Display(Name = "Please select")]
        None,
        Salary,
        Business,
        Property,
        Gift,
        Other
    }
}
