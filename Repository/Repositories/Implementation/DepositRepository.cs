using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementation
{
    public class DepositRepository : IRepository<Deposit>
    {
        private readonly BankContext _db;

        public DepositRepository(BankContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Deposit>> GetAllAsync()
        {
            return await _db.Deposits.Include(d => d.Currency).ToListAsync();
        }

        public async Task<Deposit> GetAsync(int id)
        {
            return await _db.Deposits.Include(d => d.Currency).FirstOrDefaultAsync(d => d.DepositId == id);
        }

        public async Task CreateAsync(Deposit deposit)
        {
            await _db.Deposits.AddAsync(deposit);
        }

        public async Task UpdateAsync(Deposit deposit)
        {
            _db.Entry(deposit).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var deposit = await _db.Deposits.FindAsync(id);
            if (deposit != null)
                _db.Deposits.Remove(deposit);
            await Task.CompletedTask;
        }
    }
}
