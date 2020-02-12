using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Models;
using PaymentGateway.Models.Dto.Payment;
using PaymentGateway.Services;

namespace PaymentGateway.Controllers
{
    /// <summary>
    /// Contains GET and POST methods for retrieving payment information and submitting payment requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger _logger;
        IPaymentService _paymentService;

        /// <summary>
        /// PaymentsController constructor
        /// </summary>
        /// <param name="service">IPaymentService</param>
        /// <param name="logger">ILogger</param>
        public PaymentsController(IPaymentService service, ILogger<PaymentsController> logger)
        {
            _paymentService = service;
            _logger = logger;
        }

        /// <summary>
        /// Temporary endpoint to ensure API is up and running.
        /// </summary>
        /// <returns>String representing a heartbeat check.</returns>
        [HttpGet]
        public string Get()
        {
            return "Payments API Running";
        }

        /// <summary>
        /// Retrieve payment details using a payment identifier.
        /// </summary>
        /// <param name="id">The payment identifier</param>
        /// <returns>A PaymentInformationDto, which contains information about the payment.</returns>
        [HttpGet("{id}")]
        public ActionResult<PaymentInformationDto> Get(int id)
        {
            PaymentInformationDto payment = _paymentService.GetPaymentInformation(id);

            if (payment == null)
            {
                _logger.LogInformation("Payment with id #" + id.ToString() + " not found.");

                return NotFound();
            }

            return Ok(payment);
        }

        // Submit a new payment request.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>A PaymentResponseDto, which contains the payment identifier and status.</returns>
        [HttpPost]
        public ActionResult<PaymentResponseDto> Post([FromBody]PaymentRequestDto payment)
        {
            PaymentResponseDto result = _paymentService.MakePayment(payment);

            return Ok(result);
        }
    }
}
