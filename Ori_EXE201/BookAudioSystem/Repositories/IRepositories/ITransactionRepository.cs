using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Repositories.IRepositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionByIdAsync(string transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId);

        void Add(Transaction transaction);
        Task<int> SaveChangesAsync();
        void Update(Transaction transaction);

        Transaction GetByTransactionId(string transactionId);
    }

}
