using System;
namespace PaymentGateway.Models
{
    public class BankPaymentRequestDto
    {
        public Card Card { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
    }
}
