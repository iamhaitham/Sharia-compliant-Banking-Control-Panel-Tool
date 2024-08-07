using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class LoginUserRequestDto
{
    [EmailAddress]
    public required string Email { get; set; }
    
    public required string Password { get; set; }
}