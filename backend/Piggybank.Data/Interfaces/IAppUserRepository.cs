using Piggybank.Models;

namespace Piggybank.Data.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
    }
}
