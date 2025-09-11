using FinancePlanner.Contexts;
using FinancePlanner.Models;
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
                yearlyIncome = calculateYearlyIncomes(currentIncome, 1),
                yearlyOutcome = calculateYearlyOutcomes(currentOutcome, 1)
            };

            return View(dashboardViewModel);
        }

        [HttpPost]
        public IActionResult Index(int expectedIncomeIncreasePercentage, int expectedOutcomeIncreasePercentage)
        {
            int currentIncome = _context.Income.Sum(i => i.Amount);
            int currentOutcome = _context.Outcome.Sum(o => o.Cost);

            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                monthlyIncome = currentIncome,
                monthlyOutcome = currentOutcome,
                yearlyIncome = calculateYearlyIncomes(currentIncome, expectedIncomeIncreasePercentage),
                yearlyOutcome = calculateYearlyOutcomes(currentOutcome, expectedOutcomeIncreasePercentage)
            };

            return View(dashboardViewModel);
        }

        public int[] calculateYearlyIncomes(int currentIncome, int expectedIncomeIncreasePercentage)
        {
            List<int> yearlyIncomes = new() { currentIncome };
            for (int years = 1; years <= 10; years++)
            {
                int nextIncomeIncrease = yearlyIncomes.ElementAt(years - 1) * expectedIncomeIncreasePercentage;
                yearlyIncomes.Add(nextIncomeIncrease);
            }

            return yearlyIncomes.ToArray();
        }

        public int[] calculateYearlyOutcomes(int currentOutcome, int expectedOutcomeIncreasePercentage)
        {
            List<int> yearlyOutcomes = new() { currentOutcome };
            for (int years = 1; years <= 10; years++)
            {
                int nextOutcomeIncrease = yearlyOutcomes.ElementAt(years - 1) * expectedOutcomeIncreasePercentage;
                yearlyOutcomes.Add(nextOutcomeIncrease);
            }

            return yearlyOutcomes.ToArray();
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
