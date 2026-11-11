using ArtHistoryMap.Api.DTOs.Artworks;
using ArtHistoryMap.Api.DTOs.Common;

namespace ArtHistoryMap.Api.Services;

public interface IArtworkService
{
    Task<PagedResult<ArtworkDto>> GetArtworksAsync(PaginationQuery query, CancellationToken cancellationToken = default);
    Task<ArtworkDto?> GetArtworkByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
