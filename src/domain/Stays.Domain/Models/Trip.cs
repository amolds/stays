namespace Stays.Domain.Models;

public class Trip
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid CreatedByUserId { get; set; }

    public bool VisibleToPublic { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User CreatedByUser { get; set; } = null!;

    public ICollection<Visit> Visits { get; set; } = new List<Visit>();

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}