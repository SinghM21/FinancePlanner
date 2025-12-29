using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancePlanner.Contexts;
using FinancePlanner.Mappers;
using FinancePlanner.Models.Investment;
using FinancePlanner.Services;
using FinancePlanner.ViewModels;

namespace FinancePlanner.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly IInvestmentMapper _investmentMapper;
        private readonly IInvestmentService _investmentService;

        public InvestmentsController(IInvestmentMapper investmentMapper, IInvestmentService investmentService)
        {
            _investmentMapper = investmentMapper;
            _investmentService = investmentService;
        }

        // GET: Investments
        public async Task<IActionResult> Index()
        {
            var investments = await _investmentService.GetAllInvestmentsAsync();
            return View(investments);
        }

        // GET: Investments/Create
        public IActionResult Create()
        {
            InvestmentViewModel vm = new();
            return View(vm);
        }

        // POST: Investments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestmentViewModel investmentVm)
        {
            if (ModelState.IsValid)
            {
                var investmentDto = _investmentMapper.MapToDTO(investmentVm);
                await _investmentService.CreateInvestmentAsync(investmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(investmentVm);
        }

        // GET: Investments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var investmentDto = await _investmentService.GetInvestmentByIdAsync(id.Value);
            if (investmentDto == null) return NotFound();

            var investmentVm = _investmentMapper.MapToViewModel(investmentDto);
            return View(investmentVm);
        }

        // POST: Investments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InvestmentViewModel investmentVm)
        {
            // integrity check: route id must match posted VM id
            if (id != investmentVm.ID) return BadRequest();
            if (!ModelState.IsValid) return View(investmentVm);

            var investmentDto = _investmentMapper.MapToDTO(investmentVm);
            await _investmentService.UpdateInvestmentAsync(id, investmentDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Investments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var investmentDto = await _investmentService.GetInvestmentByIdAsync(id.Value);
            if (investmentDto == null) return NotFound();

            var investmentVm = _investmentMapper.MapToViewModel(investmentDto);
            return View(investmentVm);
        }

        // GET: Investments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var investmentDto = await _investmentService.GetInvestmentByIdAsync(id.Value);
            if (investmentDto == null) return NotFound();

            var investmentVm = _investmentMapper.MapToViewModel(investmentDto);
            return View(investmentVm);
        }

        // POST: Investments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _investmentService.DeleteInvestmentAsync(id))
            {
                ModelState.AddModelError("", "An error occurred while deleting the investment");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
