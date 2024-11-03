using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Services.IService;
using Net.payOS.Types;
using Net.payOS;
using BookAudioSystem.Repositories.IRepositories;
using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.Repositories;

namespace BookAudioSystem.Services
{
    public class PayOsServices
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly PayOS _payOs;
        public PayOsServices(ITransactionRepository transactionRepository, IUserService userService, IOrderService orderService,PayOS payOs)
        {
            _transactionRepository = transactionRepository;
            _userService = userService;
            _orderService = orderService;
            _payOs = payOs;
        }

        public async Task<string> CreatePayment(PaymentRequest model)
        {
            // First get the order to use its price
            var order = await _orderService.GetOrderByIdAsync(model.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            string txnRef = GenerateTransactionId();
            var transaction = new BusinessObjects.Entities.Transaction
            {
                TransactionID = txnRef,
                UserID = model.UserId,
                BookID = order.BookID,
                OrderId = model.OrderId,
                TransactionDate = DateTime.Now,
                Amount = order.Price,    // Use the price from order
                Status = 1
            };

            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveChangesAsync();

            long expiredAt = (long)(DateTime.UtcNow.AddMinutes(10) - new DateTime(1970, 1, 1)).TotalSeconds;

            var paymentData = new PaymentData(
                orderCode: long.Parse(txnRef.Substring(5)),
                amount: (int)order.Price,    // Use the price from order
                description: $"Payment for Book Order #{model.OrderId}",
                items: new List<ItemData>(),
                cancelUrl: "https://dev.fancy.io.vn/paymen-failed/",
                returnUrl: "https://dev.fancy.io.vn/payment-page/",
                expiredAt: expiredAt
            );

            var createPaymentResult = await _payOs.createPaymentLink(paymentData);
            return createPaymentResult.checkoutUrl;
        }

        public async Task<int> ProcessPaymentResponse(WebhookType webhookBody)
        {
            var verifiedData = _payOs.verifyPaymentWebhookData(webhookBody); // Verify webhook data
            string responseCode = verifiedData.code;
            string transactionId = "TRANS" + verifiedData.orderCode;

            var transaction = _transactionRepository.GetByTransactionId(transactionId);

            if (transaction != null)
            {
                transaction.Status = responseCode == "00" ? 1 : 3; // Success or Failed
                _transactionRepository.Update(transaction);
                await _transactionRepository.SaveChangesAsync();

                if (responseCode == "00")
                {
                    var user = await _userService.GetUserByIdAsync(transaction.UserID); // Ensure this is async
                    if (user != null)
                    {
                        await _userService.UpdateWalletBalanceAsync(transaction.UserID, transaction.Amount / 1000);
                        return 0; // 0 for successful payment
                    }
                }
            }
            return int.Parse(responseCode); // Return error code or failure
        }

        private string GenerateTransactionId()
        {
            // Implementation for generating transaction ID
            return $"TRANS{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
    }
}
