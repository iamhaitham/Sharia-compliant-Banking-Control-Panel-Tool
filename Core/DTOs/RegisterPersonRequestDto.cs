using System.ComponentModel.DataAnnotations;
using Core.CustomAttributes;
using Core.Enums;

namespace Core.DTOs;

public class RegisterPersonRequestDto
{
    [MaxLength(59)]
    public required string FirstName { get; set; }
    
    [MaxLength(59)]
    public required string LastName { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }
    
    [MobileNumber]
    public required MobileNumberDto MobileNumber { get; set; } 
    
    [ExactLength(11)]
    public required string PersonalId { get; set; }
    
    [ValidEnum(typeof(Core.Enums.Sex))]
    public required Sex Sex { get; set; }
}