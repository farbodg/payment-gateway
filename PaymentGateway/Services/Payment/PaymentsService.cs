using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PaymentGateway.Models;
using PaymentGateway.Models.Dto.Payment;
using PaymentGateway.Repositories;
using PaymentGateway.Services.Card;

namespace PaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// PaymentService constructor
        /// </summary>
        /// <param name="paymentRepository">IPaymentRepository instance</param>
        /// <param name="cardService">ICardService instance</param>
        /// <param name="bankService">IBankService instance</param>
        /// <param name="logger">ILogger instance</param>
        /// <param name="mapper">IMapper instance</param>
        public PaymentService(IPaymentRepository paymentRepository, ICardService cardService, IBankService bankService, ILogger<PaymentService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _bankService = bankService;
            _cardService = cardService;
        }

        private readonly ILogger _logger;
        IMapper _mapper;
        IPaymentRepository _paymentRepository;
        IBankService _bankService;
        ICardService _cardService;

        /// <summary>
        /// Retrieves details for a payment using a payment identifier.
        /// </summary>
        /// <param name="paymentId">The identifier of the payment.</param>
        /// <returns>A PaymentInformationDto, which contains information about the payment.</returns>
        public PaymentInformationDto GetPaymentInformation(int paymentId)
        {
            _logger.LogInformation("Retrieving information about payment #" + paymentId.ToString());

            Payment payment = _paymentRepository.RetrievePayment(paymentId);
            PaymentInformationDto paymentResponseDto = _mapper.Map<PaymentInformationDto>(payment);

            return paymentResponseDto;
        }

        /// <summary>
        /// Processes a payment request, verifying payment details with the bank and returning the result of the payment request
        /// </summary>
        /// <param name="paymentDto">The DTO representing a PaymentRequestDto</param>
        /// <returns>A PaymentResponseDto, which contains the payment identifier and status.</returns>
        public PaymentResponseDto MakePayment(PaymentRequestDto paymentDto)
        {
            _logger.LogInformation("Received a Payment request for " + paymentDto.Amount.ToString() + " " + paymentDto.Currency + ".");

            Payment payment = _mapper.Map<Payment>(paymentDto);
            payment.Card = _cardService.FindOrCreateCard(payment.Card);

            BankPaymentRequestDto bankPaymentRequest = _mapper.Map<BankPaymentRequestDto>(payment);
            BankPaymentResponseDto bankPaymentResponse = _bankService.PostPayment(bankPaymentRequest);

            if (bankPaymentResponse.ReasonCode == -1)
            {
                _logger.LogInformation("Payment rejected by bank.");

                payment.Status = PaymentStatus.Failed;
            }
            else
            {
                _logger.LogInformation("Payment accepted by bank!");

                payment.Status = PaymentStatus.Success;
            }

            payment.BankTransactionId = bankPaymentResponse.BankTransactionId;
            payment.PaymentDate = DateTime.Now;
            Payment newPayment = _paymentRepository.InsertPayment(payment);

            _logger.LogInformation("Payment successfully processed.");

            PaymentResponseDto newPaymentDto = _mapper.Map<PaymentResponseDto>(newPayment);

            return newPaymentDto;
        }
    }
}
