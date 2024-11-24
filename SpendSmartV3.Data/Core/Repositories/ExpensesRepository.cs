using Microsoft.EntityFrameworkCore;
using SpendSmartV3.Data.Core.Interfaces;
using SpendSmartV3.Objects.Models.Expense;

namespace SpendSmartV3.Data.Core.Repositories
{
    public class ExpensesRepository : GenericRepository<Expense>, IExpensesRepository
    {
        public ExpensesRepository(SpendSmartDbContext context) : base(context)
        {
        }

        public override async Task<bool> AddEntity(Expense model)
        {
            try
            {
                await DbSet.AddAsync(model);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public override Task<Expense> GetAsync(string userId, int id)
        {
            return DbSet.FirstOrDefaultAsync(e => e.UserId == userId && e.Id == id);
        }

        public override async Task<bool> UpdateEntity(Expense model)
        {
            try
            {
                var existingExpense = await DbSet.FirstOrDefaultAsync(e => e.Id == model.Id);
                if (existingExpense != null)
                {
                    existingExpense.Value = model.Value;
                    existingExpense.Discerption = model.Discerption;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<bool> DeleteEntity(int id)
        {
            try
            {
                var existingExpense = await DbSet.FirstOrDefaultAsync(e => e.Id == id);
                if (existingExpense != null)
                {
                    DbSet.Remove(existingExpense);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<List<Expense>> GetAllAsync(string userId)
        {
            return await DbSet.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
