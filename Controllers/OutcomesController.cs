using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancePlanner.Contexts;
using FinancePlanner.Models;

namespace FinancePlanner.Controllers
{
    public class OutcomesController : Controller
    {
        private readonly FinancePlannerContext _context;

        public OutcomesController(FinancePlannerContext context)
        {
            _context = context;
        }

        // GET: Outcomes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Outcome.ToListAsync());
        }

        // GET: Outcomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outcome = await _context.Outcome
                .FirstOrDefaultAsync(m => m.ID == id);
            if (outcome == null)
            {
                return NotFound();
            }

            return View(outcome);
        }

        // GET: Outcomes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Outcomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Quantity,Cost")] Outcome outcome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outcome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(outcome);
        }

        // GET: Outcomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outcome = await _context.Outcome.FindAsync(id);
            if (outcome == null)
            {
                return NotFound();
            }
            return View(outcome);
        }

        // POST: Outcomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Quantity,Cost")] Outcome outcome)
        {
            if (id != outcome.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outcome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutcomeExists(outcome.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(outcome);
        }

        // GET: Outcomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outcome = await _context.Outcome
                .FirstOrDefaultAsync(m => m.ID == id);
            if (outcome == null)
            {
                return NotFound();
            }

            return View(outcome);
        }

        // POST: Outcomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outcome = await _context.Outcome.FindAsync(id);
            if (outcome != null)
            {
                _context.Outcome.Remove(outcome);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutcomeExists(int id)
        {
            return _context.Outcome.Any(e => e.ID == id);
        }
    }
}
