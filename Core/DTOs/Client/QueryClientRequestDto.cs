using Core.Enums;

namespace Core.DTOs.Client;

public class QueryClientRequestDto
{
    public int? Skip { get; set; }

    public int? Take { get; set; }

    public Sort Sort { get; set; } = Sort.Ascending;
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public Sex? Sex { get; set; }
    
    public string Country { get; set; } = string.Empty;
    
    public string City { get; set; } = string.Empty;
    
    public string Street { get; set; } = string.Empty;
    
    public string ZipCode { get; set; } = string.Empty;
    
    public string MobileNumber { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string PersonalId { get; set; } = string.Empty;

    public List<string> Accounts { get; set; } = new();
}