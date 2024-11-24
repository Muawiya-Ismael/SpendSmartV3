using SpendSmartV3.Data.Core.Interfaces;
using SpendSmartV3.Objects.Models.Expense;

namespace SpendSmartV3.Services.Account
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddExpense(Expense model)
        {
            return await _unitOfWork.expensesRepository.AddEntity(model);
        }

        public async Task<bool> DeleteExpense(int id)
        {
            return await _unitOfWork.expensesRepository.DeleteEntity(id);
        }

        public async Task<List<Expense>> GetAllExpenses(string userId)
        {
            return await _unitOfWork.expensesRepository.GetAllAsync(userId);
        }

        public async Task<Expense> GetExpense(string userId, int id)
        {
            return await _unitOfWork.expensesRepository.GetAsync(userId, id);
        }

        public async Task<bool> UpdateExpense(Expense model)
        {
            return await _unitOfWork.expensesRepository.UpdateEntity(model);
        }

        public async Task Commit()
        {
            await _unitOfWork.CompleteAsync();
        }
    }
}
