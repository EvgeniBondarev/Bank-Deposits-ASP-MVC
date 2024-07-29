using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementation
{
    public class EmployeeOperationRepository : IRepository<EmployeeOperation>
    {
        private readonly BankContext _db;

        public EmployeeOperationRepository(BankContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<EmployeeOperation>> GetAllAsync()
        {
            return await _db.EmployeeOperations.Include(eo => eo.Employee).Include(eo => eo.Operation).ToListAsync();
        }

        public async Task<EmployeeOperation> GetAsync(int id)
        {
            return await _db.EmployeeOperations.Include(eo => eo.Employee).Include(eo => eo.Operation).FirstOrDefaultAsync(eo => eo.EmployeeOperationId == id);
        }

        public async Task CreateAsync(EmployeeOperation employeeOperation)
        {
            await _db.EmployeeOperations.AddAsync(employeeOperation);
        }

        public async Task UpdateAsync(EmployeeOperation employeeOperation)
        {
            _db.Entry(employeeOperation).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var employeeOperation = await _db.EmployeeOperations.FindAsync(id);
            if (employeeOperation != null)
                _db.EmployeeOperations.Remove(employeeOperation);
            await Task.CompletedTask;
        }
    }
}
