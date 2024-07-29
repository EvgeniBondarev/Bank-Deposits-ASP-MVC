using Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BankContext : DbContext
    {
        public BankContext()
        {
        }

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Depositor> Depositors { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeOperation> EmployeeOperations { get; set; }
    }
}
