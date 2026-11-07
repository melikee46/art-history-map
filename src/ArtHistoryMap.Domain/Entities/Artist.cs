using ArtHistoryMap.Domain.Common;

namespace ArtHistoryMap.Domain.Entities;

public class Artist : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public int? BirthYear { get; set; }
    public int? DeathYear { get; set; }
    public string? Nationality { get; set; }

    // Navigation Properties
    public ICollection<ArtistMovement> ArtistMovements { get; set; } = new List<ArtistMovement>();
    public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
}
