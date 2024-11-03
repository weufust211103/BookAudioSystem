using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Repositories.IRepositories;
using BookAudioSystem.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace BookAudioSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository; // Ensure you have a repository to get book details

        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
        }

        public async Task<OrderModel> CreateOrderAsync(int bookId, int buyerId)
        {
            // Fetch the book details
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                throw new ArgumentException("Book not found.");
            }
            string orderStatus;
            if (book.Status == "For Sale")
            {
                orderStatus = "Buying";
            }
            else if (book.Status == "For Rent")
            {
                orderStatus = "Renting";
            }
            else
            {
                throw new InvalidOperationException("Invalid book status for creating an order.");
            }
            // Create the order
            var order = new Order
            {
                BookID = bookId,
                BuyerID = buyerId,
                Price = book.Price,
                OrderStatus = book.Status, // Use the book status or define your own order status
                OrderDate = DateTime.UtcNow // Set the transaction date to now
            };

            // Save the order to the database
            await _orderRepository.AddOrderAsync(order);

            // Map to OrderModel to return
            return new OrderModel
            {
                OrderID = order.OrderID,
                BookID = order.BookID,
                BuyerID = buyerId,
                OrderStatus = order.OrderStatus,
                Price = book.Price,
                OrderDate = order.OrderDate
            };
        }
        public async Task<OrderDetailDto> GetOrderDetailAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return null;
            }

            return new OrderDetailDto
            {
                OrderID = order.OrderID,
                BookID = order.BookID,
                OrderStatus = order.OrderStatus,
                Price = order.Price,
                OrderDate = order.OrderDate,
                Buyer = new BuyerInfoDto
                {
                    Email = order.Buyer.Email,
                    FullName = order.Buyer.FullName,
                    BirthDate = order.Buyer.birthDate,
                    PhoneNumber = order.Buyer.PhoneNumber,
                    Address = order.Buyer.Address,
                    Ward = order.Buyer.Ward,
                    District = order.Buyer.District,
                    Province = order.Buyer.Province
                }
            };
        }

        public async Task<bool> UpdateOrderDetailsAsync(UpdateOrderModel model)
        {
            // Get the order with the buyer details
            var order = await _orderRepository.GetOrderByIdAsync(model.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            // Update the order’s buyer details
            order.Buyer.PhoneNumber = model.PhoneNumber;
            order.Buyer.Address = model.Address;
            order.Buyer.Ward = model.Ward;
            order.Buyer.District = model.District;
            order.Buyer.Province = model.Province;

            // Save updates to both Order and User tables
            return await _orderRepository.UpdateOrderAsync(order);
        }


        public async Task<IEnumerable<OrderModel>> GetOrdersForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            return orders.Select(o => new OrderModel
            {
                OrderID = o.OrderID,
                BookID = o.BookID,
                BuyerID = o.BuyerID,
                OrderStatus = o.OrderStatus,
                Price = o.Price,
                OrderDate = o.OrderDate
            }).ToList();
        }

        public async Task<OrderModel> GetOrderByIdAsync(int orderId)
        {
            // Retrieve order from the repository
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            // Map to OrderModel (you might need a mapper)
            return new OrderModel
            {
                OrderID = order.OrderID,
                BookID = order.BookID,
                BuyerID = order.BuyerID,
                OrderStatus = order.OrderStatus,
                Price = order.Price,
                OrderDate = order.OrderDate
            };
        }
    }
}
