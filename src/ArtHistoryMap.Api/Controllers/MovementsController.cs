using ArtHistoryMap.Api.DTOs.Common;
using ArtHistoryMap.Api.DTOs.Movements;
using ArtHistoryMap.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtHistoryMap.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovementsController : ControllerBase
{
    private readonly IMovementService _movementService;

    public MovementsController(IMovementService movementService)
    {
        _movementService = movementService;
    }

    /// <summary>
    /// Sanat akımlarını kronolojik sıralı ve sayfalı olarak listeler.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<MovementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<MovementDto>>> GetMovements(
        [FromQuery] PaginationQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _movementService.GetMovementsAsync(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Cytoscape.js, D3.js ve interaktif haritalar için tüm akımları ve yönlü etkileşim kenarlarını döndürür.
    /// </summary>
    [HttpGet("graph")]
    [ProducesResponseType(typeof(MovementGraphDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<MovementGraphDto>> GetMovementGraph(CancellationToken cancellationToken)
    {
        var result = await _movementService.GetMovementGraphAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Belirtilen ID'ye sahip sanat akımının detaylarını, sanatçılarını, eserlerini ve ilişkilerini getirir.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MovementDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovementDetailDto>> GetMovementById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var movement = await _movementService.GetMovementByIdAsync(id, cancellationToken);
        if (movement == null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Kayıt Bulunamadı",
                Detail = $"ID: '{id}' olan sanat akımı bulunamadı."
            });
        }

        return Ok(movement);
    }

    /// <summary>
    /// Belirli bir akımın diğer akımlarla olan tüm yönlü etkileşim ve başkaldırı ilişkilerini listeler.
    /// </summary>
    [HttpGet("{id:guid}/relations")]
    [ProducesResponseType(typeof(IReadOnlyList<MovementRelationItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MovementRelationItemDto>>> GetMovementRelations(
        Guid id,
        CancellationToken cancellationToken)
    {
        var relations = await _movementService.GetRelationsByMovementIdAsync(id, cancellationToken);
        return Ok(relations);
    }
}
