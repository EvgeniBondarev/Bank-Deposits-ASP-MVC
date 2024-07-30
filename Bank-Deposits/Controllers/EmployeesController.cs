using Bank_Deposits.ViewModels;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Bank_Deposits.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public EmployeesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            PageViewModel<Employee> pageViewModel = new PageViewModel<Employee>(await _unitOfWork.Employees.GetAllAsync(), page, 10);
            return View(pageViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = await _unitOfWork.Employees.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {

            await _unitOfWork.Employees.CreateAsync(employee);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = await _unitOfWork.Employees.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            try
            {
                await _unitOfWork.Employees.UpdateAsync(employee);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Employees.GetAsync(employee.EmployeeId) != null)
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

            Employee employee = await _unitOfWork.Employees.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(id);
            if (employee != null)
            {
                await _unitOfWork.Employees.DeleteAsync(id);
                await _unitOfWork.SaveAsync();  
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
