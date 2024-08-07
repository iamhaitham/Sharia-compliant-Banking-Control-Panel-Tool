using Core.Enums;

namespace Core.DTOs;

public class RegisterUserRequestDto : RegisterPersonRequestDto
{
    public required string Password { get; set; }
    
    public required UserRole Role { get; set; }
}