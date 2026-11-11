using ArtHistoryMap.Api.DTOs.Common;
using ArtHistoryMap.Api.DTOs.Movements;

namespace ArtHistoryMap.Api.Services;

public interface IMovementService
{
    Task<PagedResult<MovementDto>> GetMovementsAsync(PaginationQuery query, CancellationToken cancellationToken = default);
    Task<MovementDetailDto?> GetMovementByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<MovementGraphDto> GetMovementGraphAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MovementRelationItemDto>> GetRelationsByMovementIdAsync(Guid id, CancellationToken cancellationToken = default);
}
