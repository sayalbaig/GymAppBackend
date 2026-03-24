namespace GymAppBackend.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<WorkoutSplit> WorkoutSplits { get; set; } = new List<WorkoutSplit>();
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    public UserStats? Stats { get; set; }
}

public class UserStats
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public int TotalWorkouts { get; set; }
    public int TotalExercises { get; set; }
    public double TotalVolume { get; set; }
    public int CurrentStreak { get; set; }
    public DateTime LastWorkoutDate { get; set; }
}
