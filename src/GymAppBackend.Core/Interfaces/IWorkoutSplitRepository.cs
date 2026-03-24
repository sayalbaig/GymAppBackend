using GymAppBackend.Core.Entities;

namespace GymAppBackend.Core.Interfaces;

public interface IWorkoutSplitRepository : IRepository<WorkoutSplit>
{
    Task<IEnumerable<WorkoutSplit>> GetByUserIdAsync(Guid userId);
    Task<WorkoutSplit?> GetWithDaysAsync(Guid id);
    Task<WorkoutSplit?> GetActiveByUserIdAsync(Guid userId);
}
