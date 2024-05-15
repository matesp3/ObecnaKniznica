using Microsoft.EntityFrameworkCore;
using ObecnaKniznicaAPI.Models;

namespace ObecnaKniznicaAPI.Data
{
    public class AppDbContext : DbContext // vyuzitie EF Core
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
