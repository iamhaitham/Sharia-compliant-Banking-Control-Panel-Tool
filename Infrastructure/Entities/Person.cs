using Core.Enums;

namespace Infrastructure.Entities;

public class Person
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public required string MobileNumber { get; set; } 
    
    public required string PersonalId { get; set; }
    
    public required Sex Sex { get; set; }
}
