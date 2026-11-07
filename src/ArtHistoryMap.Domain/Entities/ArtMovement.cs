using ArtHistoryMap.Domain.Common;

namespace ArtHistoryMap.Domain.Entities;

public class ArtMovement : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public string? OriginRegion { get; set; }

    // Navigation Properties
    /// <summary>
    /// Bu akımın kaynak (başlatan/etkileyen) olduğu dışa doğru yönlü ilişkiler.
    /// </summary>
    public ICollection<ArtMovementRelation> OutgoingRelations { get; set; } = new List<ArtMovementRelation>();

    /// <summary>
    /// Bu akımın hedef (etkilenen/tepki veren) olduğu içe doğru yönlü ilişkiler.
    /// </summary>
    public ICollection<ArtMovementRelation> IncomingRelations { get; set; } = new List<ArtMovementRelation>();

    /// <summary>
    /// Bu akıma bağlı sanatçı eşleşmeleri (explicit join).
    /// </summary>
    public ICollection<ArtistMovement> ArtistMovements { get; set; } = new List<ArtistMovement>();

    /// <summary>
    /// Bu akım altında sınıflandırılan sanat eserleri.
    /// </summary>
    public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
}
