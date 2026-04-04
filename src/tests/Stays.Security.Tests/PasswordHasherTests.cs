using Stays.Security;

namespace Stays.Security.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void HashPassword_GeneratesUniqueSaltAndHash()
    {
        // Arrange
        var password = "TestPassword123!";

        // Act
        var hash1 = PasswordHasher.HashPassword(password, out var salt1);
        var hash2 = PasswordHasher.HashPassword(password, out var salt2);

        // Assert
        Assert.NotNull(hash1);
        Assert.NotNull(salt1);
        Assert.NotNull(hash2);
        Assert.NotNull(salt2);

        // Same password should generate different salts and hashes
        Assert.NotEqual(salt1, salt2);
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void HashPassword_GeneratesDifferentHashesForDifferentPasswords()
    {
        // Arrange
        var password1 = "Password123!";
        var password2 = "DifferentPassword456!";

        // Act
        var hash1 = PasswordHasher.HashPassword(password1, out var salt1);
        var hash2 = PasswordHasher.HashPassword(password2, out var salt2);

        // Assert
        Assert.NotEqual(hash1, hash2);
        Assert.NotEqual(salt1, salt2);
    }

    [Fact]
    public void VerifyPassword_ReturnsTrue_ForCorrectPassword()
    {
        // Arrange
        var password = "MySecurePassword123!";
        var hash = PasswordHasher.HashPassword(password, out var salt);

        // Act
        var result = PasswordHasher.VerifyPassword(password, hash, salt);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_ReturnsFalse_ForIncorrectPassword()
    {
        // Arrange
        var correctPassword = "MySecurePassword123!";
        var wrongPassword = "WrongPassword456!";
        var hash = PasswordHasher.HashPassword(correctPassword, out var salt);

        // Act
        var result = PasswordHasher.VerifyPassword(wrongPassword, hash, salt);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_ReturnsFalse_ForWrongSalt()
    {
        // Arrange
        var password = "MySecurePassword123!";
        var hash = PasswordHasher.HashPassword(password, out var correctSalt);

        // Create a different salt
        var wrongSalt = PasswordHasher.HashPassword("dummy", out var _);

        // Act
        var result = PasswordHasher.VerifyPassword(password, hash, wrongSalt);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_ReturnsFalse_ForWrongHash()
    {
        // Arrange
        var password = "MySecurePassword123!";
        var correctHash = PasswordHasher.HashPassword(password, out var salt);
        var wrongHash = PasswordHasher.HashPassword("different", out var _);

        // Act
        var result = PasswordHasher.VerifyPassword(password, wrongHash, salt);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HashPassword_HandlesEmptyPassword()
    {
        // Arrange
        var password = "";

        // Act
        var hash = PasswordHasher.HashPassword(password, out var salt);

        // Assert
        Assert.NotNull(hash);
        Assert.NotNull(salt);
        Assert.True(IsValidBase64(hash));
        Assert.True(IsValidBase64(salt));
    }

    [Fact]
    public void VerifyPassword_HandlesEmptyPassword()
    {
        // Arrange
        var password = "";
        var hash = PasswordHasher.HashPassword("test", out var salt);

        // Act
        var result = PasswordHasher.VerifyPassword(password, hash, salt);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HashPassword_GeneratesValidBase64Strings()
    {
        // Arrange
        var password = "TestPassword123!";

        // Act
        var hash = PasswordHasher.HashPassword(password, out var salt);

        // Assert
        Assert.True(IsValidBase64(hash));
        Assert.True(IsValidBase64(salt));
    }

    [Fact]
    public void HashPassword_SaltHasCorrectLength()
    {
        // Arrange
        var password = "TestPassword123!";

        // Act
        PasswordHasher.HashPassword(password, out var salt);

        // Assert
        var saltBytes = Convert.FromBase64String(salt);
        Assert.Equal(16, saltBytes.Length); // 128 bits = 16 bytes
    }

    [Fact]
    public void HashPassword_HashHasCorrectLength()
    {
        // Arrange
        var password = "TestPassword123!";

        // Act
        var hash = PasswordHasher.HashPassword(password, out var salt);

        // Assert
        var hashBytes = Convert.FromBase64String(hash);
        Assert.Equal(32, hashBytes.Length); // 256 bits = 32 bytes
    }

    [Fact]
    public void VerifyPassword_IsCaseSensitive()
    {
        // Arrange
        var password = "Password123!";
        var wrongCasePassword = "password123!";
        var hash = PasswordHasher.HashPassword(password, out var salt);

        // Act
        var result = PasswordHasher.VerifyPassword(wrongCasePassword, hash, salt);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_HandlesSpecialCharacters()
    {
        // Arrange
        var password = "P@ssw0rd!#$%^&*()";
        var hash = PasswordHasher.HashPassword(password, out var salt);

        // Act
        var result = PasswordHasher.VerifyPassword(password, hash, salt);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_HandlesUnicodeCharacters()
    {
        // Arrange
        var password = "Pässword123!ñçü";
        var hash = PasswordHasher.HashPassword(password, out var salt);

        // Act
        var result = PasswordHasher.VerifyPassword(password, hash, salt);

        // Assert
        Assert.True(result);
    }

    private static bool IsValidBase64(string base64String)
    {
        try
        {
            Convert.FromBase64String(base64String);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
