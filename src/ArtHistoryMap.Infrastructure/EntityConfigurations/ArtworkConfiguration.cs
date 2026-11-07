using ArtHistoryMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtHistoryMap.Infrastructure.EntityConfigurations;

public class ArtworkConfiguration : IEntityTypeConfiguration<Artwork>
{
    public void Configure(EntityTypeBuilder<Artwork> builder)
    {
        builder.ToTable("artworks");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasIndex(w => w.Title);

        builder.Property(w => w.Medium)
            .HasMaxLength(150);

        builder.Property(w => w.CurrentLocation)
            .HasMaxLength(250);

        builder.Property(w => w.CreatedAt)
            .IsRequired();

        // KRİTİK CASCADE KARARI:
        // Sanatçı silinirse altındaki sanat eserleri ASLA sessizce silinmemelidir!
        // DeleteBehavior.Restrict sayesinde kazaen veri kaybı DB seviyesinde engellenir.
        builder.HasOne(w => w.Artist)
            .WithMany(a => a.Artworks)
            .HasForeignKey(w => w.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);

        // Akım silinirse eserin akım etiketi boşa çıkar (null), ancak eser ve sanatçı bağı korunur.
        builder.HasOne(w => w.PrimaryMovement)
            .WithMany(m => m.Artworks)
            .HasForeignKey(w => w.PrimaryMovementId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
