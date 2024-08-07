using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Infrastructure.Entities;

public class Person : BaseEntity
{
    [MaxLength(59)]
    public required string FirstName { get; set; }

    [MaxLength(59)]
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public required string MobileNumber { get; set; } 
    
    public required string PersonalId { get; set; }
    
    public required Sex Sex { get; set; }
}
