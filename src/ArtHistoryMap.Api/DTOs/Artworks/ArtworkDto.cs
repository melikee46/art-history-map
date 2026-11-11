namespace ArtHistoryMap.Api.DTOs.Artworks;

public class ArtworkDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int? CreationYear { get; init; }
    public string? Medium { get; init; }
    public string? CurrentLocation { get; init; }

    public Guid ArtistId { get; init; }
    public string ArtistName { get; init; } = string.Empty;

    public Guid? PrimaryMovementId { get; init; }
    public string? PrimaryMovementName { get; init; }
}
