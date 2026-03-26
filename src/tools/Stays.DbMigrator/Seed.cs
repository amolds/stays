using Microsoft.EntityFrameworkCore;
using Stays.Domain.Models;

public static class Seed
{
    public static async Task Reset(StaysDbContext dbContext)
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
    }    

    public static async Task SeedData(StaysDbContext dbContext)
    {
        var jon = await CreateOrGetUser(dbContext, "jon.lonely@outlook.com", "Jon Lonely", "https://example.com/avatar.jpg", "Wisconsin, USA", false);
        var dad = await CreateOrGetUser(dbContext, "dad.smith@outlook.com", "Dad Smith", "https://example.com/dad-avatar.jpg", "Wisconsin, USA", false);
        var mom = await CreateOrGetUser(dbContext, "mom.smith@outlook.com", "Mom Smith", "https://example.com/mom-avatar.jpg", "Wisconsin, USA", true);
        var son = await CreateOrGetUser(dbContext, "son.smith@outlook.com", "Son Smith", "https://example.com/son-avatar.jpg", "Wisconsin, USA", true);
        var daughter = await CreateOrGetUser(dbContext, "daughter.smith@outlook.com", "Daughter Smith", "https://example.com/daughter-avatar.jpg", "Wisconsin, USA", true);

        if (!await dbContext.Families.AnyAsync())
        {
            await CreateFamilyWithMembers(dbContext, dad, new[] { dad, mom, son, daughter });
        }
    }

    private static async Task<User> CreateOrGetUser(StaysDbContext dbContext, string email, string displayName, string avatarUrl, string homeLocation, bool visibleToPublic)
    {
        var existing = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (existing != null)
        {
            return existing;
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            DisplayName = displayName,
            AvatarUrl = avatarUrl,
            HomeLocation = homeLocation,
            VisibleToPublic = visibleToPublic
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return user;
    }

    private static async Task CreateFamilyWithMembers(StaysDbContext dbContext, User creator, IEnumerable<User> members)
    {
        var family = new Family
        {
            Name = "Smith Family",
            VisibleToPublic = true,
            CreatedByUserId = creator.Id,
            CreatedByUser = creator,
            FamilyMembers = members.ToList()
        };

        dbContext.Families.Add(family);
        await dbContext.SaveChangesAsync();
    }
}