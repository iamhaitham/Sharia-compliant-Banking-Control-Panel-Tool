namespace Infrastructure.Entities;

public class BaseEntity
{
    public int Id { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    
    public DateTimeOffset UpdatedOn { get; set; }
}