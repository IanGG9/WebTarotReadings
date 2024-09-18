using WebTarotReadings.Models;

namespace WebTarotReadings.Services
{
	public class TarotCardsService
	{
		public readonly AppDbContext _context;
		private List<int> _cardIds = new();
		private readonly Random _random = new Random();

		public TarotCardsService(AppDbContext context)
		{
			_context = context;
		}

		public TarotCardsModel GetTarotCard(int cardId)
		{
			return _context.TarotCards.FirstOrDefault(s => s.Id == cardId);
		}

		public string GetCardMeaning(int id)
		{
			var card = _context.TarotCards.FirstOrDefault(s => s.Id == id);
			_cardIds.Add(card.Id);
			return card.Meaning;
		}

		public string GetCardName(int id)
		{
			var card = _context.TarotCards.FirstOrDefault(c => c.Id == id);
			if (card.IsReversed)
			{
				return card.CardName;
			}
			else
			{
				return card.CardName + " reversed";
			}
		}

		public Dictionary<string, string> GenerateNewCards()
		{
			var cards = new Dictionary<string, string>();
            if (_cardIds.Count > 98)
            {
                _cardIds.Clear();
            }
            while (cards.Count < 10)
			{
				int id = _random.Next(1, 108);
				if (!_cardIds.Contains(id)) {
					cards.Add(GetCardName(id), GetCardMeaning(id));
					_cardIds.Add(id);
				}
			}
			return cards;
		}

	}
}
