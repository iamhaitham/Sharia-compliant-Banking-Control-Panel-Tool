namespace Infrastructure.Entities;

public class Client : Person
{
    public string ProfilePhoto { get; set; } = string.Empty;

    public Address Address { get; set; } = new();
    
    public required List<string> Accounts { get; set; }
}