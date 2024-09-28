using Piggybank.Models;

namespace Piggybank.Data.Interfaces
{
    /// <summary>
    /// Provides access to operations related to <see cref="AppUser"/> entities in the database.
    /// </summary>
    public interface IAppUserRepository
    {
        /// <summary>
        /// Retrieves all <see cref="AppUser"/> entities from the database asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="AppUser"/> entities.</returns>
        Task<IEnumerable<AppUser>> GetAllAsync();
    }
}
