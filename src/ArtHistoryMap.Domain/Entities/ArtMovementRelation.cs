using ArtHistoryMap.Domain.Enums;

namespace ArtHistoryMap.Domain.Entities;

/// <summary>
/// İki sanat akımı arasındaki yönlü (directed graph edge) ilişkiyi temsil eden varlık.
/// Semantik Model: SourceMovement -> [RelationType] -> TargetMovement
/// Örnek: Realizm (Source) -> [Influenced] -> Empresyonizm (Target)
/// Örnek: Romantizm (Source) -> [ReactedAgainst] -> Neoklasizm (Target)
/// </summary>
public class ArtMovementRelation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// İlişkiyi başlatan / kaynak akım (Öncül veya Tepki veren akım).
    /// </summary>
    public Guid SourceMovementId { get; set; }
    public ArtMovement SourceMovement { get; set; } = null!;

    /// <summary>
    /// İlişkinin hedefi olan akım (Etkilenen veya Tepki duyulan akım).
    /// </summary>
    public Guid TargetMovementId { get; set; }
    public ArtMovement TargetMovement { get; set; } = null!;

    /// <summary>
    /// Yönlü ilişkinin ontolojik türü (Influenced, ReactedAgainst, EvolvedInto, ConcurrentWith).
    /// </summary>
    public MovementRelationType RelationType { get; set; }

    /// <summary>
    /// Bu spesifik tarihsel etkileşimin küratöryel / akademik açıklaması.
    /// </summary>
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
