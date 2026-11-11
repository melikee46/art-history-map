using ArtHistoryMap.Api.DTOs.Artists;
using ArtHistoryMap.Api.DTOs.Common;
using ArtHistoryMap.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtHistoryMap.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    /// <summary>
    /// Sanatçıları alfabetik sıralı ve sayfalı olarak listeler.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ArtistDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<ArtistDto>>> GetArtists(
        [FromQuery] PaginationQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _artistService.GetArtistsAsync(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Belirtilen ID'ye sahip sanatçının detaylarını, akım üyeliklerini ve eserlerini getirir.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArtistDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArtistDetailDto>> GetArtistById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var artist = await _artistService.GetArtistByIdAsync(id, cancellationToken);
        if (artist == null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Kayıt Bulunamadı",
                Detail = $"ID: '{id}' olan sanatçı bulunamadı."
            });
        }

        return Ok(artist);
    }
}
