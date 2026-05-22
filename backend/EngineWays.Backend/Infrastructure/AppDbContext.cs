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
        modelBuilder.Entity<GtfsStop>(entity =>
        {
            entity.HasKey(s => s.StopId
            );
        });
        modelBuilder.Entity<GtfsRoute>(entity =>
        {
            entity.HasKey(r => r.RouteId);
        });
        modelBuilder.Entity<GtfsTrip>(entity =>
        {
            entity.HasKey(t => t.TripId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.Username).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);

            entity.HasMany<UserRole>()
                .WithMany()
                .UsingEntity(j => j.ToTable("UserUserRoles"));
        });
        
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => ur.Id);

            entity.Property(ur => ur.RoleName).IsRequired().HasMaxLength(50);
        });
    }
}
