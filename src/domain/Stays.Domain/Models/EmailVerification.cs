namespace Stays.Domain.Models;

public class EmailVerification
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string VerificationToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}