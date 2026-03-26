namespace Stays.Domain.Models;

public class Family
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid CreatedByUserId { get; set; }

    public bool VisibleToPublic { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User CreatedByUser { get; set; } = null!;

    public ICollection<User> FamilyMembers { get; set; } = new List<User>();
}