using FinancePlanner.Contexts;
using FinancePlanner.DTOs;
using FinancePlanner.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Services
{
    public class InvestmentService : IInvestmentService
    {
        FinancePlannerContext _context;
        IInvestmentMapper _investmentMapper;
        public InvestmentService(FinancePlannerContext context, IInvestmentMapper investmentMapper) { 
            _context = context;
            _investmentMapper = investmentMapper;
        }

        public Task<InvestmentDto> CreateInvestmentAsync(InvestmentDto investmentDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInvestmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<InvestmentDto>> GetAllInvestmentsAsync()
        {
            var entities = await _context.Investment
                                 .AsNoTracking()
                                 .ToListAsync();

            var investmentDTOs = entities
                .Select(e => _investmentMapper.MapToDTO(e))
                .ToList()
                .AsReadOnly();

            return investmentDTOs;
        }

        public Task<InvestmentDto?> GetInvestmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<InvestmentDto?> UpdateInvestmentAsync(int id, InvestmentDto investmentDto)
        {
            throw new NotImplementedException();
        }
    }
}
