using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.Repositories.IRepositories;
using BookAudioSystem.Services.IService;

namespace BookAudioSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public TransactionService(ITransactionRepository transactionRepository, IBookRepository bookRepository, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }


        public async Task<Transaction> CreateTransactionAsync(int bookId, int userId, int orderId, decimal amount)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            var user = await _userRepository.GetUserByIdAsync(userId);
            var order = await _userRepository.GetUserByIdAsync(orderId);

            if (book == null || user == null || order == null)
            {
                throw new Exception("Invalid book, user, or owner.");
            }

            var transaction = new Transaction
            {
                BookID = bookId,
                UserID = userId,
                OrderId = orderId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow
            };

            return await _transactionRepository.CreateTransactionAsync(transaction);
        }

        public async Task<Transaction> GetTransactionDetailsAsync(string transactionId)
        {
            return await _transactionRepository.GetTransactionByIdAsync(transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId)
        {
            return await _transactionRepository.GetTransactionsByUserIdAsync(userId);
        }
    }


}
