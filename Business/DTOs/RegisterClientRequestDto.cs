using System.ComponentModel.DataAnnotations;
using Core.DTOs.Address;
using Core.DTOs.Person;
using Microsoft.AspNetCore.Http;

namespace Business.DTOs;

public class RegisterClientRequestDto : RegisterPersonRequestDto
{
    public IFormFile? ProfilePhoto { get; set; }

    public AddressDto Address { get; set; } = new();
    
    [MinLength(1)]
    public required List<string> Accounts { get; set; }
}