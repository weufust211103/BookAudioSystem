using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects.Models;

namespace BookAudioSystem.Services.IService
{
    public interface IOrderService
    {

        Task<OrderDetailDto> GetOrderDetailAsync(int orderId);
        Task<OrderModel> CreateOrderAsync(int bookId, int buyerId);

        Task<bool> UpdateOrderDetailsAsync(UpdateOrderModel model);

        Task<IEnumerable<OrderModel>> GetOrdersForUserAsync(int userId);

        Task<OrderModel> GetOrderByIdAsync(int orderId);

    }
}
