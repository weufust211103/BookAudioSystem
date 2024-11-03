using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Services.IService
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(int bookId, int userId, int ownerId, decimal amount);
        Task<Transaction> GetTransactionDetailsAsync(string transactionId);
        Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId);

    }
}
