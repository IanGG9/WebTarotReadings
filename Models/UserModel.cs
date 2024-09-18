using System.ComponentModel.DataAnnotations;

namespace WebTarotReadings.Models
{
    public class UserModel
    {
        [Key] public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string? HoroscopeSign { get; set; }
    }
}
