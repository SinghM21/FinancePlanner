using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.Investment
{
    public enum InvestmentType
    {
        [Display(Name = "Please select")]
        None,
        Stocks,
        Bonds,
        RealEstate,
        MutualFunds,
        ETFs,
        RetirementAccounts,
        Cryptocurrencies,
        Commodities,
        Other
    }
}
