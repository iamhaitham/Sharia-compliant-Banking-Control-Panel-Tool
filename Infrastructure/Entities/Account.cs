namespace Infrastructure.Entities;

public class Account : BaseEntity
{
    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public string AccountNumber { get; set; } = string.Empty;
}