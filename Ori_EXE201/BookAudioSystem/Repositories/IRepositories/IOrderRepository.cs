using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        // Other order-related methods can be added here

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<bool> UpdateOrderAsync(Order order);

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
