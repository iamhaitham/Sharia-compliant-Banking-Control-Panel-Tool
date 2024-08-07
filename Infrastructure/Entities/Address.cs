namespace Infrastructure.Entities;

public class Address : BaseEntity
{
    public ICollection<Client> Clients { get; set; } = new List<Client>();

    public string Country { get; set; } = string.Empty;
    
    public string City { get; set; } = string.Empty;
    
    public string Street { get; set; } = string.Empty;
    
    public string ZipCode { get; set; } = string.Empty;
}