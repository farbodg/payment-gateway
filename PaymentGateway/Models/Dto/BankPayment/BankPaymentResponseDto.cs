using System;
namespace PaymentGateway.Models
{
    public class BankPaymentResponseDto
    {
        public int BankTransactionId { get; set; }
        public int ReasonCode { get; set; }
        public string Reason { get; set; }
    }
}
