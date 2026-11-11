using ArtHistoryMap.Api.DTOs.Artists;
using ArtHistoryMap.Api.DTOs.Common;
using ArtHistoryMap.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ArtHistoryMap.Api.Services;

public class ArtistService : IArtistService
{
    private readonly ArtHistoryDbContext _dbContext;

    public ArtistService(ArtHistoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<ArtistDto>> GetArtistsAsync(PaginationQuery query, CancellationToken cancellationToken = default)
    {
        var baseQuery = _dbContext.Artists
            .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var items = await baseQuery
            .OrderBy(a => a.FullName)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(a => new ArtistDto
            {
                Id = a.Id,
                FullName = a.FullName,
                Nationality = a.Nationality,
                BirthYear = a.BirthYear,
                DeathYear = a.DeathYear,
                Movements = a.ArtistMovements.Select(am => am.ArtMovement.Name).ToList()
            })
            .ToListAsync(cancellationToken);

        return PagedResult<ArtistDto>.Create(items, query.Page, query.PageSize, totalCount);
    }

    public async Task<ArtistDetailDto?> GetArtistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Artists
            .AsNoTracking()
            .Where(a => a.Id == id)
            .Select(a => new ArtistDetailDto
            {
                Id = a.Id,
                FullName = a.FullName,
                Biography = a.Biography,
                BirthYear = a.BirthYear,
                DeathYear = a.DeathYear,
                Nationality = a.Nationality,
                Movements = a.ArtistMovements.Select(am => new ArtistMovementItemDto
                {
                    MovementId = am.ArtMovementId,
                    MovementName = am.ArtMovement.Name,
                    Role = am.Role.ToString(),
                    StartYear = am.StartYear,
                    EndYear = am.EndYear,
                    IsPrimary = am.IsPrimary
                }).ToList(),
                Artworks = a.Artworks.Select(w => new ArtistArtworkItemDto
                {
                    ArtworkId = w.Id,
                    Title = w.Title,
                    CreationYear = w.CreationYear,
                    Medium = w.Medium,
                    CurrentLocation = w.CurrentLocation
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
