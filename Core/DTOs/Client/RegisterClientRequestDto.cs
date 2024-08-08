using System.ComponentModel.DataAnnotations;
using Core.DTOs.Address;
using Core.DTOs.Person;

namespace Core.DTOs.Client;

public class RegisterClientRequestDto : RegisterPersonRequestDto
{
    public string ProfilePhoto { get; set; } = string.Empty;

    public AddressDto Address { get; set; } = new();
    
    [MinLength(1)]
    public required List<string> Accounts { get; set; }
}