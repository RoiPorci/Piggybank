using Piggybank.Models;

namespace Piggybank.Business.Interfaces
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
    }
}
