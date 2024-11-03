using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;

namespace BookAudioSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOsController : ControllerBase
    {
        private readonly PayOsServices _payOsServices;
        private readonly ILogger<PayOsController> _logger;

        public PayOsController(PayOsServices payOsServices, ILogger<PayOsController> logger)
        {
            _payOsServices = payOsServices;
            _logger = logger;
        }

        #region Create Payment URL For PayOs
        /// <summary>
        /// Create Payment URL For PayOs
        /// </summary>
        [HttpPost("payOs")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] PaymentRequest model)
        {
            if (model == null)
            {
                return BadRequest("Invalid payment request.");
            }

            try
            {
                string paymentLink = await _payOsServices.CreatePayment(model);
                return Ok(new { Url = paymentLink });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating payment link: {0}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Webhook
        /// <summary>
        /// Data from Webhook
        /// </summary>
        [HttpPost("hook")]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookType webhookBody)
        {
            try
            {
                int responseCode = await _payOsServices.ProcessPaymentResponse(webhookBody);

                if (responseCode == 0) // Assuming 0 means success
                {
                    return Ok(new { Message = "Webhook processed successfully" });
                }

                return BadRequest(new { Message = "Webhook processing failed.", Code = responseCode });
            }
            catch (Exception ex)
            {
                _logger.LogError("Webhook processing error: {0}", ex.ToString());
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion
    }
}
