using ArtHistoryMap.Api.DTOs.Artworks;
using ArtHistoryMap.Api.DTOs.Common;
using ArtHistoryMap.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtHistoryMap.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtworksController : ControllerBase
{
    private readonly IArtworkService _artworkService;

    public ArtworksController(IArtworkService artworkService)
    {
        _artworkService = artworkService;
    }

    /// <summary>
    /// Sanat eserlerini kronolojik ve sayfalı olarak listeler.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ArtworkDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<ArtworkDto>>> GetArtworks(
        [FromQuery] PaginationQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _artworkService.GetArtworksAsync(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Belirtilen ID'ye sahip eserin detaylarını getirir.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArtworkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtworkDto>> GetArtworkById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var artwork = await _artworkService.GetArtworkByIdAsync(id, cancellationToken);
        if (artwork == null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Kayıt Bulunamadı",
                Detail = $"ID: '{id}' olan sanat eseri bulunamadı."
            });
        }

        return Ok(artwork);
    }
}
