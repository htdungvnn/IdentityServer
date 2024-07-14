docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=YourStrongPassword!' -p 1433:1433 --name sql-edge -d mcr.microsoft.com/azure-sql-edge

 "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=YourDatabaseName;User Id=sa;Password=YourStrongPassword!;TrustServerCertificate=True;"
  }

dotnet ef migrations add InitialIdentityServerConfigurationMigration -c AppConfigurationDbContext
dotnet ef migrations add InitialPersistedGrantDbMigration -c AppPersistedGrantDbContext
dotnet ef migrations add InitialIdentityDbMigration -c ApplicationDbContext
dotnet ef database update -c AppConfigurationDbContext
dotnet ef database update -c AppPersistedGrantDbContext
dotnet ef database update -c ApplicationDbContext



dotnet run -- /seed
