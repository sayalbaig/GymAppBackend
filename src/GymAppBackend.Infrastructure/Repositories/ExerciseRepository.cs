using GymAppBackend.Core.Entities;
using GymAppBackend.Core.Interfaces;
using GymAppBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.Repositories;

public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
{
    public ExerciseRepository(GymDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Exercise>> GetByMuscleGroupAsync(string muscleGroup)
    {
        return await _dbSet
            .Where(e => e.MuscleGroup.ToLower() == muscleGroup.ToLower())
            .ToListAsync();
    }

    public async Task<IEnumerable<Exercise>> SearchByNameAsync(string name)
    {
        return await _dbSet
            .Where(e => e.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }
}
