namespace Stays.Domain.Models;

public class Note
{
    public Guid Id { get; set; }

    public Guid VisitId { get; set; }

    public Guid AuthorUserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User AuthorUser { get; set; } = null!;

    public Visit Visit { get; set; } = null!;
}