namespace SpendSmartV3.Data.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IExpensesRepository expensesRepository { get; }

        Task CompleteAsync();

        void Dispose();
    }
}
