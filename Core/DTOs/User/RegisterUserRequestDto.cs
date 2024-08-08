using Core.DTOs.Person;
using Core.Enums;

namespace Core.DTOs.User;

public class RegisterUserRequestDto : RegisterPersonRequestDto
{
    public required string Password { get; set; }
    
    public required UserRole Role { get; set; }
}