using Core.Enums;

namespace Core.DTOs;

public class RegisterPersonRequestDto
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public required MobileNumberDto MobileNumber { get; set; } 
    
    public required string PersonalId { get; set; }
    
    public required Sex Sex { get; set; }
}