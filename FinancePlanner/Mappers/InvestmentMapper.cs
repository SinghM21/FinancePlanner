using System;
using FinancePlanner.DTOs;
using FinancePlanner.Models.Investment;
using FinancePlanner.ViewModels;

namespace FinancePlanner.Mappers
{
    public class InvestmentMapper : IInvestmentMapper
    {
        public InvestmentDto MapToDTO(InvestmentViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            
            var dto = new InvestmentDto
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Type = viewModel.Type,
                Quantity = viewModel.Quantity,
                Cost = viewModel.Cost,
                Recurring = viewModel.Recurring,
                Frequency = viewModel.Frequency,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };
            return dto;
        }

        public InvestmentDto MapToDTO(Investment investment)
        {
            ArgumentNullException.ThrowIfNull(investment);
            
            var dto = new InvestmentDto
            {
                ID = investment.ID,
                Name = investment.Name,
                Description = investment.Description,
                Type = investment.Type,
                Quantity = investment.Quantity,
                Cost = investment.Cost,
                Recurring = investment.Recurring,
                Frequency = investment.Frequency,
                StartDate = investment.StartDate,
                EndDate = investment.EndDate
            };
            return dto;
        }

        public InvestmentViewModel MapToViewModel(InvestmentDto investmentDto)
        {
            ArgumentNullException.ThrowIfNull(investmentDto);
            
            var vm = new InvestmentViewModel
            {
                ID = investmentDto.ID,
                Name = investmentDto.Name,
                Description = investmentDto.Description,
                Type = investmentDto.Type,
                Quantity = investmentDto.Quantity,
                Cost = investmentDto.Cost,
                Recurring = investmentDto.Recurring,
                Frequency = investmentDto.Frequency,
                StartDate = investmentDto.StartDate,
                EndDate = investmentDto.EndDate
            };
            return vm;
        }

        public Investment MapToInvestmentEntity(InvestmentDto investmentDto)
        {
            ArgumentNullException.ThrowIfNull(investmentDto);
            
            Investment investment = new Investment
            {
                Name = investmentDto.Name,
                Description = investmentDto.Description,
                Type = investmentDto.Type,
                Quantity = investmentDto.Quantity,
                Cost = investmentDto.Cost,
                Recurring = investmentDto.Recurring,
                Frequency = investmentDto.Frequency,
                StartDate = investmentDto.StartDate,
                EndDate = investmentDto.EndDate
            };
            return investment;
        }

        public Investment UpdateEntityFromDTO(Investment investment, InvestmentDto investmentDto)
        {
            ArgumentNullException.ThrowIfNull(investment);
            ArgumentNullException.ThrowIfNull(investmentDto);
            EnsureFrequencyForRecurring(investmentDto.Recurring, investmentDto.Frequency, "UpdateEntityFromDTO");
            
            investment.Name = investmentDto.Name;
            investment.Description = investmentDto.Description;
            investment.Type = investmentDto.Type;
            investment.Quantity = investmentDto.Quantity;
            investment.Cost = investmentDto.Cost;
            investment.Recurring = investmentDto.Recurring;
            investment.Frequency = investmentDto.Frequency;
            investment.StartDate = investmentDto.StartDate;
            investment.EndDate = investmentDto.EndDate;

            return investment;
        }

        private static void EnsureFrequencyForRecurring(bool recurring, FrequencyType? frequency, string source)
        {
            if (recurring && frequency == null)
            {
                throw new ArgumentException($"Frequency is required for recurring investments (source: {source}).");
            }
        }
    }
}
