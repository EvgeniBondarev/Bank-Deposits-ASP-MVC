using Bank_Deposits.ViewModels;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Bank_Deposits.Controllers
{
    public class OperationsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public OperationsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            PageViewModel<Operation> pageViewModel = new PageViewModel<Operation>(await _unitOfWork.Operations.GetAllAsync(), page, 10);
            return View(pageViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Operation operation = await _unitOfWork.Operations.GetAsync(id.Value);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["DepositId"] = new SelectList(await _unitOfWork.Deposits.GetAllAsync(), "DepositId", "Name");
            ViewData["DepositorId"] = new SelectList(await _unitOfWork.Depositors.GetAllAsync(), "DepositorId", "FullName");
            ViewData["EmployeeIds"] = new SelectList(await _unitOfWork.Employees.GetAllAsync(), "EmployeeId", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Operation model)
        {
            model.EmployeeOperations = model.SelectedEmployeeIds?.Select(id => new EmployeeOperation { EmployeeId = id }).ToList();

            await _unitOfWork.Operations.CreateAsync(model);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Operation operation = await _unitOfWork.Operations.GetAsync(id.Value);
            if (operation == null)
            {
                return NotFound();
            }

            operation.SelectedEmployeeIds = operation.EmployeeOperations.Select(eo => eo.EmployeeId).ToList();

            ViewData["DepositId"] = new SelectList(await _unitOfWork.Deposits.GetAllAsync(), "DepositId", "Name");
            ViewData["DepositorId"] = new SelectList(await _unitOfWork.Depositors.GetAllAsync(), "DepositorId", "FullName");
            ViewData["EmployeeIds"] = new SelectList(await _unitOfWork.Employees.GetAllAsync(), "EmployeeId", "FullName");
            return View(operation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Operation operation)
        {
            if (id != operation.OperationId)
            {
                return NotFound();
            }

            try
            {
                Operation existingOperation = await _unitOfWork.Operations.GetAsync(id);
                if (existingOperation == null)
                {
                    return NotFound();
                }

                List<EmployeeOperation> existingEmployeeOperations = (await _unitOfWork.EmployeeOperations.GetAllAsync()).Where(eo => eo.OperationId == id).ToList();

                foreach(EmployeeOperation eo in existingEmployeeOperations)
                {
                    await _unitOfWork.EmployeeOperations.DeleteAsync(eo.EmployeeId);
                }

                

                if (operation.SelectedEmployeeIds != null)
                {
                    existingOperation.EmployeeOperations = operation.SelectedEmployeeIds
                                                                    .Select(eid => new EmployeeOperation { EmployeeId = eid, OperationId = id })
                                                                    .ToList();
                }

                await _unitOfWork.Operations.UpdateAsync(existingOperation);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Operations.GetAsync(operation.OperationId) == null)
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

            Operation operation = await _unitOfWork.Operations.GetAsync(id.Value);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Operation operation = await _unitOfWork.Operations.GetAsync(id);
            if (operation != null)
            {
                await _unitOfWork.Operations.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
