using FinancePlanner.Contexts;
using FinancePlanner.Models;
using FinancePlanner.Models.Dashboard;
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
            int currentOutcome = _context.Outcome.Sum(o => o.Cost);

            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                monthlyIncome = currentIncome,
                monthlyOutcome = currentOutcome,
                yearlyIncome = CalculateYearlyIncomes(currentIncome, 0),
                yearlyOutcome = CalculateYearlyOutcomes(currentOutcome, 0)
            };

            return View(dashboardViewModel);
        }

        [HttpPost]
        public IActionResult Index(DashboardFormValues dashboardFormValues)
        {
            int currentIncome = _context.Income.Sum(i => i.Amount);
            int currentOutcome = _context.Outcome.Sum(o => o.Cost);

            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                monthlyIncome = currentIncome,
                monthlyOutcome = currentOutcome,
                yearlyIncome = CalculateYearlyIncomes(currentIncome, ConvertNumberToDecimal(dashboardFormValues.IncomeProjectionIncreaseNumber)),
                yearlyOutcome = CalculateYearlyOutcomes(currentOutcome, ConvertNumberToDecimal(dashboardFormValues.OutcomeProjectionIncreaseNumber))
            };

            return View(dashboardViewModel);
        }

        public decimal[] CalculateYearlyIncomes(int currentIncome, decimal expectedIncomeIncreasePercentage)
        {
            List <decimal> yearlyIncomes = new() { currentIncome };
            for (int years = 1; years <= 10; years++)
            {
                decimal nextIncomeIncrease = yearlyIncomes.ElementAt(years - 1) * (expectedIncomeIncreasePercentage + 1);
                yearlyIncomes.Add(nextIncomeIncrease);
            }

            return yearlyIncomes.ToArray();
        }

        public decimal[] CalculateYearlyOutcomes(int currentOutcome, decimal expectedOutcomeIncreasePercentage)
        {
            List<decimal> yearlyOutcomes = new() { currentOutcome };
            for (int years = 1; years <= 10; years++)
            {
                decimal nextOutcomeIncrease = yearlyOutcomes.ElementAt(years - 1) * (expectedOutcomeIncreasePercentage + 1);
                yearlyOutcomes.Add(nextOutcomeIncrease);
            }

            return yearlyOutcomes.ToArray();
        }

        public decimal ConvertNumberToDecimal(int number)
        {
            return (decimal)number / 100;
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
