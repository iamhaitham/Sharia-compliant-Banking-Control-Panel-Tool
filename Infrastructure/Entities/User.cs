using Core.Enums;

namespace Infrastructure.Entities;

public class User : Person
{
    public required string Password { get; set; }
    
    public required UserRole Role { get; set; }
}