using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public int CVV { get; set; }
    }
}
