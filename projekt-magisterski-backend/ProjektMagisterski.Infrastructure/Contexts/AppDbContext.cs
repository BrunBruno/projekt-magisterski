
using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Infrastructure.Configuration;

namespace ProjektMagisterski.Infrastructure.Contexts;

internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var configuration = new DbContextConfiguration();
    }
}
