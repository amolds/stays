namespace Stays.Domain.Models;

public class Tag
{
    public Guid Id { get; set; }

    public Guid OwnerUserId { get; set; }

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User OwnerUser { get; set; } = null!;

    public ICollection<Trip> TripTags { get; set; } = new List<Trip>();
    
    public ICollection<Visit> VisitTags { get; set; } = new List<Visit>();
}