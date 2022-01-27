namespace invoice.app.Entity;

public class JoinCode
{
    public Guid Code { get; private set; }

    public string ForEmail { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    
    public TimeSpan Expiration { get; set; }

    public Guid CreatorId { get; set; }
    
    public virtual AppUser Creator { get; set; }
    
    public Guid OrganizationId { get; set; }
    
    public virtual Organization Organization { get; set; }

    public JoinCode()
    {
        Code = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
        Expiration = TimeSpan.FromHours(1);
    }
}