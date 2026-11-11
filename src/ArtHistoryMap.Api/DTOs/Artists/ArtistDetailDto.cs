namespace ArtHistoryMap.Api.DTOs.Artists;

public class ArtistDetailDto
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string? Biography { get; init; }
    public int? BirthYear { get; init; }
    public int? DeathYear { get; init; }
    public string? Nationality { get; init; }

    public IReadOnlyList<ArtistMovementItemDto> Movements { get; init; } = Array.Empty<ArtistMovementItemDto>();
    public IReadOnlyList<ArtistArtworkItemDto> Artworks { get; init; } = Array.Empty<ArtistArtworkItemDto>();
}

public class ArtistMovementItemDto
{
    public Guid MovementId { get; init; }
    public string MovementName { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public int? StartYear { get; init; }
    public int? EndYear { get; init; }
    public bool IsPrimary { get; init; }
}

public class ArtistArtworkItemDto
{
    public Guid ArtworkId { get; init; }
    public string Title { get; init; } = string.Empty;
    public int? CreationYear { get; init; }
    public string? Medium { get; init; }
    public string? CurrentLocation { get; init; }
}
