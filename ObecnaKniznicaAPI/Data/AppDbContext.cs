using Microsoft.EntityFrameworkCore;
using ObecnaKniznicaLogic.Models;
using System.Reflection.Metadata;

namespace ObecnaKniznicaAPI.Data
{
    public class AppDbContext : DbContext // vyuzitie EF Core
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        //public DbSet<Right> Rights { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>()
        //        .HasMany(e => e.Authors)                
        //        .WithMany(e => e.Books)
        //        .UsingEntity<Right>();
        //}
    }
}
