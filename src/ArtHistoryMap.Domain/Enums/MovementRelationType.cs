namespace ArtHistoryMap.Domain.Enums;

/// <summary>
/// Sanat akımları arasındaki yönlü (directed) ontolojik ve stilistik ilişki tipleri.
/// Semantik Yön: SourceMovement -> [RelationType] -> TargetMovement
/// </summary>
public enum MovementRelationType
{
    /// <summary>
    /// Kaynak akım, hedef akımı doğrudan biçimsel, felsefi veya teknik olarak etkilemiştir.
    /// Örn: Realizm -> [Influenced] -> Empresyonizm
    /// </summary>
    Influenced = 1,

    /// <summary>
    /// Kaynak akım, hedef akımın estetik/ideolojik ilkelerine doğrudan bir başkaldırı veya tepki olarak doğmuştur.
    /// Örn: Romantizm -> [ReactedAgainst] -> Neoklasizm
    /// </summary>
    ReactedAgainst = 2,

    /// <summary>
    /// Kaynak akım, organik bir dönüşüm süreciyle hedef akıma evrilmiştir.
    /// Örn: Erken Rönesans -> [EvolvedInto] -> Yüksek Rönesans
    /// </summary>
    EvolvedInto = 3,

    /// <summary>
    /// İki akım aynı zaman diliminde paralel var olmuş, karşılıklı diyalog ve rekabet içinde bulunmuştur.
    /// </summary>
    ConcurrentWith = 4
}
