using Microsoft.EntityFrameworkCore;
using Piggybank.Data.Interfaces;
using Piggybank.Models;

namespace Piggybank.Data.Repositories
{
    /// <summary>
    /// Implements the <see cref="IAppUserRepository"/> interface to provide access to <see cref="AppUser"/> entities using Entity Framework.
    /// </summary>
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for accessing <see cref="AppUser"/> entities.</param>
        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }
    }
}
