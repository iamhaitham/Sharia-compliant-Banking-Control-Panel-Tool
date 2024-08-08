using Core.DTOs.Address;
using Core.Enums;

namespace Core.DTOs.Client;

public class QueryClientResponseDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }
    
    public Sex? Sex { get; set; }
    
    public AddressDto Address { get; set; } = new();
    
    public required string MobileNumber { get; set; }
    
    public required string Email { get; set; }
    
    public required string PersonalId { get; set; }
    
    public required List<string> Accounts { get; set; }
}