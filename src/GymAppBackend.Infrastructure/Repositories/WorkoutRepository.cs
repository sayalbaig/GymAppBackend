using GymAppBackend.Core.Entities;
using GymAppBackend.Core.Interfaces;
using GymAppBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.Repositories;

public class WorkoutRepository : Repository<Workout>, IWorkoutRepository
{
    public WorkoutRepository(GymDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.StartedAt)
            .ToListAsync();
    }

    public async Task<Workout?> GetWithExercisesAsync(Guid id)
    {
        return await _dbSet
            .Include(w => w.Exercises)
                .ThenInclude(e => e.Exercise)
            .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
            .FirstOrDefaultAsync(w => w.Id == id);
    }
}
