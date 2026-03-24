using GymAppBackend.Core.Entities;
using GymAppBackend.Core.Interfaces;
using GymAppBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.Repositories;

public class WorkoutSplitRepository : Repository<WorkoutSplit>, IWorkoutSplitRepository
{
    public WorkoutSplitRepository(GymDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WorkoutSplit>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(ws => ws.UserId == userId)
            .Include(ws => ws.Days)
                .ThenInclude(d => d.Exercises)
                    .ThenInclude(e => e.Exercise)
            .OrderByDescending(ws => ws.CreatedAt)
            .ToListAsync();
    }

    public async Task<WorkoutSplit?> GetWithDaysAsync(Guid id)
    {
        return await _dbSet
            .Include(ws => ws.Days)
                .ThenInclude(d => d.Exercises)
                    .ThenInclude(e => e.Exercise)
            .FirstOrDefaultAsync(ws => ws.Id == id);
    }

    public async Task<WorkoutSplit?> GetActiveByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(ws => ws.UserId == userId && ws.IsActive)
            .Include(ws => ws.Days)
                .ThenInclude(d => d.Exercises)
                    .ThenInclude(e => e.Exercise)
            .FirstOrDefaultAsync();
    }
}
