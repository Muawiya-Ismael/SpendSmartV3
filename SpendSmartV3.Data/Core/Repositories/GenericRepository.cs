using Microsoft.EntityFrameworkCore;
using SpendSmartV3.Data.Core.Interfaces;

namespace SpendSmartV3.Data.Core.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected SpendSmartDbContext _context;
        internal DbSet<T> DbSet { get; set; }

        public GenericRepository(SpendSmartDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public virtual Task<bool> AddEntity(T model)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> GetAllAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetAsync(string userId, int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateEntity(T model)
        {
            throw new NotImplementedException();
        }
    }
}
