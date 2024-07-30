using Bank_Deposits.ViewModels;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Bank_Deposits.Controllers
{
    public class ExchangeRatesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public ExchangeRatesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            PageViewModel<ExchangeRate> pageViewModel = new PageViewModel<ExchangeRate>(await _unitOfWork.ExchangeRates.GetAllAsync(), page, 10);
            return View(pageViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExchangeRate exchangeRate = await _unitOfWork.ExchangeRates.GetAsync(id.Value);
            if (exchangeRate == null)
            {
                return NotFound();
            }

            return View(exchangeRate);
        }


        public async Task<IActionResult> Create()
        {
            ViewData["CurrencyId"] = new SelectList(await _unitOfWork.Currencies.GetAllAsync(), "CurrencyId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExchangeRate exchangeRate)
        {
            await _unitOfWork.ExchangeRates.CreateAsync(exchangeRate);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExchangeRate exchangeRate = await _unitOfWork.ExchangeRates.GetAsync(id.Value);
            if (exchangeRate == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(await _unitOfWork.Currencies.GetAllAsync(), "CurrencyId", "Name", exchangeRate.CurrencyId);
            return View(exchangeRate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExchangeRate exchangeRate)
        {
            if (id != exchangeRate.ExchangeRateId)
            {
                return NotFound();
            }
            try
            {
                await _unitOfWork.ExchangeRates.UpdateAsync(exchangeRate);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.ExchangeRates.GetAsync(exchangeRate.ExchangeRateId) != null)
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

            ExchangeRate exchangeRate = await _unitOfWork.ExchangeRates.GetAsync(id.Value);
            if (exchangeRate == null)
            {
                return NotFound();
            }

            return View(exchangeRate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ExchangeRate exchangeRate = await _unitOfWork.ExchangeRates.GetAsync(id);
            if (exchangeRate != null)
            {
               await _unitOfWork.ExchangeRates.DeleteAsync(id);
               await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
