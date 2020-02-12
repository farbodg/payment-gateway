using Microsoft.Extensions.Logging;
using PaymentGateway.Repositories;
using PaymentGateway.Services.Card;

namespace PaymentGateway.Services
{
    public class CardService : ICardService
    {
        public CardService(ICardRepository repository, ILogger<CardService> logger)
        {
            _logger = logger;
            _cardsRepository = repository;
        }

        ILogger<CardService> _logger;
        ICardRepository _cardsRepository;

        /// <summary>
        /// Finds or creates a card entry.
        /// </summary>
        /// <param name="card">The requested card</param>
        /// <returns>An existing or new card entry.</returns>
        public Models.Card FindOrCreateCard(Models.Card card)
        {
            Models.Card cardResult = _cardsRepository.FindCardByNumberAndExpiry(card.CardNumber, card.ExpiryDate);

            if (cardResult != null)
            {
                _logger.LogInformation("Card found!");

                return cardResult;
            }
            else
            {
                _logger.LogInformation("Card not found in database, creating new card entry.");

                return _cardsRepository.InsertCard(card);
            }
        }
    }
}
