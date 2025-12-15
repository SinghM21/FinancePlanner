using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancePlanner.Contexts;
using FinancePlanner.Models.Investment;
using FinancePlanner.ViewModels;

namespace FinancePlanner.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly FinancePlannerContext _context;

        public InvestmentsController(FinancePlannerContext context)
        {
            _context = context;
        }

        // GET: Investments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Investment.ToListAsync());
        }

        // GET: Investments/Create
        public IActionResult Create()
        {
            InvestmentViewModel vm = new()
            {
                Name = string.Empty
            };
            return View(vm);
        }

        // POST: Investments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestmentViewModel investment)
        {
            if (ModelState.IsValid)
            {
                var investmentEntity = new Investment
                {
                    Name = investment.Name,
                    Description = investment.Description,
                    Type = investment.Type,
                    Quantity = investment.Quantity,
                    Cost = investment.Cost,
                    Recurring = investment.Recurring,
                    Frequency = investment.Frequency ?? null,
                    StartDate = investment.StartDate ?? DateTime.Now,
                    EndDate = investment.EndDate ?? null
                };
                _context.Investment.Add(investmentEntity);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(investment);
        }

        // GET: Investments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var investmentEntity = await _context.Investment.FindAsync(id);
            if (investmentEntity == null) return NotFound();

            var investmentVm = new InvestmentViewModel
            {
                ID = investmentEntity.ID,
                Name = investmentEntity.Name,
                Description = investmentEntity.Description,
                Type = investmentEntity.Type,
                Quantity = investmentEntity.Quantity,
                Cost = investmentEntity.Cost,
                Recurring = investmentEntity.Recurring,
                Frequency = investmentEntity.Frequency ?? null,
                StartDate = investmentEntity.StartDate ?? null,
                EndDate = investmentEntity.EndDate ?? null
            };
            return View(investmentVm);
        }

        // POST: Investments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InvestmentViewModel investment)
        {
            // integrity check: route id must match posted VM id
            if (investment == null || id != investment.ID) return BadRequest();

            if (!ModelState.IsValid) return View(investment);

            var investmentEntity = await _context.Investment.FindAsync(id);
            if (investmentEntity == null)
            {
                return NotFound();
            }

            investmentEntity.Name = investment.Name;
            investmentEntity.Description = investment.Description;
            investmentEntity.Type = investment.Type;
            investmentEntity.Quantity = investment.Quantity;
            investmentEntity.Cost = investment.Cost;
            investmentEntity.Recurring = investment.Recurring;
            investmentEntity.Frequency = investment.Frequency ?? null;
            investmentEntity.StartDate = investment.StartDate ?? null;
            investmentEntity.EndDate = investment.EndDate ?? null;

            _context.Update(investmentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Investments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vm = await LoadInvestmentViewModelAsync(id.Value);
            if (vm == null) return NotFound();

            return View(vm);
        }

        // GET: Investments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vm = await LoadInvestmentViewModelAsync(id.Value);
            if (vm == null) return NotFound();

            return View(vm);
        }

        // POST: Investments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investment = await _context.Investment.FindAsync(id);
            if (investment != null)
            {
                _context.Investment.Remove(investment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // map DB entity -> ViewModel
        private async Task<InvestmentViewModel?> LoadInvestmentViewModelAsync(int id)
        {
            var investment = await _context.Investment.FindAsync(id);
            if (investment == null) return null;

            return new InvestmentViewModel
            {
                ID = investment.ID,
                Name = investment.Name,
                Description = investment.Description,
                Type = investment.Type,
                Quantity = investment.Quantity,
                Cost = investment.Cost,
                Recurring = investment.Recurring,
                Frequency = investment.Frequency ?? null,
                StartDate = investment.StartDate ?? null,
                EndDate = investment.EndDate ?? null
            };
        }
    }
}
