using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementation
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly BankContext _db;

        public EmployeeRepository(BankContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _db.Employees.Include(e => e.EmployeeOperations).ThenInclude(eo => eo.Operation).ToListAsync();
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _db.Employees.Include(e => e.EmployeeOperations).ThenInclude(eo => eo.Operation).FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task CreateAsync(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _db.Entry(employee).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee != null)
                _db.Employees.Remove(employee);
            await Task.CompletedTask;
        }
    }

}
