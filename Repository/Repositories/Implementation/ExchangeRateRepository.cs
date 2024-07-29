using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementation
{
    public class ExchangeRateRepository : IRepository<ExchangeRate>
    {
        private readonly BankContext _db;

        public ExchangeRateRepository(BankContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<ExchangeRate>> GetAllAsync()
        {
            return await _db.ExchangeRates.Include(er => er.Currency).ToListAsync();
        }

        public async Task<ExchangeRate> GetAsync(int id)
        {
            return await _db.ExchangeRates.Include(er => er.Currency).FirstOrDefaultAsync(er => er.ExchangeRateId == id);
        }

        public async Task CreateAsync(ExchangeRate exchangeRate)
        {
            await _db.ExchangeRates.AddAsync(exchangeRate);
        }

        public async Task UpdateAsync(ExchangeRate exchangeRate)
        {
            _db.Entry(exchangeRate).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var exchangeRate = await _db.ExchangeRates.FindAsync(id);
            if (exchangeRate != null)
                _db.ExchangeRates.Remove(exchangeRate);
            await Task.CompletedTask;
        }
    }
}
