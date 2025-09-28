using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.Expense
{
    public enum ExpenseType
    {
        [Display(Name = "Please select")]
        None,
        Food,
        Rent,
        Utilities,
        Entertainment,
        Healthcare,
        Transportation,
        Education,
        Savings,
        Other
    }
}
