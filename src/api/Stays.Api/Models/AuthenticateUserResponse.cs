namespace Stays.Api.Models;

public sealed record AuthenticateUserResponse(bool IsAuthenticated, Guid? UserId, string? DisplayName, string? ErrorMessage = null);
