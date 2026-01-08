using FinancePlanner.DTOs;
using FinancePlanner.Models.Investment;
using FinancePlanner.ViewModels;

namespace FinancePlanner.Mappers
{
    public interface IInvestmentMapper
    {
        InvestmentDto MapToDTO(InvestmentViewModel viewModel);
        InvestmentDto MapToDTO(Investment investment);
        InvestmentViewModel MapToViewModel(InvestmentDto investmentDto);
        Investment MapToInvestmentEntity(InvestmentDto investmentDto);
        Investment UpdateEntityFromDTO(Investment investment, InvestmentDto investmentDto);
    }
}
