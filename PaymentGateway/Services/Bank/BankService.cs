using System;
using Microsoft.Extensions.Logging;
using PaymentGateway.Models;

namespace PaymentGateway.Services
{
    public class BankService : IBankService
    {
        public BankService(ILogger<BankService> logger)
        {
            _logger = logger;
        }

        ILogger<BankService> _logger;

        /// <summary>
        /// Posts a payment request to the bank. Currently the status is determined randomly until functionality is swapped out for a real bank API call.
        /// </summary>
        /// <param name="payment">The payment request</param>
        /// <returns>A BankPaymentResponseDto, which contains the response from the bank</returns>
        public BankPaymentResponseDto PostPayment(BankPaymentRequestDto payment)
        {
            _logger.LogInformation("Submitting payment request to bank for processsing.");

            Random random = new Random();

            BankPaymentResponseDto dto = new BankPaymentResponseDto();
            dto.BankTransactionId = random.Next(1000, 100000);

            bool successfulTransaction = random.Next(1, 10) < 8 ? true : false;

            if (!successfulTransaction)
            {
                dto.Reason = "Failure";
                dto.ReasonCode = -1;
            }
            else
            {
                dto.Reason = "Success";
                dto.ReasonCode = 1;
            }

            return dto;
        }
    }
}
