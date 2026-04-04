using Microsoft.EntityFrameworkCore;
using Stays.Domain.Models;
using Stays.Security;

namespace Stays.Api.Services.Authentication;

public sealed class UserAuthenticationService : IUserAuthenticationService
{
    private readonly StaysDbContext _dbContext;

    public UserAuthenticationService(StaysDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserAuthenticationResult> AuthenticateAsync(string email, string password)
    {
        var user = await _dbContext.Users.Include(u => u.Credential)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user?.Credential is null)
        {
            return UserAuthenticationResult.InvalidCredentials();
        }

        if (user.Credential.LockedUntil is DateTime lockedUntil && lockedUntil > DateTime.UtcNow)
        {
            return UserAuthenticationResult.AccountLocked(lockedUntil);
        }

        if (!PasswordHasher.VerifyPassword(password, user.Credential.PasswordHash, user.Credential.PasswordSalt))
        {
            return UserAuthenticationResult.InvalidCredentials();
        }

        return UserAuthenticationResult.Success(user.Id, user.DisplayName);
    }
}
