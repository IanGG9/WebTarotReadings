using System.ComponentModel.DataAnnotations;

namespace WebTarotReadings.Models
{
    public class HoroscopeSignModel
    {
        [Key]public int Id { get; set; }
        public string Sign { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
