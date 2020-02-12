using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public class PaymentRequestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int MerchantId { get; set; }

        [Required]
        public CardDto Card { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Currency { get; set; }

        [Required]
        [Range(0.01, 100000000.00)]
        public double Amount { get; set; }
    }
}
