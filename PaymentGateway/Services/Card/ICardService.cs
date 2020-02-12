using System;
using PaymentGateway.Models;

namespace PaymentGateway.Services.Card
{
    public interface ICardService
    {
        public Models.Card FindOrCreateCard(Models.Card card);
    }
}
