using System.ComponentModel.DataAnnotations;

namespace GymAppBackend.Api.DTOs.Auth;

public class UpdateProfileRequest
{
    [MinLength(3)]
    public string? Username { get; set; }

    public string? DisplayName { get; set; }

    [MinLength(6)]
    public string? NewPassword { get; set; }
}
