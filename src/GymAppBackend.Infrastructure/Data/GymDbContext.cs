using GymAppBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.Data;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserStats> UserStats => Set<UserStats>();
    public DbSet<WorkoutSplit> WorkoutSplits => Set<WorkoutSplit>();
    public DbSet<SplitDay> SplitDays => Set<SplitDay>();
    public DbSet<SplitDayExercise> SplitDayExercises => Set<SplitDayExercise>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
    public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();
    public DbSet<Exercise> Exercises => Set<Exercise>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<UserStats>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                .WithOne(u => u.Stats)
                .HasForeignKey<UserStats>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkoutSplit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                .WithMany(u => u.WorkoutSplits)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SplitDay>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.WorkoutSplit)
                .WithMany(ws => ws.Days)
                .HasForeignKey(e => e.WorkoutSplitId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SplitDayExercise>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.SplitDay)
                .WithMany(sd => sd.Exercises)
                .HasForeignKey(e => e.SplitDayId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Exercise)
                .WithMany(ex => ex.SplitDayExercises)
                .HasForeignKey(e => e.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.MuscleGroup);
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                .WithMany(u => u.Workouts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.WorkoutSplit)
                .WithMany()
                .HasForeignKey(e => e.WorkoutSplitId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<WorkoutExercise>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Workout)
                .WithMany(w => w.Exercises)
                .HasForeignKey(e => e.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Exercise)
                .WithMany(ex => ex.WorkoutExercises)
                .HasForeignKey(e => e.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkoutSet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.WorkoutExercise)
                .WithMany(we => we.Sets)
                .HasForeignKey(e => e.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
