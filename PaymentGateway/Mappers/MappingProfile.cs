using AutoMapper;
using PaymentGateway.Models;
using PaymentGateway.Models.Dto.Card;
using PaymentGateway.Models.Dto.Payment;

namespace PaymentGateway.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CardDto, Card>();
            CreateMap<Card, MaskedCardDto>()
                .ForSourceMember(dest => dest.Id, opt => opt.DoNotValidate())
                .ForMember(des => des.CardNumber, opt => opt.MapFrom(src => "XXXXXXXXXXXX" + src.CardNumber.Substring(src.CardNumber.Length - 4, 4)));
            CreateMap<PaymentRequestDto, Payment>();
            CreateMap<Payment, PaymentResponseDto>();
            CreateMap<Payment, PaymentInformationDto>();
            CreateMap<Payment, BankPaymentRequestDto>()
                .ForSourceMember(dest => dest.Id, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.BankTransactionId, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Status, opt => opt.DoNotValidate());
        }
    }
}
