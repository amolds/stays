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

        if (!await dbContext.Visits.AnyAsync())
        {
            var paris = await CreatePlaceAsync(dbContext, "Paris", "City", "Paris, France");
            var rome = await CreatePlaceAsync(dbContext, "Rome", "City", "Rome, Italy");
            var barcelona = await CreatePlaceAsync(dbContext, "Barcelona", "City", "Barcelona, Spain");
            var aspen = await CreatePlaceAsync(dbContext, "Aspen", "City", "Aspen, Colorado, USA");
            var vail = await CreatePlaceAsync(dbContext, "Vail", "City", "Vail, Colorado, USA");

            var jonParisVisit = await CreateVisitAsync(dbContext, jon, paris, new DateTime(2023, 6, 1), new DateTime(2023, 6, 2));
            await CreateNoteAsync(dbContext, jon, jonParisVisit, "Had a great time in Paris! Visited the Eiffel Tower and Louvre.");
            await CreatePhotoAsync(dbContext, jon, jonParisVisit, "Eiffel Tower", "2023-06-01T14:30:00Z", "https://example.com/paris-photo.jpg");
            await CreateTagAsync(dbContext, jon, jonParisVisit, "Paris");

            var jonRomeVisit = await CreateVisitAsync(dbContext, jon, rome, new DateTime(2023, 6, 5), new DateTime(2023, 6, 6));
            await CreateNoteAsync(dbContext, jon, jonRomeVisit, "Rome was amazing! The Colosseum and Vatican City were highlights.");
            await CreatePhotoAsync(dbContext, jon, jonRomeVisit, "Colosseum", "2023-06-05T14:30:00Z", "https://example.com/rome-photo.jpg");
            await CreateTagAsync(dbContext, jon, jonRomeVisit, "Rome");

            var jonBarcelonaVisit = await CreateVisitAsync(dbContext, jon, barcelona, new DateTime(2023, 6, 10), new DateTime(2023, 6, 11));
            await CreateNoteAsync(dbContext, jon, jonBarcelonaVisit, "Barcelona was beautiful! The Sagrada Familia and Park Güell were top picks.");
            await CreatePhotoAsync(dbContext, jon, jonBarcelonaVisit, "Sagrada Familia", "2023-06-10T14:30:00Z", "https://example.com/barcelona-photo.jpg");
            await CreateTagAsync(dbContext, jon, jonBarcelonaVisit, "Barcelona");

            var momAspenVisit = await CreateVisitAsync(dbContext, mom, aspen, new DateTime(2023, 12, 15), new DateTime(2023, 12, 16));
            await CreateNoteAsync(dbContext, mom, momAspenVisit, "Aspen was wonderful! The skiing was incredible.");
            await CreatePhotoAsync(dbContext, mom, momAspenVisit, "Skiing", "2023-12-15T14:30:00Z", "https://example.com/aspen-photo.jpg");
            await CreateTagAsync(dbContext, mom, momAspenVisit, "Aspen");

            var dadVailVisit = await CreateVisitAsync(dbContext, dad, vail, new DateTime(2023, 12, 20), new DateTime(2023, 12, 21));
            await CreateNoteAsync(dbContext, dad, dadVailVisit, "Vail had amazing snow conditions!");
            await CreatePhotoAsync(dbContext, dad, dadVailVisit, "Skiing", "2023-12-20T14:30:00Z", "https://example.com/vail-photo.jpg");
            await CreateTagAsync(dbContext, dad, dadVailVisit, "Vail");

            var sonAspenVisit = await CreateVisitAsync(dbContext, son, aspen, new DateTime(2023, 12, 15), new DateTime(2023, 12, 16));
            await CreateNoteAsync(dbContext, son, sonAspenVisit, "Barcelona was beautiful! The Sagrada Familia and Park Güell were top picks.");
            await CreatePhotoAsync(dbContext, son, sonAspenVisit, "Skiing", "2023-12-15T14:30:00Z", "https://example.com/son-photo.jpg");
            await CreateTagAsync(dbContext, son, sonAspenVisit, "Aspen");

            var daughterVailVisit = await CreateVisitAsync(dbContext, daughter, vail, new DateTime(2023, 12, 20), new DateTime(2023, 12, 21));
            await CreateNoteAsync(dbContext, daughter, daughterVailVisit, "Vail had amazing snow conditions!");
            await CreatePhotoAsync(dbContext, daughter, daughterVailVisit, "Skiing", "2023-12-20T14:30:00Z", "https://example.com/daughter-photo.jpg");
            await CreatePhotoAsync(dbContext, daughter, daughterVailVisit, "Skiing", "2023-12-20T14:30:00Z", "https://example.com/daughter-photo2.jpg");
            await CreateTagAsync(dbContext, daughter, daughterVailVisit, "Vail");
        }

        if (!await dbContext.Trips.AnyAsync())
        {
            await CreateTripWithVisits(dbContext, jon, "Summer Vacation 2023", new[]
            {
                ("Paris", new DateTime(2023, 6, 1)),
                ("Rome", new DateTime(2023, 6, 5)),
                ("Barcelona", new DateTime(2023, 6, 10))
            });

            await CreateTripWithVisits(dbContext, dad, "Winter Getaway 2023", new[]
            {
                ("Aspen", new DateTime(2023, 12, 15)),
                ("Vail", new DateTime(2023, 12, 20))
            });
        }
    }

    private static async Task<Place> CreatePlaceAsync(StaysDbContext dbContext, string name, string category = "City", string address = "Unknown Address")
    {
        var place = new Place 
        { 
            Name = name,
            Category = category,
            Address = address,
            Latitude = 0,
            Longitude = 0
        };
        dbContext.Places.Add(place);

        await dbContext.SaveChangesAsync();

        return place;
    }

    private static async Task<Visit> CreateVisitAsync(StaysDbContext dbContext, User user, Place place, DateTime startDate, DateTime endDate)
    {
        var visit = new Visit
        {
            UserId = user.Id,
            User = user,
            StartDate = startDate,
            EndDate = endDate,
        };
        visit.Places.Add(place);

        dbContext.Visits.Add(visit);
        await dbContext.SaveChangesAsync();
        
        return visit;
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

    private static async Task CreateTripWithVisits(StaysDbContext dbContext, User creator, string tripName, IEnumerable<(string placeName, DateTime visitDate)> visits)
    {
        var trip = new Trip
        {
            Name = tripName,
            VisibleToPublic = true,
            CreatedByUserId = creator.Id,
            CreatedByUser = creator
        };

        foreach (var (placeName, visitDate) in visits)
        {
            var place = await dbContext.Places.FirstOrDefaultAsync(p => p.Name == placeName);
            if (place == null)
            {
                place = await CreatePlaceAsync(dbContext, placeName, "City", $"{placeName}, Unknown");
            }

            var visit = new Visit
            {
                UserId = creator.Id,
                User = creator,
                StartDate = visitDate,
                EndDate = visitDate.AddDays(1),
            };
            visit.Places.Add(place);

            trip.Visits.Add(visit);
        }

        trip.Tags.Add(new Tag { Name = tripName, Color = "#FF0000", CreatedAt = DateTime.UtcNow, OwnerUserId = creator.Id, OwnerUser = creator });

        dbContext.Trips.Add(trip);

        await dbContext.SaveChangesAsync();
    }

    private static async Task<Note> CreateNoteAsync(StaysDbContext dbContext, User user, Visit visit, string content)
    {
        var note = new Note
        {
            Content = content,
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Notes.Add(note);

        user.Notes.Add(note);
        visit.Notes.Add(note);            
        
        await dbContext.SaveChangesAsync();

        return note;
    }

    private static async Task<Photo> CreatePhotoAsync(StaysDbContext dbContext, User user, Visit visit, string caption, string capturedAt, string url)
    {
        var photo = new Photo
        {
            Caption = caption,
            CapturedAt = capturedAt,
            StorageKey = url,
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Photos.Add(photo);

        user.Photos.Add(photo);
        visit.Photos.Add(photo);            
        
        await dbContext.SaveChangesAsync();

        return photo;
    }

    private static async Task<Tag> CreateTagAsync(StaysDbContext dbContext, User user, Visit visit, string name)
    {
        var tag = new Tag
        {
            Name = name,
            Color = "#FF0000",
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Tags.Add(tag);

        user.Tags.Add(tag);
        visit.Tags.Add(tag);            
        
        await dbContext.SaveChangesAsync();

        return tag;
    }
}