using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.Services.IService;
using System.Security.Cryptography;
using System.Text;

namespace BookAudioSystem.Services
{
    public class VNPayService : IVNPayService
    {
        private readonly IConfiguration _configuration;

        public VNPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateVNPayQRCodeUrl(Transaction transaction)
        {
            var vnPayBaseUrl = _configuration["VNPay:BaseUrl"];
            var secretKey = _configuration["VNPay:SecretKey"];
            var vnp_TxnRef = transaction.TransactionID.ToString();
            var vnp_Amount = ((int)(transaction.Amount * 100)).ToString();
            var vnp_OrderInfo = Uri.EscapeDataString($"PaymentForOrder{transaction.TransactionID}");

            // Construct query with proper encoding
            var query = $"vnp_TxnRef={vnp_TxnRef}&vnp_Amount={vnp_Amount}&vnp_OrderInfo={vnp_OrderInfo}";

            // Generate checksum
            var secureHash = GenerateSignature(query, secretKey);
            query += $"&vnp_SecureHash={secureHash}";

            return $"{vnPayBaseUrl}?{query}";
        }

        private string GenerateSignature(string data, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using var hmac = new HMACSHA512(keyBytes);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var hashBytes = hmac.ComputeHash(dataBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

    }
}
