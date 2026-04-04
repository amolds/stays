namespace Stays.Api.Services.Authentication;

public interface IUserAuthenticationService
{
    Task<UserAuthenticationResult> AuthenticateAsync(string email, string password);
}
