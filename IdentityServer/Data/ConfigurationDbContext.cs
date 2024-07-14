using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class AppConfigurationDbContext : ConfigurationDbContext<AppConfigurationDbContext>
{
    public AppConfigurationDbContext(DbContextOptions<AppConfigurationDbContext> options)
        : base(options)
    {
    }
}