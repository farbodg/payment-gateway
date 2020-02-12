using System.Linq;
using System.Collections.Generic;
using PaymentGateway.Models;
using PaymentGateway.Repositories;

namespace PaymentGatewayTests
{
    public class PaymentRepositoryInMemory : IPaymentRepository
    {
        public PaymentRepositoryInMemory()
        {
        }

        private readonly List<Payment> _paymentList = new List<Payment>();
        private int id = 1;

        public Payment RetrievePayment(int paymentId)
        {
            return _paymentList.FirstOrDefault(p => p.Id == paymentId);
        }

        public Payment InsertPayment(Payment payment)
        {
            payment.Id = id++;
            _paymentList.Add(payment);
            return payment;
        }
    }
}
