using System.Linq;
using System.Collections.Generic;
using PaymentGateway.Models;
using PaymentGateway.Repositories;

namespace PaymentGatewayTests
{
    public class CardRepositoryInMemory : ICardRepository
    {
        public CardRepositoryInMemory()
        {
            _cardList = new List<Card>();
        }

        private readonly List<Card> _cardList;
        private int id = 1;

        public Card FindCardByNumber(string number)
        {
            return _cardList.FirstOrDefault(c => c.CardNumber == number);
        }

        public Card FindCardByNumberAndExpiry(string number, string expiry)
        {
            return _cardList.FirstOrDefault(c => c.CardNumber == number && c.ExpiryDate == expiry);
        }

        public Card InsertCard(Card card)
        {
            card.Id = id++;
            _cardList.Add(card);
            return card;
        }
    }
}
