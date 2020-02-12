using System.Linq;
using PaymentGateway.Data;
using PaymentGateway.Models;

namespace PaymentGateway.Repositories
{
    public class CardRepository : ICardRepository
    {
        PaymentGatewayContext _context;

        public CardRepository(PaymentGatewayContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Finds a card with a matching card number
        /// </summary>
        /// <param name="cardNumber">The card number</param>
        /// <returns>The matched card, if found, otherwise null</returns>
        public Card FindCardByNumber(string cardNumber)
        {
            return _context.Card.Where(c => c.CardNumber == cardNumber).FirstOrDefault();
        }

        /// <summary>
        /// Finds a card with a matching card number and expiry date
        /// </summary>
        /// <param name="cardNumber">The card number</param>
        /// <param name="expiryDate">The expiry date</param>
        /// <returns>The matched card, if found, otherwise null</returns>
        public Card FindCardByNumberAndExpiry(string cardNumber, string expiryDate)
        {
            return _context.Card.Where(c => c.CardNumber == cardNumber && c.ExpiryDate == expiryDate).FirstOrDefault();
        }

        /// <summary>
        /// Inserts a new card entry
        /// </summary>
        /// <param name="card">The card</param>
        /// <returns>The card with the identifier</returns>
        public Card InsertCard(Card card)
        {
            Card newCard = _context.Card.Add(card).Entity;
            _context.SaveChanges();

            return newCard;
        }
    }
}
