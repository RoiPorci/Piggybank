using Piggybank.Business.Interfaces;
using Piggybank.Data.Interfaces;
using Piggybank.Models;

namespace Piggybank.Business.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _userRepository;

        public AppUserService(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
