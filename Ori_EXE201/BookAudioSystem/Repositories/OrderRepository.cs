using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects;
using BookAudioSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookAudioSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RentalBookDbContext _context;

        public OrderRepository(RentalBookDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Book)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            _context.Users.Update(order.Buyer); // Update related user details
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Book)
                .Where(o => o.BuyerID == userId)
                .ToListAsync();
        }
    }
}
