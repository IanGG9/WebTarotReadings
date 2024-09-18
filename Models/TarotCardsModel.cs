using System.ComponentModel.DataAnnotations;

namespace WebTarotReadings.Models
{
	public class TarotCardsModel
	{
		[Key]public int Id { get; set; }
		public string CardName { get; set; }
		public bool IsReversed { get; set; }
		public string Meaning { get; set; }
	}
}
