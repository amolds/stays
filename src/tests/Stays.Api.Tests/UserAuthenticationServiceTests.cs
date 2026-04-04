using Microsoft.EntityFrameworkCore;
using Stays.Api.Services.Authentication;
using Stays.Domain.Models;
using Stays.Security;

namespace Stays.Api.Tests;

public class UserAuthenticationServiceTests
{
    [Fact]
    public async Task AuthenticateAsync_ReturnsSuccess_WhenPasswordMatches()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<StaysDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var userId = Guid.NewGuid();
        var password = "TestPassword!123";
        var passwordHash = PasswordHasher.HashPassword(password, out var passwordSalt);

        await using (var dbContext = new StaysDbContext(options))
        {
            dbContext.Users.Add(new User
            {
                Id = userId,
                Email = "test@example.com",
                DisplayName = "Test User",
                AvatarUrl = "https://example.com/avatar.png",
                HomeLocation = "Testville",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Credential = new UserCredential
                {
                    UserId = userId,
                    User = null!,
                    PasswordAlgorithm = "PBKDF2-SHA256",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            });

            await dbContext.SaveChangesAsync();
        }

        await using (var dbContext = new StaysDbContext(options))
        {
            var service = new UserAuthenticationService(dbContext);
            var result = await service.AuthenticateAsync("test@example.com", password);

            Assert.True(result.IsAuthenticated);
            Assert.Equal(userId, result.UserId);
            Assert.Equal("Test User", result.DisplayName);
        }
    }

    [Fact]
    public async Task AuthenticateAsync_ReturnsInvalidCredentials_WhenPasswordDoesNotMatch()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<StaysDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var userId = Guid.NewGuid();
        var password = "CorrectPassword!123";
        var wrongPassword = "WrongPassword!123";
        var passwordHash = PasswordHasher.HashPassword(password, out var passwordSalt);

        await using (var dbContext = new StaysDbContext(options))
        {
            dbContext.Users.Add(new User
            {
                Id = userId,
                Email = "test2@example.com",
                DisplayName = "Test User 2",
                AvatarUrl = "https://example.com/avatar2.png",
                HomeLocation = "Testville 2",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Credential = new UserCredential
                {
                    UserId = userId,
                    User = null!,
                    PasswordAlgorithm = "PBKDF2-SHA256",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            });

            await dbContext.SaveChangesAsync();
        }

        await using (var dbContext = new StaysDbContext(options))
        {
            var service = new UserAuthenticationService(dbContext);
            var result = await service.AuthenticateAsync("test2@example.com", wrongPassword);

            Assert.False(result.IsAuthenticated);
            Assert.Null(result.UserId);
            Assert.Equal("Invalid credentials.", result.ErrorMessage);
        }
    }
}
