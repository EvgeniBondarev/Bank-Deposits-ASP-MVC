using Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class BankContext : DbContext
    {
        private readonly string _connectionString = "Server=DESKTOP-QAU182Q\\SQLEXPRESS;Database=BankDeposits;Trusted_Connection=True; TrustServerCertificate=True;";

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public BankContext()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
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
