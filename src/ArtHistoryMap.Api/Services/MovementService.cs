using ArtHistoryMap.Api.DTOs.Common;
using ArtHistoryMap.Api.DTOs.Movements;
using ArtHistoryMap.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ArtHistoryMap.Api.Services;

public class MovementService : IMovementService
{
    private readonly ArtHistoryDbContext _dbContext;

    public MovementService(ArtHistoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<MovementDto>> GetMovementsAsync(PaginationQuery query, CancellationToken cancellationToken = default)
    {
        var baseQuery = _dbContext.ArtMovements
            .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        // Kronolojik sıralama: Başlangıç yılı olanlar önce, yılı olmayanlar sonda
        var items = await baseQuery
            .OrderBy(m => m.StartYear.HasValue ? 0 : 1)
            .ThenBy(m => m.StartYear)
            .ThenBy(m => m.Name)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(m => new MovementDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartYear = m.StartYear,
                EndYear = m.EndYear,
                OriginRegion = m.OriginRegion
            })
            .ToListAsync(cancellationToken);

        return PagedResult<MovementDto>.Create(items, query.Page, query.PageSize, totalCount);
    }

    public async Task<MovementDetailDto?> GetMovementByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ArtMovements
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new MovementDetailDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartYear = m.StartYear,
                EndYear = m.EndYear,
                OriginRegion = m.OriginRegion,
                Artists = m.ArtistMovements.Select(am => new MovementArtistItemDto
                {
                    ArtistId = am.ArtistId,
                    FullName = am.Artist.FullName,
                    Role = am.Role.ToString(),
                    StartYear = am.StartYear,
                    EndYear = am.EndYear,
                    IsPrimary = am.IsPrimary
                }).ToList(),
                Artworks = m.Artworks.Select(w => new MovementArtworkItemDto
                {
                    ArtworkId = w.Id,
                    Title = w.Title,
                    CreationYear = w.CreationYear,
                    ArtistName = w.Artist.FullName
                }).ToList(),
                OutgoingRelations = m.OutgoingRelations.Select(r => new MovementRelationItemDto
                {
                    RelationId = r.Id,
                    RelatedMovementId = r.TargetMovementId,
                    RelatedMovementName = r.TargetMovement.Name,
                    RelationType = r.RelationType.ToString(),
                    Description = r.Description
                }).ToList(),
                IncomingRelations = m.IncomingRelations.Select(r => new MovementRelationItemDto
                {
                    RelationId = r.Id,
                    RelatedMovementId = r.SourceMovementId,
                    RelatedMovementName = r.SourceMovement.Name,
                    RelationType = r.RelationType.ToString(),
                    Description = r.Description
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<MovementGraphDto> GetMovementGraphAsync(CancellationToken cancellationToken = default)
    {
        // 1. Tüm düğümleri (akımları) projeksiyonla çek
        var nodes = await _dbContext.ArtMovements
            .AsNoTracking()
            .OrderBy(m => m.StartYear)
            .Select(m => new GraphNodeDto
            {
                Id = m.Id,
                Label = m.Name,
                StartYear = m.StartYear,
                EndYear = m.EndYear,
                OriginRegion = m.OriginRegion
            })
            .ToListAsync(cancellationToken);

        // 2. Tüm yönlü kenarları (ilişkileri) tek seferde çek
        var edges = await _dbContext.ArtMovementRelations
            .AsNoTracking()
            .Select(r => new GraphEdgeDto
            {
                Id = r.Id,
                SourceId = r.SourceMovementId,
                SourceName = r.SourceMovement.Name,
                TargetId = r.TargetMovementId,
                TargetName = r.TargetMovement.Name,
                RelationType = r.RelationType.ToString(),
                Description = r.Description
            })
            .ToListAsync(cancellationToken);

        return new MovementGraphDto
        {
            Nodes = nodes,
            Edges = edges
        };
    }

    public async Task<IReadOnlyList<MovementRelationItemDto>> GetRelationsByMovementIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ArtMovementRelations
            .AsNoTracking()
            .Where(r => r.SourceMovementId == id || r.TargetMovementId == id)
            .Select(r => new MovementRelationItemDto
            {
                RelationId = r.Id,
                RelatedMovementId = r.SourceMovementId == id ? r.TargetMovementId : r.SourceMovementId,
                RelatedMovementName = r.SourceMovementId == id ? r.TargetMovement.Name : r.SourceMovement.Name,
                RelationType = r.RelationType.ToString(),
                Description = r.Description
            })
            .ToListAsync(cancellationToken);
    }
}
