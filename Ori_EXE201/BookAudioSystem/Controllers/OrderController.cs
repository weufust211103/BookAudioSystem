using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAudioSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Renter")]
        public async Task<IActionResult> CreateOrder([FromBody] int bookId) // Accept BookID as a simple int
        {
            var buyerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BuyerID");
            if (buyerIdClaim == null || !int.TryParse(buyerIdClaim.Value, out int buyerId))
            {
                return Unauthorized("Buyer ID not found in token or is not valid.");
            }


            try
            {
                // Create the order
                var orderResponse = await _orderService.CreateOrderAsync(bookId, buyerId);
                return CreatedAtAction(nameof(CreateOrder), new { id = orderResponse.OrderID }, orderResponse);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var orderDetail = await _orderService.GetOrderDetailAsync(orderId);
            if (orderDetail == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(orderDetail);
        }

        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateOrderDetails([FromBody] UpdateOrderModel model)
        {
            try
            {
                var success = await _orderService.UpdateOrderDetailsAsync(model);
                if (!success)
                {
                    return StatusCode(500, "An error occurred while updating the order.");
                }
                return Ok("Order and user information updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var orders = await _orderService.GetOrdersForUserAsync(userId);
            return Ok(orders);
        }
    }
}
