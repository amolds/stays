Stays.Persistence/ (DbContext, EF Core entities, migrations)
Stays.Persistence.Tests/

## Adding migrations
dotnet ef migrations add AddUserProperties --project data/Stays.Persistence --startup-project tools/Stays.DbMigrator
