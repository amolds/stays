namespace Stays.Domain.Models;

public class User
{
    public Guid Id { get; set; }

    public Guid? FamilyId { get; set; }

    public string Email { get; set; } = null!;

    public bool EmailVerified { get; set; }

    public string DisplayName { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public string HomeLocation { get; set; } = null!;

    public bool VisibleToPublic { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Family? Family { get; set; }
}
