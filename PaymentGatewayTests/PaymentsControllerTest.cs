using System;
using AutoMapper;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Controllers;
using PaymentGateway.Mappers;
using PaymentGateway.Repositories;
using PaymentGateway.Services;
using PaymentGateway.Services.Card;
using PaymentGateway.Models;
using PaymentGateway.Models.Dto.Payment;

namespace PaymentGatewayTests
{
    public class PaymentsControllerTest
    {
        public PaymentsControllerTest()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = mapperConfig.CreateMapper();

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _bankService = new BankService(loggerFactory.CreateLogger<BankService>());

            _cardRepository = new CardRepositoryInMemory();
            _cardService = new CardService(_cardRepository, loggerFactory.CreateLogger<CardService>());

            _paymentRepository = new PaymentRepositoryInMemory();
            _paymentService = new PaymentService(_paymentRepository, _cardService, _bankService, loggerFactory.CreateLogger<PaymentService>(), _mapper);
            _paymentsController = new PaymentsController(_paymentService, loggerFactory.CreateLogger<PaymentsController>());
        }

        PaymentsController _paymentsController;
        IPaymentService _paymentService;
        IPaymentRepository _paymentRepository;
        ICardService _cardService;
        ICardRepository _cardRepository;
        IBankService _bankService;
        IMapper _mapper;

        /// <summary>
        /// Test to see if we can post a payment successfully, and return back a payment id and status.
        /// </summary>
        [Fact]
        public void PostPayment_ShouldAccept()
        {
            CardDto card = GenerateRandomCard();

            PaymentRequestDto paymentRequest = new PaymentRequestDto();
            paymentRequest.MerchantId = 1;
            paymentRequest.Amount = 25.99;
            paymentRequest.Currency = "CAD";
            paymentRequest.Card = card;
            var okResult = _paymentsController.Post(paymentRequest);

            Assert.IsType<OkObjectResult>(okResult.Result);
            OkObjectResult objectResult = okResult.Result as OkObjectResult;

            Assert.IsType<PaymentResponseDto>(objectResult.Value);
            PaymentResponseDto paymentResponse = objectResult.Value as PaymentResponseDto;

            Assert.True(paymentResponse.Id > 0);
            Assert.NotNull(paymentResponse.Status);
        }

        /// <summary>
        /// Test to see if we can post a payment successfully, and retrieve information about the made payment.
        /// </summary>
        [Fact]
        public void PostAndGetPayment_ShouldAcceptAndRetrieve()
        {
            CardDto card = GenerateRandomCard();

            PaymentRequestDto paymentRequest = new PaymentRequestDto();
            paymentRequest.MerchantId = 1;
            paymentRequest.Amount = 25.99;
            paymentRequest.Currency = "CAD";
            paymentRequest.Card = card;

            var postResult = _paymentsController.Post(paymentRequest);

            Assert.IsType<OkObjectResult>(postResult.Result);
            OkObjectResult objectResult = postResult.Result as OkObjectResult;

            Assert.IsType<PaymentResponseDto>(objectResult.Value);
            PaymentResponseDto paymentResponse = objectResult.Value as PaymentResponseDto;

            Assert.True(paymentResponse.Id > 0);
            Assert.NotNull(paymentResponse.Status);

            int paymentId = paymentResponse.Id;
            var getResult = _paymentsController.Get(paymentId);

            Assert.IsType<OkObjectResult>(getResult.Result);
            OkObjectResult getObjectResult = getResult.Result as OkObjectResult;

            Assert.IsType<PaymentInformationDto>(getObjectResult.Value);
            PaymentInformationDto paymentInformation = getObjectResult.Value as PaymentInformationDto;

            Assert.Equal(paymentRequest.Amount, paymentInformation.Amount);
            Assert.Equal(paymentRequest.Currency, paymentInformation.Currency);
        }

        public CardDto GenerateRandomCard()
        {
            Random random = new Random();

            CardDto card = new CardDto();
            card.CardHolderName = "Test Test" + random.Next(100, 1000).ToString();
            card.CardNumber = "12341234" + random.Next(11111111, 99999999).ToString();
            card.CVV = random.Next(111, 999);
            card.ExpiryDate = random.Next(1111, 9999).ToString();

            return card;
        }
    }
}
