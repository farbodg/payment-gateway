using PaymentGateway.Models;
using PaymentGateway.Models.Dto.Payment;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        PaymentInformationDto GetPaymentInformation(int paymentId);
        PaymentResponseDto MakePayment(PaymentRequestDto paymentDto);
    }
}
