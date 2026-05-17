using Microsoft.EntityFrameworkCore;

namespace EngineWays.Backend.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<GtfsStop> Stops { get; set; }
    public DbSet<GtfsRoute> Routes { get; set; }
    public DbSet<GtfsTrip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration if needed
    }
}
