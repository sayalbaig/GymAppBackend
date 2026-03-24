namespace GymAppBackend.Core.Entities;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string MuscleGroup { get; set; } = string.Empty;
    public string? SecondaryMuscles { get; set; }
    public string? Equipment { get; set; }
    public string? Instructions { get; set; }
    public bool IsCustom { get; set; }
    public Guid? CreatedByUserId { get; set; }

    public ICollection<SplitDayExercise> SplitDayExercises { get; set; } = new List<SplitDayExercise>();
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
}
