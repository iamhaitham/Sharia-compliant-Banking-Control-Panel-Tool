using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTOs.User;

public class LoginUserResponseDto
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }

    [Required]
    public string Jwt { get; set; } = string.Empty;
}