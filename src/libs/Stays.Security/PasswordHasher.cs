using System.Security.Cryptography;
using System.Text;

namespace Stays.Security;

/// <summary>
/// Provides secure password hashing and verification using PBKDF2-SHA256.
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16; // 128 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 100000; // NIST recommendation: at least 100,000 iterations
    private const string Algorithm = "PBKDF2-SHA256";

    /// <summary>
    /// Hashes a password using PBKDF2-SHA256 with a random salt.
    /// </summary>
    /// <param name="password">The plain text password to hash</param>
    /// <param name="salt">Output: The generated salt (Base64 encoded)</param>
    /// <returns>The password hash (Base64 encoded)</returns>
    public static string HashPassword(string password, out string salt)
    {
        byte[] saltBytes = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        salt = Convert.ToBase64String(saltBytes);

        byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, HashAlgorithmName.SHA256, HashSize);
        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Verifies a password against a stored hash and salt.
    /// </summary>
    /// <param name="password">The plain text password to verify</param>
    /// <param name="hash">The stored password hash (Base64 encoded)</param>
    /// <param name="salt">The stored salt (Base64 encoded)</param>
    /// <returns>True if the password matches, false otherwise</returns>
    public static bool VerifyPassword(string password, string hash, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, HashAlgorithmName.SHA256, HashSize);
        string computedHash = Convert.ToBase64String(hashBytes);

        // Use constant-time comparison to prevent timing attacks
        return ConstantTimeComparison(hash, computedHash);
    }

    /// <summary>
    /// Performs a constant-time string comparison to prevent timing attacks.
    /// </summary>
    private static bool ConstantTimeComparison(string a, string b)
    {
        byte[] aBytes = Encoding.UTF8.GetBytes(a);
        byte[] bBytes = Encoding.UTF8.GetBytes(b);

        if (aBytes.Length != bBytes.Length)
            return false;

        int result = 0;
        for (int i = 0; i < aBytes.Length; i++)
        {
            result |= aBytes[i] ^ bBytes[i];
        }

        return result == 0;
    }
}