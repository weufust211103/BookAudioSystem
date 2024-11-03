using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.Services;
using BookAudioSystem.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAudioSystem.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IVNPayService _vnPayService;
        private readonly IBookService _bookService;
        private readonly IOrderService _orderService;

        public TransactionController(ITransactionService transactionService, IVNPayService vnPayService, IBookService bookService, IOrderService orderService)
        {
            _transactionService = transactionService;
            _vnPayService = vnPayService;
            _bookService = bookService;
            _orderService = orderService;
        }

        [HttpPost("create-vnpay-qr")]
        public async Task<IActionResult> CreateVNPayQR(int orderId)
        {
            // Retrieve userId from JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            // Parse the userId correctly
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("User ID is not in a correct format.");
            }

            // Retrieve the order details
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Retrieve the ownerId using the bookId from the order
            var ownerId = await _bookService.GetOwnerIdByBookIdAsync(order.BookID);
            if (ownerId == null)
            {
                return NotFound("Owner not found for the specified book.");
            }

            // Use the amount from the order
            decimal amount = order.Price;

            // Create the transaction
            var result = await _transactionService.CreateTransactionAsync(order.BookID, userId, ownerId.Value, amount);
            if (result == null)
            {
                return BadRequest("Transaction creation failed.");
            }

            var qrCodeUrl = _vnPayService.GenerateVNPayQRCodeUrl(result);
            return Ok(new { TransactionId = result.TransactionID, QRCodeUrl = qrCodeUrl });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(int bookId, decimal amount)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }
            int userId = int.Parse(userIdClaim.Value);

            var ownerId = await _bookService.GetOwnerIdByBookIdAsync(bookId);
            if (ownerId == null)
            {
                return NotFound("Owner not found for the specified book.");
            }

            var transaction = await _transactionService.CreateTransactionAsync(bookId, userId, ownerId.Value, amount);
            if (transaction == null)
            {
                return BadRequest("Transaction creation failed.");
            }

            return Ok(transaction);
        }
    }



}

