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
            var dto = new InvestmentDto
            {
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
            var dto = new InvestmentDto
            {
                Name = investment.Name,
                Description = investment.Description,
                Type = investment.Type,
                Quantity = investment.Quantity,
                Cost = investment.Cost
            };

            if (investment is RecurringInvestment recurringInvestment)
            {
                dto.Recurring = true;
                dto.Frequency = recurringInvestment.Frequency;
                dto.StartDate = recurringInvestment.StartDate;
                dto.EndDate = recurringInvestment.EndDate;
            }
            else
            {
                dto.Recurring = false;
            }

            return dto;
        }

        public InvestmentViewModel MapToViewModel(InvestmentDto investmentDto)
        {
            var vm = new InvestmentViewModel
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
            return vm;
        }

        public Investment MapToInvestmentEntity(InvestmentDto investmentDto)
        { 
            Investment investment;
            if (investmentDto.Recurring)
            {
                EnsureFrequencyForRecurring(investmentDto.Recurring, investmentDto.Frequency, "MapEntityFromDTO");
                var recurringInvestment = new RecurringInvestment
                {
                    Frequency = investmentDto.Frequency!.Value,
                    StartDate = investmentDto.StartDate,
                    EndDate = investmentDto.EndDate
                };
                investment = recurringInvestment;
            }
            else
            {
                investment = new Investment();
            }

            investment.Name = investmentDto.Name;
            investment.Description = investmentDto.Description;
            investment.Type = investmentDto.Type;
            investment.Quantity = investmentDto.Quantity;
            investment.Cost = investmentDto.Cost;
            return investment;
        }

        public Investment UpdateEntityFromDTO(Investment investment, InvestmentDto investmentDto)
        {
            investment.Name = investmentDto.Name;
            investment.Description = investmentDto.Description;
            investment.Type = investmentDto.Type;
            investment.Quantity = investmentDto.Quantity;
            investment.Cost = investmentDto.Cost;

            if (investmentDto.Recurring && investment is RecurringInvestment recurringInvestment)
            {
                EnsureFrequencyForRecurring(investmentDto.Recurring, investmentDto.Frequency, "UpdateEntityFromDTO");
                recurringInvestment.Frequency = investmentDto.Frequency!.Value;
                recurringInvestment.StartDate = investmentDto.StartDate;
                recurringInvestment.EndDate = investmentDto.EndDate;
                return recurringInvestment;
            }

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
