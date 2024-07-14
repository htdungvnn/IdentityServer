using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class AppPersistedGrantDbContext : PersistedGrantDbContext<AppPersistedGrantDbContext>
{
    public AppPersistedGrantDbContext(DbContextOptions<AppPersistedGrantDbContext> options)
        : base(options)
    {
    }
}