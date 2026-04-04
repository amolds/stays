namespace Stays.Api.Models;

public sealed record AuthenticateUserRequest(string Email, string Password);
