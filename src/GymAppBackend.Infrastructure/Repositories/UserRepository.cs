using GymAppBackend.Core.Entities;
using GymAppBackend.Core.Interfaces;
using GymAppBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(GymDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }

    public async Task<User?> GetWithStatsAsync(Guid id)
    {
        return await _dbSet
            .Include(u => u.Stats)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
