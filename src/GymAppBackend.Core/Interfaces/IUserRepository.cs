using GymAppBackend.Core.Entities;

namespace GymAppBackend.Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetWithStatsAsync(Guid id);
}
