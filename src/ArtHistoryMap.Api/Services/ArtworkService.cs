using ArtHistoryMap.Api.DTOs.Artworks;
using ArtHistoryMap.Api.DTOs.Common;
using ArtHistoryMap.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ArtHistoryMap.Api.Services;

public class ArtworkService : IArtworkService
{
    private readonly ArtHistoryDbContext _dbContext;

    public ArtworkService(ArtHistoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<ArtworkDto>> GetArtworksAsync(PaginationQuery query, CancellationToken cancellationToken = default)
    {
        var baseQuery = _dbContext.Artworks
            .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var items = await baseQuery
            .OrderBy(w => w.CreationYear.HasValue ? 0 : 1)
            .ThenBy(w => w.CreationYear)
            .ThenBy(w => w.Title)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(w => new ArtworkDto
            {
                Id = w.Id,
                Title = w.Title,
                CreationYear = w.CreationYear,
                Medium = w.Medium,
                CurrentLocation = w.CurrentLocation,
                ArtistId = w.ArtistId,
                ArtistName = w.Artist.FullName,
                PrimaryMovementId = w.PrimaryMovementId,
                PrimaryMovementName = w.PrimaryMovement != null ? w.PrimaryMovement.Name : null
            })
            .ToListAsync(cancellationToken);

        return PagedResult<ArtworkDto>.Create(items, query.Page, query.PageSize, totalCount);
    }

    public async Task<ArtworkDto?> GetArtworkByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Artworks
            .AsNoTracking()
            .Where(w => w.Id == id)
            .Select(w => new ArtworkDto
            {
                Id = w.Id,
                Title = w.Title,
                CreationYear = w.CreationYear,
                Medium = w.Medium,
                CurrentLocation = w.CurrentLocation,
                ArtistId = w.ArtistId,
                ArtistName = w.Artist.FullName,
                PrimaryMovementId = w.PrimaryMovementId,
                PrimaryMovementName = w.PrimaryMovement != null ? w.PrimaryMovement.Name : null
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
