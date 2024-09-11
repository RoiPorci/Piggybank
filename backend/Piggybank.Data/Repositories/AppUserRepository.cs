using Microsoft.EntityFrameworkCore;
using Piggybank.Data.Interfaces;
using Piggybank.Models;

namespace Piggybank.Data.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }
    }
}
