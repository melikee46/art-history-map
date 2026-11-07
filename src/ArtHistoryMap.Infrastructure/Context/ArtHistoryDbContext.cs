using ArtHistoryMap.Domain.Common;
using ArtHistoryMap.Domain.Entities;
using ArtHistoryMap.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace ArtHistoryMap.Infrastructure.Context;

public class ArtHistoryDbContext : DbContext
{
    public ArtHistoryDbContext(DbContextOptions<ArtHistoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<ArtMovement> ArtMovements => Set<ArtMovement>();
    public DbSet<ArtMovementRelation> ArtMovementRelations => Set<ArtMovementRelation>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<ArtistMovement> ArtistMovements => Set<ArtistMovement>();
    public DbSet<Artwork> Artworks => Set<Artwork>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API konfigürasyonlarını assembly'den otomatik yükle
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArtHistoryDbContext).Assembly);

        // Seed verisini uygula
        ArtHistoryDataSeeder.SeedData(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.CreatedAt == default)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
