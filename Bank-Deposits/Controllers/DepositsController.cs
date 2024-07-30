using Bank_Deposits.ViewModels;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Bank_Deposits.Controllers
{
    public class DepositsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public DepositsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            PageViewModel<Deposit> pageViewModel = new PageViewModel<Deposit>(await _unitOfWork.Deposits.GetAllAsync(), page, 10);
            return View(pageViewModel);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deposit deposit = await _unitOfWork.Deposits.GetAsync(id.Value);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CurrencyId"] = new SelectList(await _unitOfWork.Currencies.GetAllAsync(), "CurrencyId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepositId,Name,MinimumTerm,MinimumAmount,CurrencyId,InterestRate,AdditionalConditions")] Deposit deposit)
        {
            await _unitOfWork.Deposits.CreateAsync(deposit);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deposit deposit = await _unitOfWork.Deposits.GetAsync(id.Value);
            if (deposit == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(await _unitOfWork.Currencies.GetAllAsync(), "CurrencyId", "Name", deposit.CurrencyId);
            return View(deposit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepositId,Name,MinimumTerm,MinimumAmount,CurrencyId,InterestRate,AdditionalConditions")] Deposit deposit)
        {
            if (id != deposit.DepositId)
            {
                return NotFound();
            }

            try
            {
                await _unitOfWork.Deposits.UpdateAsync(deposit);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Deposits.GetAsync(deposit.DepositId) != null)
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

            Deposit deposit = await _unitOfWork.Deposits.GetAsync(id.Value);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Deposit deposit = await _unitOfWork.Deposits.GetAsync(id);
            if (deposit != null)
            {
                await _unitOfWork.Deposits.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
