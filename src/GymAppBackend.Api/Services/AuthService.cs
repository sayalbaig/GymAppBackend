using GymAppBackend.Api.DTOs.Auth;
using GymAppBackend.Core.Entities;
using GymAppBackend.Core.Interfaces;
using GymAppBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Api.Services;

public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    Task<UserDto?> GetProfileAsync(Guid userId);
    Task<UserDto?> UpdateProfileAsync(Guid userId, UpdateProfileRequest request);
}

public class AuthService : IAuthService
{
    private readonly GymDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(GymDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email.ToLower() == request.Email.ToLower()))
            return null;

        if (await _context.Users.AnyAsync(u => u.Username.ToLower() == request.Username.ToLower()))
            return null;

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            DisplayName = request.DisplayName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Stats = new UserStats
            {
                Id = Guid.NewGuid(),
                TotalWorkouts = 0,
                TotalExercises = 0,
                TotalVolume = 0,
                CurrentStreak = 0
            }
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token,
            User = MapToUserDto(user)
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .Include(u => u.Stats)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return null;

        var token = _jwtService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token,
            User = MapToUserDto(user)
        };
    }

    public async Task<UserDto?> GetProfileAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user == null ? null : MapToUserDto(user);
    }

    public async Task<UserDto?> UpdateProfileAsync(Guid userId, UpdateProfileRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return null;

        if (!string.IsNullOrEmpty(request.Username))
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == request.Username.ToLower() && u.Id != userId))
                return null;
            user.Username = request.Username;
        }

        if (!string.IsNullOrEmpty(request.DisplayName))
            user.DisplayName = request.DisplayName;

        if (!string.IsNullOrEmpty(request.NewPassword))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        user.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return MapToUserDto(user);
    }

    private static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DisplayName = user.DisplayName
        };
    }
}
