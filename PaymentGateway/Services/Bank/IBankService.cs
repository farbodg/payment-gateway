using PaymentGateway.Models;

namespace PaymentGateway.Services
{
    public interface IBankService
    {
        BankPaymentResponseDto PostPayment(BankPaymentRequestDto payment);
    }
}
