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
            int currentOutcome = _context.Expense.Sum(o => o.Cost);

            DashboardViewModel dashboardViewModel = new(currentIncome, currentOutcome);

            return View(dashboardViewModel);
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
