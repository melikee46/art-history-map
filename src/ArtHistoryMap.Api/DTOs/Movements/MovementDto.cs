namespace ArtHistoryMap.Api.DTOs.Movements;

public class MovementDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int? StartYear { get; init; }
    public int? EndYear { get; init; }
    public string? OriginRegion { get; init; }
}
