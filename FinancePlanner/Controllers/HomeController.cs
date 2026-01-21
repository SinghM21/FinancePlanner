using FinancePlanner.Contexts;
using FinancePlanner.Helpers;
using FinancePlanner.Models;
using FinancePlanner.Models.Dashboard;
using FinancePlanner.Models.Investment;
using FinancePlanner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;

namespace FinancePlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FinancePlannerContext _context;

        public HomeController(ILogger<HomeController> logger, FinancePlannerContext financePlannerContext)
        {
            _logger = logger;
            _context = financePlannerContext;
        }

        public IActionResult Index()
        {
            int currentIncome = _context.Income.Sum(i => i.Amount);
            int currentOutcome = _context.Expense.Sum(o => o.Cost);
            currentOutcome = AddRecurringInvestmentCostsToOutcome(currentOutcome);
            var expensePercentagesByType = GetExpensePercentagesByType();

            DashboardViewModel dashboardViewModel = new(currentIncome, currentOutcome, expensePercentagesByType);

            return View(dashboardViewModel);
        }

        private int AddRecurringInvestmentCostsToOutcome(int currentOutcome)
        {
            var recurringInvestments = _context.Investment.Where(i => i.Recurring).ToList();
            if (recurringInvestments.Any())
            {
                currentOutcome += recurringInvestments.Sum(ri => ri.Cost * FrequencyTypeHelper.GetAnnualMultiplier(ri.Frequency!.Value));
            }
            return currentOutcome;
        }
        
        private Dictionary<string, decimal> GetExpensePercentagesByType()
        {
            var expenses = _context.Expense.ToList();
            var totalCost = expenses.Sum(e => e.Cost);
    
            if (totalCost == 0) return new Dictionary<string, decimal>();
    
            var expensesPercentagesByType = expenses
                .GroupBy(e => e.Type)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => Math.Round((decimal)(g.Sum(e => e.Cost) * 100) / totalCost, 0)
                );
    
            return expensesPercentagesByType;
        }

        [HttpPost]
        public IActionResult Index(DashboardFormValues dashboardFormValues)
        {
            int currentIncome = _context.Income.Sum(i => i.Amount);
            int currentOutcome = _context.Expense.Sum(o => o.Cost);

            DashboardViewModel dashboardViewModel = new(currentIncome, currentOutcome, dashboardFormValues);

            return View(dashboardViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
