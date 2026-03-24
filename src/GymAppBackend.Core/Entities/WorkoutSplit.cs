namespace GymAppBackend.Core.Entities;

public class WorkoutSplit
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<SplitDay> Days { get; set; } = new List<SplitDay>();
}

public class SplitDay
{
    public Guid Id { get; set; }
    public Guid WorkoutSplitId { get; set; }
    public WorkoutSplit WorkoutSplit { get; set; } = null!;
    public int DayNumber { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<SplitDayExercise> Exercises { get; set; } = new List<SplitDayExercise>();
}

public class SplitDayExercise
{
    public Guid Id { get; set; }
    public Guid SplitDayId { get; set; }
    public SplitDay SplitDay { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Order { get; set; }
    public int TargetSets { get; set; }
    public int? TargetReps { get; set; }
    public int? TargetRpe { get; set; }
}
