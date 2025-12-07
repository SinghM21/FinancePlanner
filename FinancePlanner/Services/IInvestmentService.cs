using FinancePlanner.DTOs;

namespace FinancePlanner.Services
{
    public interface IInvestmentService
    {
        public Task<InvestmentDto> CreateInvestmentAsync(InvestmentDto investmentDto);
        public Task<InvestmentDto?> UpdateInvestmentAsync(int id, InvestmentDto investmentDto);
        public Task<bool> DeleteInvestmentAsync(int id);
        public Task<InvestmentDto?> GetInvestmentByIdAsync(int id);
        public Task<IReadOnlyList<InvestmentDto>> GetAllInvestmentsAsync();

    }
}
