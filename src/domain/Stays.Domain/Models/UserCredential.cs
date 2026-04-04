namespace Stays.Domain.Models;

public class UserCredential
{
    public Guid UserId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string PasswordAlgorithm { get; set; } = null!;

    public DateTime? PasswordUpdatedAt { get; set; }

    public int FailedLoginCount { get; set; }

    public DateTime? LockedUntil { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}
