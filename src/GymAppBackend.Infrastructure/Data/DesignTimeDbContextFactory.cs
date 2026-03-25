using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GymAppBackend.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GymDbContext>
{
    public GymDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GymDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=gymapp;Username=postgres;Password=1235");

        return new GymDbContext(optionsBuilder.Options);
    }
}
