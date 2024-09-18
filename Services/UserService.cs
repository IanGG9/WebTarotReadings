using WebTarotReadings.Models;
using WebTarotReadings.Services;

namespace WebTarotReadings.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly HoroscopeSignService _horoscopeService;
        public UserService(AppDbContext context)
        {
            _context = context;
            _horoscopeService = new HoroscopeSignService(_context);
        }

        public void CreateUser(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public UserModel GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public UserModel GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public UserModel Login(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public void AssignHoroscopeSign(UserModel user, List<HoroscopeSignModel> horoscopeSigns)
        {
            string horoscopeSign = _horoscopeService.GetHoroscopeSign(user.BirthDate);

            user.HoroscopeSign = horoscopeSign;
        }
    }
}
