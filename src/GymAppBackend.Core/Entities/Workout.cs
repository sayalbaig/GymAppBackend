namespace GymAppBackend.Core.Entities;

public class Workout
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid? WorkoutSplitId { get; set; }
    public WorkoutSplit? WorkoutSplit { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    public bool IsCompleted { get; set; }

    public ICollection<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();
}

public class WorkoutExercise
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public Workout Workout { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Order { get; set; }
    public string? Notes { get; set; }

    public ICollection<WorkoutSet> Sets { get; set; } = new List<WorkoutSet>();
}

public class WorkoutSet
{
    public Guid Id { get; set; }
    public Guid WorkoutExerciseId { get; set; }
    public WorkoutExercise WorkoutExercise { get; set; } = null!;
    public int SetNumber { get; set; }
    public int? Reps { get; set; }
    public double? Weight { get; set; }
    public int? Rpe { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
}
