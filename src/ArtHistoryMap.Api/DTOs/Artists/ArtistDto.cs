namespace ArtHistoryMap.Api.DTOs.Artists;

public class ArtistDto
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string? Nationality { get; init; }
    public int? BirthYear { get; init; }
    public int? DeathYear { get; init; }
    public IReadOnlyList<string> Movements { get; init; } = Array.Empty<string>();
}
