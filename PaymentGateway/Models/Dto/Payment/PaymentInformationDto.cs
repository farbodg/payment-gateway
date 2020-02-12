using System;
using PaymentGateway.Models.Dto.Card;

namespace PaymentGateway.Models
{
    public class PaymentInformationDto
    {
        public string Currency { get; set; }
        public double Amount { get; set; }
        public MaskedCardDto Card { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
