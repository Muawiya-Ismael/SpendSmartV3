using SpendSmartV3.Data.Core.Interfaces;

namespace SpendSmartV3.Data.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IExpensesRepository expensesRepository { get; private set; }

        private readonly SpendSmartDbContext _context;

        public UnitOfWork(SpendSmartDbContext context)
        {
            _context = context;
            expensesRepository = new ExpensesRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
