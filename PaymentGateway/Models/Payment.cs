using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Models
{
    public enum PaymentStatus
    {
        Accepted,
        Success,
        Invalid,
        Failed
    }

    public class Payment
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public Card Card { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public int BankTransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
