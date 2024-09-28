using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Piggybank.Models;

namespace Piggybank.Data
{
    /// <summary>
    /// Represents the application's database context, extending the <see cref="IdentityDbContext{TUser, TRole, TKey}"/> for <see cref="AppUser"/> and <see cref="AppRole"/>.
    /// </summary>
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }

        /// <summary>
        /// Configures the model during creation of the database schema.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> being used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
