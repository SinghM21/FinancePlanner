using FinancePlanner.Models.Investment;
using FinancePlanner.ViewModels;

namespace FinancePlanner.Mappers
{
    public interface IInvestmentMapper
    {
        InvestmentViewModel MapToViewModel(Investment investment);
        Investment MapToInvestmentEntity(InvestmentViewModel investmentViewModel);
        Investment UpdateEntityFromViewModel(Investment investment, InvestmentViewModel investmentViewModel);
    }
}
