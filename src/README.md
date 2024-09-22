
nuget packages management
dotnet add <PROJECT> package <PACKAGE_NAME> [options]

migration flow:

create migration:
dotnet ef migrations add <MigrationName> --project Lockbox.Infrastructure --startup-project Lockbox.Presentation
run migration:
dotnet ef database update --project Lockbox.Infrastructure --startup-project Lockbox.Presentation
