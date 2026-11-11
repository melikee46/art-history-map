using ArtHistoryMap.Api.DTOs.Artists;
using ArtHistoryMap.Api.DTOs.Common;

namespace ArtHistoryMap.Api.Services;

public interface IArtistService
{
    Task<PagedResult<ArtistDto>> GetArtistsAsync(PaginationQuery query, CancellationToken cancellationToken = default);
    Task<ArtistDetailDto?> GetArtistByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
