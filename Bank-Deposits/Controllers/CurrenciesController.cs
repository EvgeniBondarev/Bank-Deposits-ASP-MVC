using Bank_Deposits.ViewModels;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Bank_Deposits.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CurrenciesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            PageViewModel<Currency> pageViewModel = new PageViewModel<Currency>(await _unitOfWork.Currencies.GetAllAsync(), page, 10); 
            return View(pageViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Currency currency = await _unitOfWork.Currencies.GetAsync(id.Value);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Currency currency)
        {
            await _unitOfWork.Currencies.CreateAsync(currency);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Currency currency = await _unitOfWork.Currencies.GetAsync(id.Value);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Currency currency)
        {
            if (id != currency.CurrencyId)
            {
                return NotFound();
            }

            try
            {
                await _unitOfWork.Currencies.UpdateAsync(currency);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Currencies.GetAsync(currency.CurrencyId) != null)
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

            Currency currency = await _unitOfWork.Currencies.GetAsync(id.Value);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Currency currency = await _unitOfWork.Currencies.GetAsync(id);
            if (currency != null)
            {
                await _unitOfWork.Currencies.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
