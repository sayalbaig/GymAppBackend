using GymAppBackend.Core.Entities;

namespace GymAppBackend.Core.Interfaces;

public interface IWorkoutRepository : IRepository<Workout>
{
    Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId);
    Task<Workout?> GetWithExercisesAsync(Guid id);
}
