namespace Stays.Domain.Models;

public class Photo
{
    public Guid Id { get; set; }

    public Guid VisitId { get; set; }

    public Guid OwnerUserId { get; set; }

    public string StorageKey { get; set; } = null!;

    public string CapturedAt { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Caption { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User OwnerUser { get; set; } = null!;

    public Visit Visit { get; set; } = null!;
}