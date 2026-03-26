dev scripts, codegen, swagger tooling, migration helpers

## User Secrets
dotnet user-secrets init --project <Project>
dotnet user-secrets set <connection-string> --project <Project>

## Adding migrations
dotnet ef migrations add InitialCreate \
  --project src/data/Stays.Persistence \
  --startup-project src/tools/Stays.DbMigrator \
  --output-dir Migrations

## Running migrations
dotnet run --project tools/Stays.DbMigrator
