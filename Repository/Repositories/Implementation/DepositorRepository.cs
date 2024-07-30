using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementation
{
    public class DepositorRepository : IRepository<Depositor>
    {
        private readonly BankContext _db;

        public DepositorRepository(BankContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Depositor>> GetAllAsync()
        {
            return await _db.Depositors.Include(d => d.Operations).ThenInclude(o => o.Deposit).ToListAsync();
        }

        public async Task<Depositor> GetAsync(int id)
        {
            return await _db.Depositors.Include(d => d.Operations).ThenInclude(o => o.Deposit).FirstOrDefaultAsync(d => d.DepositorId == id);
        }

        public async Task CreateAsync(Depositor depositor)
        {
            await _db.Depositors.AddAsync(depositor);
        }

        public async Task UpdateAsync(Depositor depositor)
        {
            _db.Depositors.Update(depositor);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var depositor = await _db.Depositors.FindAsync(id);
            if (depositor != null)
                _db.Depositors.Remove(depositor);
            await Task.CompletedTask;
        }
    }
}
