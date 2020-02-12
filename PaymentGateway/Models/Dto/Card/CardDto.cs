using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public class CardDto
    {
        [Required]
        [MinLength(15)]
        public string CardNumber { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string ExpiryDate { get; set; }

        [Required]
        [Range(100, 999)]
        public int CVV { get; set; }
    }
}
