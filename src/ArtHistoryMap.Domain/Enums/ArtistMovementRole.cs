namespace ArtHistoryMap.Domain.Enums;

/// <summary>
/// Sanatçının akım içerisindeki rolü ve tarihsel pozisyonu.
/// </summary>
public enum ArtistMovementRole
{
    Founder = 1,      // Akımı başlatan, manifestosunu yazan kurucu (Örn: Kübizm için Picasso)
    KeyFigure = 2,    // Akımın ana gövdesini temsil eden kilit figür (Örn: Empresyonizm için Monet)
    Pioneer = 3,      // Akımın zeminini hazırlayan öncü isim
    Follower = 4,     // Akımın ikinci kuşak veya bölgesel takipçisi
    Associate = 5     // Akımla organik bağ kurup sergilerine katılan ama özerk kalan isim
}
