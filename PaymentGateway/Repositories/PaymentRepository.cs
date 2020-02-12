using System.Linq;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Data;
using PaymentGateway.Models;

namespace PaymentGateway.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        PaymentGatewayContext _context;

        public PaymentRepository(PaymentGatewayContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves information about a payment using the identifier
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <returns></returns>
        public Payment RetrievePayment(int paymentId)
        {
            return _context.Payment.Include(p => p.Card).FirstOrDefault(p => p.Id == paymentId);
        }

        /// <summary>
        /// Inserts a new payment entry
        /// </summary>
        /// <param name="payment">The payment</param>
        /// <returns>A payment with the identifier</returns>
        public Payment InsertPayment(Payment payment)
        {
            Payment newPayment = _context.Add(payment).Entity;
            _context.SaveChanges();

            return newPayment;
        }
    }
}
