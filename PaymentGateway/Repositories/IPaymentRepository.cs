using PaymentGateway.Models;

namespace PaymentGateway.Repositories
{
    public interface IPaymentRepository
    {
        Payment RetrievePayment(int paymentId);
        Payment InsertPayment(Payment payment);
    }
}
