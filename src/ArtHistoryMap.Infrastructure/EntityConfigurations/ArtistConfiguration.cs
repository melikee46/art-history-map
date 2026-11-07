using ArtHistoryMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtHistoryMap.Infrastructure.EntityConfigurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("artists");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(a => a.FullName);

        builder.Property(a => a.Biography)
            .HasMaxLength(4000);

        builder.Property(a => a.Nationality)
            .HasMaxLength(100);

        builder.Property(a => a.CreatedAt)
            .IsRequired();
    }
}
