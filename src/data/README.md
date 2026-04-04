Stays.Persistence/ (DbContext, EF Core entities, migrations)
Stays.Persistence.Tests/

## Adding migrations
dotnet ef migrations add AddUserProperties --project data/Stays.Persistence --startup-project tools/Stays.DbMigrator

## Dropping entire database
dotnet ef database drop --project ./data/Stays.Persistence/

## Removing all migrations
dotnet ef migrations remove --project ./data/Stays.Persistence

### Docker
docker run --platform linux/amd64   -e "ACCEPT_EULA=Y"   -e "MSSQL_SA_PASSWORD=<password>"   -p 1433:1433   --name mssql-dev   --hostname sql1   -v sqlvolume:/var/opt/mssql   -d mcr.microsoft.com/mssql/server:2025-latest
