using Bank_Deposits.ViewModels;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Bank_Deposits.Controllers
{
    public class DepositorsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public DepositorsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1, string filterByName = null, decimal? minDepositAmount = null, decimal? maxDepositAmount = null)
        {
            var depositors = await _unitOfWork.Depositors.GetAllAsync();

            if (!string.IsNullOrEmpty(filterByName))
            {
                depositors = depositors.Where(d => d.FullName.Contains(filterByName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (minDepositAmount.HasValue || maxDepositAmount.HasValue)
            {
                foreach (var depositor in depositors)
                {
                    depositor.Operations = depositor.Operations
                        .Where(o => (!minDepositAmount.HasValue || o.DepositAmount >= minDepositAmount.Value) &&
                                    (!maxDepositAmount.HasValue || o.DepositAmount <= maxDepositAmount.Value))
                        .ToList();
                }
                depositors = depositors
                    .Where(d => d.Operations.Any())
                    .ToList();
            }

            PageViewModel<Depositor> pageViewModel = new PageViewModel<Depositor>(depositors, page, 10);

            ViewData["FilterByName"] = filterByName;
            ViewData["MinDepositAmount"] = minDepositAmount;
            ViewData["MaxDepositAmount"] = maxDepositAmount;

            return View(pageViewModel);
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Depositor depositor = await _unitOfWork.Depositors.GetAsync(id.Value);
            if (depositor == null)
            {
                return NotFound();
            }

            return View(depositor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Depositor depositor)
        {
            await _unitOfWork.Depositors.CreateAsync(depositor);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Depositor depositor = await _unitOfWork.Depositors.GetAsync(id.Value);
            if (depositor == null)
            {
                return NotFound();
            }
            return View(depositor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Depositor depositor)
        {
            if (id != depositor.DepositorId)
            {
                return NotFound();
            }
            try
            {
                await _unitOfWork.Depositors.UpdateAsync(depositor);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Depositors.GetAsync(depositor.DepositorId) != null)
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Depositor depositor = await _unitOfWork.Depositors.GetAsync(id.Value);
            if (depositor == null)
            {
                return NotFound();
            }

            return View(depositor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Depositor depositor = await _unitOfWork.Depositors.GetAsync(id);
            if (depositor != null)
            {
                await _unitOfWork.Depositors.DeleteAsync(depositor.DepositorId);  
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
