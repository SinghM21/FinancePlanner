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
                if (investment.Recurring)
                {
                    var recurringInvestmentEntity = new RecurringInvestment
                    {
                        Name = investment.Name,
                        Description = investment.Description,
                        Type = investment.Type,
                        Quantity = investment.Quantity,
                        Cost = investment.Cost,
                        Frequency = investment.Frequency!.Value,
                        StartDate = investment.StartDate ?? DateTime.Now,
                        EndDate = investment.EndDate
                    };
                    _context.RecurringInvestment.Add(recurringInvestmentEntity);
                }
                else
                {
                    var investmentEntity = new Investment
                    {
                        Name = investment.Name,
                        Description = investment.Description,
                        Type = investment.Type,
                        Quantity = investment.Quantity,
                        Cost = investment.Cost
                    };
                    _context.Investment.Add(investmentEntity);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(investment);
        }

        // GET: Investments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // try to load recurring entity first
            var recurringInvestmentEntity = await _context.RecurringInvestment.FindAsync(id);
            if (recurringInvestmentEntity != null)
            {
                var vm = new InvestmentViewModel
                {
                    ID = recurringInvestmentEntity.ID,
                    Name = recurringInvestmentEntity.Name,
                    Description = recurringInvestmentEntity.Description,
                    Type = recurringInvestmentEntity.Type,
                    Quantity = recurringInvestmentEntity.Quantity,
                    Cost = recurringInvestmentEntity.Cost,
                    Recurring = true,
                    Frequency = recurringInvestmentEntity.Frequency,
                    StartDate = recurringInvestmentEntity.StartDate,
                    EndDate = recurringInvestmentEntity.EndDate
                };
                return View(vm);
            }

            // otherwise plain investment
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
                Recurring = false
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

            try
            {
                if (investment.Recurring)
                {
                    var recurringInvestmentEntity = await _context.RecurringInvestment.FindAsync(id);
                    if (recurringInvestmentEntity != null)
                    {
                        recurringInvestmentEntity.Name = investment.Name;
                        recurringInvestmentEntity.Description = investment.Description;
                        recurringInvestmentEntity.Type = investment.Type;
                        recurringInvestmentEntity.Quantity = investment.Quantity;
                        recurringInvestmentEntity.Cost = investment.Cost;
                        recurringInvestmentEntity.Frequency = investment.Frequency!.Value;
                        recurringInvestmentEntity.StartDate = investment.StartDate ?? DateTime.Now;
                        recurringInvestmentEntity.EndDate = investment.EndDate;

                        _context.Update(recurringInvestmentEntity);
                    }
                    else
                    {
                        // convert plain Investment -> RecurringInvestment
                        var investmentEntity = await _context.Investment.FindAsync(id);
                        if (investmentEntity != null)
                        {
                            _context.Investment.Remove(investmentEntity);
                        }

                        var newRecurringInvestment = new RecurringInvestment
                        {
                            Name = investment.Name,
                            Description = investment.Description,
                            Type = investment.Type,
                            Quantity = investment.Quantity,
                            Cost = investment.Cost,
                            Frequency = investment.Frequency!.Value,
                            StartDate = investment.StartDate ?? DateTime.Now,
                            EndDate = investment.EndDate
                        };
                        _context.RecurringInvestment.Add(newRecurringInvestment);
                    }
                }
                else
                {
                    // non-recurring path: update existing Investment or convert RecurringInvestment -> Investment
                    var recurringInvestmentEntity = await _context.RecurringInvestment.FindAsync(id);
                    if (recurringInvestmentEntity != null)
                    {
                        recurringInvestmentEntity.Name = investment.Name;
                        recurringInvestmentEntity.Description = investment.Description;
                        recurringInvestmentEntity.Type = investment.Type;
                        recurringInvestmentEntity.Quantity = investment.Quantity;
                        recurringInvestmentEntity.Cost = investment.Cost;

                        await using var tx = await _context.Database.BeginTransactionAsync();
                        await _context.SaveChangesAsync();

                        // remove derived-row only (TPT)
                        await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM RecurringInvestment WHERE ID = {id}");
                        await tx.CommitAsync();

                        _context.Entry(recurringInvestmentEntity).State = EntityState.Detached;
                    }
                    else
                    {
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

                        _context.Update(investmentEntity);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool entityExists = await _context.Investment.AnyAsync(e => e.ID == id) ||
                                    await _context.RecurringInvestment.AnyAsync(e => e.ID == id);
                if (!entityExists) return NotFound();
                throw;
            }

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
            var recurring = await _context.RecurringInvestment.FindAsync(id);
            if (recurring != null)
            {
                _context.RecurringInvestment.Remove(recurring);
            }
            else
            {
                var investment = await _context.Investment.FindAsync(id);
                if (investment != null)
                {
                    _context.Investment.Remove(investment);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // map DB entity -> ViewModel
        private async Task<InvestmentViewModel?> LoadInvestmentViewModelAsync(int id)
        {
            var recurring = await _context.RecurringInvestment.FindAsync(id);
            if (recurring != null)
            {
                return new InvestmentViewModel
                {
                    ID = recurring.ID,
                    Name = recurring.Name,
                    Description = recurring.Description,
                    Type = recurring.Type,
                    Quantity = recurring.Quantity,
                    Cost = recurring.Cost,
                    Recurring = true,
                    Frequency = recurring.Frequency,
                    StartDate = recurring.StartDate,
                    EndDate = recurring.EndDate
                };
            }

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
                Recurring = false
            };
        }
    }
}
