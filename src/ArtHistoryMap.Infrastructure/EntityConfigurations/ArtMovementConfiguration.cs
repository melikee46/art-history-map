using ArtHistoryMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtHistoryMap.Infrastructure.EntityConfigurations;

public class ArtMovementConfiguration : IEntityTypeConfiguration<ArtMovement>
{
    public void Configure(EntityTypeBuilder<ArtMovement> builder)
    {
        builder.ToTable("art_movements");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(m => m.Name)
            .IsUnique();

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(2500);

        builder.Property(m => m.OriginRegion)
            .HasMaxLength(100);

        builder.Property(m => m.CreatedAt)
            .IsRequired();
    }
}
