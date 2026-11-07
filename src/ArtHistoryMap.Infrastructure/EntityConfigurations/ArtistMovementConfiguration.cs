using ArtHistoryMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtHistoryMap.Infrastructure.EntityConfigurations;

public class ArtistMovementConfiguration : IEntityTypeConfiguration<ArtistMovement>
{
    public void Configure(EntityTypeBuilder<ArtistMovement> builder)
    {
        builder.ToTable("artist_movements");

        // Many-to-many için bileşik birincil anahtar (Composite PK)
        builder.HasKey(am => new { am.ArtistId, am.ArtMovementId });

        builder.HasOne(am => am.Artist)
            .WithMany(a => a.ArtistMovements)
            .HasForeignKey(am => am.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(am => am.ArtMovement)
            .WithMany(m => m.ArtistMovements)
            .HasForeignKey(am => am.ArtMovementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(am => am.Role)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(am => am.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
