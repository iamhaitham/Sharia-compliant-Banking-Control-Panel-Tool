using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class RegisterClientRequestDto : RegisterPersonRequestDto
{
    public string ProfilePhoto { get; set; } = string.Empty;

    public AddressDto Address { get; set; } = new();
    
    [MinLength(1)]
    public required List<string> Accounts { get; set; }
}