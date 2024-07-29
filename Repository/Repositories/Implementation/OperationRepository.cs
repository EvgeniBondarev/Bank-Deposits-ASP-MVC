using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementation
{
    public class OperationRepository : IRepository<Operation>
    {
        private readonly BankContext _db;

        public OperationRepository(BankContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Operation>> GetAllAsync()
        {
            return await _db.Operations.Include(o => o.Depositor).Include(o => o.Deposit).ToListAsync();
        }

        public async Task<Operation> GetAsync(int id)
        {
            return await _db.Operations.Include(o => o.Depositor).Include(o => o.Deposit).FirstOrDefaultAsync(o => o.OperationId == id);
        }

        public async Task CreateAsync(Operation operation)
        {
            await _db.Operations.AddAsync(operation);
        }

        public async Task UpdateAsync(Operation operation)
        {
            _db.Entry(operation).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var operation = await _db.Operations.FindAsync(id);
            if (operation != null)
                _db.Operations.Remove(operation);
            await Task.CompletedTask;
        }
    }
}
