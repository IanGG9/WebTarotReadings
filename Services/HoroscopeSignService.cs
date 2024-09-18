using System.Globalization;
using WebTarotReadings.Models;

namespace WebTarotReadings.Services
{
    public class HoroscopeSignService
    {
        public readonly AppDbContext _context;
        public HoroscopeSignService(AppDbContext context)
        {
            _context = context;
        }
        public List<HoroscopeSignModel> GetHoroscopeSigns()
        {
            return _context.HoroscopeSigns.ToList();
        }

        public string GetHoroscopeSign(DateTime userBirthDate)
        {
            var horoscopeSigns = GetHoroscopeSigns();
            foreach (var sign in horoscopeSigns)
            {
                DateTime startDate = ParseDate(sign.StartDate);
                DateTime endDate = ParseDate(sign.EndDate);

                if (IsDateInRange(userBirthDate, startDate, endDate))
                {
                    return sign.Sign;
                }
            }

            return "Unknown"; 
        }

        private bool IsDateInRange(DateTime birthDate, DateTime startDate, DateTime endDate)
        {
            DateTime birthDateWithoutYear = new DateTime(2000, birthDate.Month, birthDate.Day);
            DateTime startDateWithoutYear = new DateTime(2000, startDate.Month, startDate.Day);
            DateTime endDateWithoutYear = new DateTime(2000, endDate.Month, endDate.Day);

            if (startDateWithoutYear > endDateWithoutYear)
            {
                return birthDateWithoutYear >= startDateWithoutYear || birthDateWithoutYear <= endDateWithoutYear;
            }
            else
            {
                return birthDateWithoutYear >= startDateWithoutYear && birthDateWithoutYear <= endDateWithoutYear;
            }
        }

        private DateTime ParseDate(string dateString)
        {
            return DateTime.ParseExact(dateString + "2000", "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
    }
}
