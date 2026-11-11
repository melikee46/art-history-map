namespace ArtHistoryMap.Api.DTOs.Movements;

public class MovementDetailDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int? StartYear { get; init; }
    public int? EndYear { get; init; }
    public string? OriginRegion { get; init; }

    public IReadOnlyList<MovementArtistItemDto> Artists { get; init; } = Array.Empty<MovementArtistItemDto>();
    public IReadOnlyList<MovementArtworkItemDto> Artworks { get; init; } = Array.Empty<MovementArtworkItemDto>();
    public IReadOnlyList<MovementRelationItemDto> OutgoingRelations { get; init; } = Array.Empty<MovementRelationItemDto>();
    public IReadOnlyList<MovementRelationItemDto> IncomingRelations { get; init; } = Array.Empty<MovementRelationItemDto>();
}

public class MovementArtistItemDto
{
    public Guid ArtistId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public int? StartYear { get; init; }
    public int? EndYear { get; init; }
    public bool IsPrimary { get; init; }
}

public class MovementArtworkItemDto
{
    public Guid ArtworkId { get; init; }
    public string Title { get; init; } = string.Empty;
    public int? CreationYear { get; init; }
    public string ArtistName { get; init; } = string.Empty;
}

public class MovementRelationItemDto
{
    public Guid RelationId { get; init; }
    public Guid RelatedMovementId { get; init; }
    public string RelatedMovementName { get; init; } = string.Empty;
    public string RelationType { get; init; } = string.Empty;
    public string? Description { get; init; }
}
