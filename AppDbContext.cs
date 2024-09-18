using Microsoft.EntityFrameworkCore;
using WebTarotReadings.Models;

namespace WebTarotReadings
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TarotCardsModel> TarotCards { get; set; }
        public DbSet<HoroscopeSignModel> HoroscopeSigns { get; set; }
    }
}
