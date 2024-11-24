using SpendSmartV3.Objects.Models.Expense;

namespace SpendSmartV3.Services
{
    public interface IExpenseServices
    {
        Task<List<Expense>> GetAllExpenses(string userId);
        Task<Expense> GetExpense(string userId, int id);
        Task<bool> AddExpense(Expense model);
        Task<bool> UpdateExpense(Expense model);
        Task<bool> DeleteExpense(int id);
        Task Commit();
    }
}
