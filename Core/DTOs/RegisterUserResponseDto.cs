namespace Core.DTOs;

public class RegisterUserResponseDto
{
    public required string Email { get; set; }
    
    public required string PasswordHash { get; set; }
}