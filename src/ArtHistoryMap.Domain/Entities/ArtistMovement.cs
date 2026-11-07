using ArtHistoryMap.Domain.Enums;

namespace ArtHistoryMap.Domain.Entities;

/// <summary>
/// Sanatçı ile Sanat Akımı arasındaki çoktan-çoğa (many-to-many) ilişkiyi
/// tarihsel bağlam ve rollerle zenginleştiren açık join varlığı.
/// </summary>
public class ArtistMovement
{
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

    public Guid ArtMovementId { get; set; }
    public ArtMovement ArtMovement { get; set; } = null!;

    /// <summary>
    /// Sanatçının bu akımdaki tarihsel rolü (Founder, KeyFigure, Follower vb.).
    /// </summary>
    public ArtistMovementRole Role { get; set; } = ArtistMovementRole.KeyFigure;

    /// <summary>
    /// Sanatçının bu akıma dahil olduğu / bu tarzda üretmeye başladığı yıl.
    /// </summary>
    public int? StartYear { get; set; }

    /// <summary>
    /// Sanatçının akımdan koptuğu veya başka bir döneme geçtiği yıl.
    /// </summary>
    public int? EndYear { get; set; }

    /// <summary>
    /// Sanatçının kariyerini en çok tanımlayan birincil akımı mı? (Örn: Monet için Empresyonizm = true).
    /// </summary>
    public bool IsPrimary { get; set; } = false;
}
