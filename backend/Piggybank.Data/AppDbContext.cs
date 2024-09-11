using Microsoft.EntityFrameworkCore;
using Piggybank.Models;

namespace Piggybank.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Supp configuration via Fluent API (if needed)
        }
    }
}
