
nuget packages management
dotnet add <PROJECT> package <PACKAGE_NAME> [options]

migration flow:

create migration:
dotnet ef migrations add <MIGRATION_NAME> --project Lockbox.Infrastructure --startup-project Lockbox.Web
run migration:
dotnet ef database update --project Lockbox.Infrastructure --startup-project Lockbox.Web
tets ubu
