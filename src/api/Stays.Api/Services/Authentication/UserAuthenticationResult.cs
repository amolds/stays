namespace Stays.Api.Services.Authentication;

public sealed record UserAuthenticationResult(bool IsAuthenticated, Guid? UserId, string? DisplayName, string? ErrorMessage)
{
    public static UserAuthenticationResult Success(Guid userId, string displayName) => new(true, userId, displayName, null);

    public static UserAuthenticationResult InvalidCredentials() => new(false, null, null, "Invalid credentials.");

    public static UserAuthenticationResult AccountLocked(DateTime lockedUntil) => new(false, null, null, $"Account locked until {lockedUntil:u}.");
}
