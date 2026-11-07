using ArtHistoryMap.Domain.Common;

namespace ArtHistoryMap.Domain.Entities;

public class Artwork : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int? CreationYear { get; set; }
    public string? Medium { get; set; } // Örn: Tuval üzerine yağlıboya, Fresk, Mermer
    public string? CurrentLocation { get; set; } // Örn: Musée d'Orsay, Paris

    // Foreign Keys & Navigations
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

    /// <summary>
    /// Eserin doğrudan ait olduğu akım (Opsiyonel / Nullable).
    /// Akım silinse dahi eser korunur (OnDelete: SetNull).
    /// </summary>
    public Guid? PrimaryMovementId { get; set; }
    public ArtMovement? PrimaryMovement { get; set; }
}
