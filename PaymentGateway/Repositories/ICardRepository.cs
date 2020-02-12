using PaymentGateway.Models;

namespace PaymentGateway.Repositories
{
    public interface ICardRepository
    {
        Card FindCardByNumber(string cardNumber);
        Card FindCardByNumberAndExpiry(string cardNumber, string expiryDate);
        Card InsertCard(Card card);
    }
}
