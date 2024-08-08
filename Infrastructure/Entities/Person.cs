using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Infrastructure.Entities;

public class Person : BaseEntity
{
    [Required]
    [MaxLength(59)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(59)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string MobileNumber { get; set; } = string.Empty; 
    
    [Required]
    public string PersonalId { get; set; } = string.Empty;
    
    public Sex? Sex { get; set; }
    
    public DateTime CreatedOn { get; set; }
}
