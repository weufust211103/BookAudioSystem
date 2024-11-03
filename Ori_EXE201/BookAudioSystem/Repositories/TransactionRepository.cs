using BookAudioSystem.BusinessObjects;
using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookAudioSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly RentalBookDbContext _context;

        public TransactionRepository(RentalBookDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> GetTransactionByIdAsync(string transactionId)
        {
            return await _context.Transactions
                .Include(t => t.Book)
                .Include(t => t.User)
                .Include(t => t.Order)
                .FirstOrDefaultAsync(t => t.TransactionID == transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserID == userId)
                .ToListAsync();
        }

        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
        }

        public Transaction GetByTransactionId(string transactionId)
        {
            return _context.Transactions.FirstOrDefault(t => t.TransactionID == transactionId);
        }
    }
}
