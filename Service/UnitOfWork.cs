using Repository;
using Repository.Repositories.Implementation;

namespace Service
{
    public class UnitOfWork : IDisposable
    {
        private readonly BankContext _context;
        private DepositRepository _depositRepository;
        private CurrencyRepository _currencyRepository;
        private ExchangeRateRepository _exchangeRateRepository;
        private DepositorRepository _depositorRepository;
        private OperationRepository _operationRepository;
        private EmployeeRepository _employeeRepository;
        private EmployeeOperationRepository _employeeOperationRepository;

        public UnitOfWork(BankContext context)
        {
            _context = context;
        }

        public DepositRepository Deposits
        {
            get
            {
                if (_depositRepository == null)
                    _depositRepository = new DepositRepository(_context);
                return _depositRepository;
            }
        }

        public CurrencyRepository Currencies
        {
            get
            {
                if (_currencyRepository == null)
                    _currencyRepository = new CurrencyRepository(_context);
                return _currencyRepository;
            }
        }

        public ExchangeRateRepository ExchangeRates
        {
            get
            {
                if (_exchangeRateRepository == null)
                    _exchangeRateRepository = new ExchangeRateRepository(_context);
                return _exchangeRateRepository;
            }
        }

        public DepositorRepository Depositors
        {
            get
            {
                if (_depositorRepository == null)
                    _depositorRepository = new DepositorRepository(_context);
                return _depositorRepository;
            }
        }

        public OperationRepository Operations
        {
            get
            {
                if (_operationRepository == null)
                    _operationRepository = new OperationRepository(_context);
                return _operationRepository;
            }
        }

        public EmployeeRepository Employees
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_context);
                return _employeeRepository;
            }
        }

        public EmployeeOperationRepository EmployeeOperations
        {
            get
            {
                if (_employeeOperationRepository == null)
                    _employeeOperationRepository = new EmployeeOperationRepository(_context);
                return _employeeOperationRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
