namespace ArtHistoryMap.Api.DTOs.Movements;

/// <summary>
/// Cytoscape.js, D3.js ve Vis.js gibi yönlü çizge (directed graph) kütüphanelerine
/// doğrudan uygun formatta düğüm (nodes) ve kenar (edges) verisi sağlayan model.
/// </summary>
public class MovementGraphDto
{
    public IReadOnlyList<GraphNodeDto> Nodes { get; init; } = Array.Empty<GraphNodeDto>();
    public IReadOnlyList<GraphEdgeDto> Edges { get; init; } = Array.Empty<GraphEdgeDto>();
}

public class GraphNodeDto
{
    public Guid Id { get; init; }
    public string Label { get; init; } = string.Empty;
    public int? StartYear { get; init; }
    public int? EndYear { get; init; }
    public string? OriginRegion { get; init; }
}

public class GraphEdgeDto
{
    public Guid Id { get; init; }
    public Guid SourceId { get; init; }
    public string SourceName { get; init; } = string.Empty;
    public Guid TargetId { get; init; }
    public string TargetName { get; init; } = string.Empty;
    public string RelationType { get; init; } = string.Empty;
    public string? Description { get; init; }
}
