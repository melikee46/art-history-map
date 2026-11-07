using ArtHistoryMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtHistoryMap.Infrastructure.EntityConfigurations;

public class ArtMovementRelationConfiguration : IEntityTypeConfiguration<ArtMovementRelation>
{
    public void Configure(EntityTypeBuilder<ArtMovementRelation> builder)
    {
        builder.ToTable("art_movement_relations", t =>
        {
            // Bir akımın kendi kendini etkilemesini engelleyen self-loop check constraint
            t.HasCheckConstraint("CK_ArtMovementRelation_NoSelfReference", "\"SourceMovementId\" <> \"TargetMovementId\"");
        });

        builder.HasKey(r => r.Id);

        // Yönlü İlişki 1: Kaynak Akım (Etkileyen/Öncül)
        // Akım silindiğinde ilişkili graf kenarları da temizlenir (Cascade)
        builder.HasOne(r => r.SourceMovement)
            .WithMany(m => m.OutgoingRelations)
            .HasForeignKey(r => r.SourceMovementId)
            .OnDelete(DeleteBehavior.Cascade);

        // Yönlü İlişki 2: Hedef Akım (Etkilenen/Tepki Veren)
        // Hedef akım silindiğinde de kenar silinir (Cascade)
        builder.HasOne(r => r.TargetMovement)
            .WithMany(m => m.IncomingRelations)
            .HasForeignKey(r => r.TargetMovementId)
            .OnDelete(DeleteBehavior.Cascade);

        // Enum -> String dönüşümü (Veritabanında okunabilirlik ve C#'ta tip güvenliği)
        builder.Property(r => r.RelationType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasMaxLength(1500);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        // COMPOSITE UNIQUE INDEX:
        // Aynı iki akım arasında aynı tipte birden fazla mükerrer ilişki kurulamaz!
        // Ancak farklı tipte ilişkiler kurulabilir (Örn: Hem EvolvedInto hem ReactedAgainst).
        builder.HasIndex(r => new { r.SourceMovementId, r.TargetMovementId, r.RelationType })
            .IsUnique();
    }
}
