using GymAppBackend.Core.Interfaces;
using GymAppBackend.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GymAppBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IWorkoutSplitRepository, WorkoutSplitRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();

        return services;
    }
}
