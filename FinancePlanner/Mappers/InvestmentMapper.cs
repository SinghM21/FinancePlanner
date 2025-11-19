using FinancePlanner.Models.Investment;
using FinancePlanner.ViewModels;

namespace FinancePlanner.Mappers
{
    public class InvestmentMapper : IInvestmentMapper
    {
        public InvestmentViewModel MapToViewModel(Investment investment)
        {
            var vm = new InvestmentViewModel();
            CopyInvestmentPropertiesToViewModel(investment, vm);

            if (investment is RecurringInvestment recurringInvestment)
            {
                vm.Recurring = true;
                CopyRecurringInvestmentPropertiesToViewModel(recurringInvestment, vm);
            }
            else
            {
                vm.Recurring = false;
            }

            return vm;
        }

        public Investment MapToInvestmentEntity(InvestmentViewModel investmentViewModel)
        {
            if (investmentViewModel.Recurring)
            {
                var recurring = new RecurringInvestment();
                CopyInvestmentPropertiesToEntity(investmentViewModel, recurring, setId: true);
                CopyRecurringInvestmentPropertiesToEntity(investmentViewModel, recurring);
                return recurring;
            }
            else
            {
                var investment = new Investment();
                CopyInvestmentPropertiesToEntity(investmentViewModel, investment, setId: true);
                return investment;
            }
        }

        public Investment UpdateEntityFromViewModel(Investment investment, InvestmentViewModel investmentViewModel)
        {
            CopyInvestmentPropertiesToEntity(investmentViewModel, investment, setId: false);

            if (investmentViewModel.Recurring && investment is RecurringInvestment recurringInvestment)
            {
                CopyRecurringInvestmentPropertiesToEntity(investmentViewModel, recurringInvestment);
                return recurringInvestment;
            }

            return investment;
        }

        private static void CopyInvestmentPropertiesToViewModel(Investment entity, InvestmentViewModel vm)
        {
            vm.ID = entity.ID;
            vm.Name = entity.Name;
            vm.Description = entity.Description;
            vm.Type = entity.Type;
            vm.Quantity = entity.Quantity;
            vm.Cost = entity.Cost;
        }

        private static void CopyRecurringInvestmentPropertiesToViewModel(RecurringInvestment entity, InvestmentViewModel vm)
        {
            vm.Frequency = entity.Frequency;
            vm.StartDate = entity.StartDate;
            vm.EndDate = entity.EndDate;
        }

        private static void CopyInvestmentPropertiesToEntity(InvestmentViewModel vm, Investment entity, bool setId)
        {
            if (setId)
            {
                entity.ID = vm.ID;
            }

            entity.Name = vm.Name;
            entity.Description = vm.Description;
            entity.Type = vm.Type;
            entity.Quantity = vm.Quantity;
            entity.Cost = vm.Cost;
        }

        private static void CopyRecurringInvestmentPropertiesToEntity(InvestmentViewModel vm, RecurringInvestment entity)
        {
            entity.Frequency = vm.Frequency ?? FrequencyType.None;
            entity.StartDate = vm.StartDate;
            entity.EndDate = vm.EndDate;
        }
    }
}
