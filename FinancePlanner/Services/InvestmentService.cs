using FinancePlanner.Contexts;
using FinancePlanner.DTOs;
using FinancePlanner.Mappers;
using FinancePlanner.Models.Investment;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Services
{
    public class InvestmentService : IInvestmentService
    {
        FinancePlannerContext _context;
        IInvestmentMapper _investmentMapper;
        public InvestmentService(FinancePlannerContext context, IInvestmentMapper investmentMapper)
        {
            _context = context;
            _investmentMapper = investmentMapper;
        }

        public async Task CreateInvestmentAsync(InvestmentDto investmentDto)
        {
            Investment investment = _investmentMapper.MapToInvestmentEntity(investmentDto);
            await _context.Investment.AddAsync(investment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteInvestmentAsync(int id)
        {
            var rowsAffected = await _context.Investment
                .Where(e => e.ID == id)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }

        public async Task<IReadOnlyList<InvestmentDto>> GetAllInvestmentsAsync()
        {
            var investments = await _context.Investment
                                 .AsNoTracking()
                                 .ToListAsync();

            var investmentDTOs = investments
                .Select(e => _investmentMapper.MapToDTO(e))
                .ToList()
                .AsReadOnly();

            return investmentDTOs;
        }

        public async Task<InvestmentDto?> GetInvestmentByIdAsync(int id)
        {
            var investment = await _context.Investment
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(e => e.ID == id);

            if (investment == null)
            {
                return null;
            }
            return _investmentMapper.MapToDTO(investment);
        }

        public async Task<InvestmentDto?> UpdateInvestmentAsync(int id, InvestmentDto investmentDto)
        {
            var investment = await _context.Investment
                     .FirstOrDefaultAsync(e => e.ID == id);

            if (investment == null)
            {
                return null;
            }

            _investmentMapper.UpdateEntityFromDTO(investment, investmentDto);
            await _context.SaveChangesAsync();

            return _investmentMapper.MapToDTO(investment);
        }
    }
}
