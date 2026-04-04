namespace Stays.Domain.Models;

public class Visit
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }    

    public double? Rating { get; set; }

    public bool VisibleToPublic { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid? TripId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Place> Places { get; set; } = new List<Place>();

    public ICollection<Note> Notes { get; set; } = new List<Note>();

    public ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public Trip? Trip { get; set; }
}