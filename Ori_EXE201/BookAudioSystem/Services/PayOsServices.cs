using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Services.IService;
using Net.payOS.Types;
using Net.payOS;
using BookAudioSystem.Repositories.IRepositories;

namespace BookAudioSystem.Services
{
    public class PayOsServices
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserService _userService;
        private readonly PayOS _payOs;
        public PayOsServices(ITransactionRepository transactionRepository, IUserService userService, PayOS payOs)
        {
            _transactionRepository = transactionRepository;
            _userService = userService;
            _payOs = payOs;
        }

        public async Task<string> CreatePayment(PaymentRequest model)
        {
            string txnRef = GenerateTransactionId();
            var transaction = new BusinessObjects.Entities.Transaction
            {
                UserID = model.UserId,
                TransactionDate = DateTime.Now,
                Amount = (int)model.Amount
            };

            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveChangesAsync(); // Ensure you await this for async

            long expiredAt = (long)(DateTime.UtcNow.AddMinutes(10) - new DateTime(1970, 1, 1)).TotalSeconds;

            var paymentData = new PaymentData(
                orderCode: long.Parse(txnRef.Substring(5)),
                amount: (int)model.Amount,
                description: $"Deposit {model.Amount} into wallet",
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
