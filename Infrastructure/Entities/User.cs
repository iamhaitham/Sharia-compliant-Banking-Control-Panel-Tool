using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Infrastructure.Entities;

public class User : Person
{
    [Required] 
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    public UserRole Role { get; set; }
}