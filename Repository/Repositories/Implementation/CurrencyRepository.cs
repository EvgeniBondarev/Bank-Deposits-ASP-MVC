using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementation
{
    public class CurrencyRepository : IRepository<Currency>
    {
        private readonly BankContext _db;

        public CurrencyRepository(BankContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _db.Currencies.ToListAsync();
        }

        public async Task<Currency> GetAsync(int id)
        {
            return await _db.Currencies.FindAsync(id);
        }

        public async Task CreateAsync(Currency currency)
        {
            await _db.Currencies.AddAsync(currency);
        }

        public async Task UpdateAsync(Currency currency)
        {
            _db.Currencies.Update(currency);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var currency = await _db.Currencies.FindAsync(id);
            if (currency != null)
                _db.Currencies.Remove(currency);
            await Task.CompletedTask;
        }
    }
}
