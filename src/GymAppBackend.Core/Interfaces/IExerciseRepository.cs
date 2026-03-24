using GymAppBackend.Core.Entities;

namespace GymAppBackend.Core.Interfaces;

public interface IExerciseRepository : IRepository<Exercise>
{
    Task<IEnumerable<Exercise>> GetByMuscleGroupAsync(string muscleGroup);
    Task<IEnumerable<Exercise>> SearchByNameAsync(string name);
}
