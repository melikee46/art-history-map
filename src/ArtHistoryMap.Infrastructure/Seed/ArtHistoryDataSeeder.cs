using ArtHistoryMap.Domain.Entities;
using ArtHistoryMap.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ArtHistoryMap.Infrastructure.Seed;

public static class ArtHistoryDataSeeder
{
    // Deterministik GUID'ler
    public static readonly Guid RenaissanceId = Guid.Parse("11111111-1111-1111-1111-111111111101");
    public static readonly Guid MannerismId = Guid.Parse("11111111-1111-1111-1111-111111111102");
    public static readonly Guid BaroqueId = Guid.Parse("11111111-1111-1111-1111-111111111103");
    public static readonly Guid RococoId = Guid.Parse("11111111-1111-1111-1111-111111111104");
    public static readonly Guid NeoclassicismId = Guid.Parse("11111111-1111-1111-1111-111111111105");
    public static readonly Guid RomanticismId = Guid.Parse("11111111-1111-1111-1111-111111111106");
    public static readonly Guid RealismId = Guid.Parse("11111111-1111-1111-1111-111111111107");
    public static readonly Guid ImpressionismId = Guid.Parse("11111111-1111-1111-1111-111111111108");
    public static readonly Guid PostImpressionismId = Guid.Parse("11111111-1111-1111-1111-111111111109");
    public static readonly Guid CubismId = Guid.Parse("11111111-1111-1111-1111-111111111110");
    public static readonly Guid SurrealismId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    // Sanatçı GUID'leri
    public static readonly Guid DaVinciId = Guid.Parse("22222222-2222-2222-2222-222222222201");
    public static readonly Guid CaravaggioId = Guid.Parse("22222222-2222-2222-2222-222222222202");
    public static readonly Guid DavidId = Guid.Parse("22222222-2222-2222-2222-222222222203");
    public static readonly Guid FriedrichId = Guid.Parse("22222222-2222-2222-2222-222222222204");
    public static readonly Guid CourbetId = Guid.Parse("22222222-2222-2222-2222-222222222205");
    public static readonly Guid MonetId = Guid.Parse("22222222-2222-2222-2222-222222222206");
    public static readonly Guid VanGoghId = Guid.Parse("22222222-2222-2222-2222-222222222207");
    public static readonly Guid CezanneId = Guid.Parse("22222222-2222-2222-2222-222222222208");
    public static readonly Guid PicassoId = Guid.Parse("22222222-2222-2222-2222-222222222209");
    public static readonly Guid DaliId = Guid.Parse("22222222-2222-2222-2222-222222222210");

    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static void SeedData(ModelBuilder modelBuilder)
    {
        SeedArtMovements(modelBuilder);
        SeedArtMovementRelations(modelBuilder);
        SeedArtists(modelBuilder);
        SeedArtistMovements(modelBuilder);
        SeedArtworks(modelBuilder);
    }

    private static void SeedArtMovements(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtMovement>().HasData(
            new ArtMovement
            {
                Id = RenaissanceId,
                Name = "Rönesans",
                Description = "Klasik Antikite'nin yeniden keşfi, hümanizm, lineer perspektif ve anatominin sanata kazandırıldığı köklü dönüşüm dönemi.",
                StartYear = 1400,
                EndYear = 1527,
                OriginRegion = "Floransa, İtalya",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = MannerismId,
                Name = "Manierizm",
                Description = "Yüksek Rönesans'ın kusursuz orantı ve simetrisine karşı uzatılmış figürler, tekinsiz kompozisyonlar ve yapay zarafetle şekillenen üslup.",
                StartYear = 1520,
                EndYear = 1600,
                OriginRegion = "İtalya",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = BaroqueId,
                Name = "Barok",
                Description = "Karşı-Reformasyon desteğiyle gelişen; chiaroscuro (sert ışık-gölge), yoğun hareket, tiyatral drama ve duygusal coşku barındıran akım.",
                StartYear = 1600,
                EndYear = 1750,
                OriginRegion = "Roma, İtalya",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = RococoId,
                Name = "Rokoko",
                Description = "Geç Barok'un saray estetiği; pastel tonlar, asimetrik kıvrımlar, neşeli ve hedonist aristokratik temalar.",
                StartYear = 1730,
                EndYear = 1780,
                OriginRegion = "Paris, Fransa",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = NeoclassicismId,
                Name = "Neoklasizm",
                Description = "Aydınlanma Çağı felsefesiyle Antik Yunan ve Roma'nın ahlaki ciddiyetine, net çizgilerine ve dengeli rasyonalizmine dönüş hareketi.",
                StartYear = 1760,
                EndYear = 1830,
                OriginRegion = "Fransa / İtalya",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = RomanticismId,
                Name = "Romantizm",
                Description = "Katı akılcılığa ve sanayileşmeye isyan; doğanın yüceliği (sublime), bireysel iç dünya, fırtınalı duygular ve gizem arayışı.",
                StartYear = 1800,
                EndYear = 1850,
                OriginRegion = "Almanya / İngiltere / Fransa",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = RealismId,
                Name = "Realizm",
                Description = "Tarihsel ve mitolojik süslemeleri reddedip çağdaş yaşamı, işçi sınıfını ve somut gerçekliği olduğu gibi aktaran devrimci akım.",
                StartYear = 1840,
                EndYear = 1880,
                OriginRegion = "Fransa",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = ImpressionismId,
                Name = "Empresyonizm",
                Description = "Atölyeden açık havaya (en plein air) çıkış; ışığın anlık titreşimlerini ve optik etkilerini hızlı fırça darbeleriyle tuvale geçiren modern hareket.",
                StartYear = 1865,
                EndYear = 1886,
                OriginRegion = "Paris, Fransa",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = PostImpressionismId,
                Name = "Post-Empresyonizm",
                Description = "Empresyonizmin saf ışık kaygısına karşı biçimsel yapı, sembolik renk ve sanatçının öznel ruh halini merkeze alan öncü arayışlar.",
                StartYear = 1886,
                EndYear = 1905,
                OriginRegion = "Fransa",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = CubismId,
                Name = "Kübizm",
                Description = "Nesneleri tek bir perspektif yerine geometrik planlara ayırıp aynı anda çoklu bakış açısından sunan 20. yüzyıl avant-garde devrimi.",
                StartYear = 1907,
                EndYear = 1919,
                OriginRegion = "Paris, Fransa",
                CreatedAt = SeedTimestamp
            },
            new ArtMovement
            {
                Id = SurrealismId,
                Name = "Sürrealizm",
                Description = "Bilinçaltı, rüyalar ve psikanalizin rehberliğinde mantığın denetiminden kurtarılmış saf psişik otomatizm.",
                StartYear = 1924,
                EndYear = 1966,
                OriginRegion = "Paris, Fransa",
                CreatedAt = SeedTimestamp
            }
        );
    }

    private static void SeedArtMovementRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtMovementRelation>().HasData(
            // 1. Rönesans -> Barok (Etkiledi: Rönesans anatomisi ve perspektifi Barok'u besledi)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333301"),
                SourceMovementId = RenaissanceId,
                TargetMovementId = BaroqueId,
                RelationType = MovementRelationType.Influenced,
                Description = "Rönesans'ın anatomi, kompozisyon ve perspektif birikimi Barok ustalarca devralınıp dramatize edilmiştir.",
                CreatedAt = SeedTimestamp
            },
            // 2. Manierizm -> Rönesans (Tepki: Manierizm Rönesans'ın dengeli harmonisine tepki duydu)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333302"),
                SourceMovementId = MannerismId,
                TargetMovementId = RenaissanceId,
                RelationType = MovementRelationType.ReactedAgainst,
                Description = "Manieristler Yüksek Rönesans'ın mükemmel simetri ve durağan dengesini yapay ve sıkıcı bularak deformasyona yöneldi.",
                CreatedAt = SeedTimestamp
            },
            // 3. Barok -> Rokoko (Evrildi: Barok saray sanatı zarif Rokoko'ya dönüştü)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333303"),
                SourceMovementId = BaroqueId,
                TargetMovementId = RococoId,
                RelationType = MovementRelationType.EvolvedInto,
                Description = "Geç Barok dönemin ağır ihtişamı Fransız saraylarında hafifleyerek pastel renkli Rokoko üslubuna evrilmiştir.",
                CreatedAt = SeedTimestamp
            },
            // 4. Neoklasizm -> Rokoko (Tepki: Neoklasizm Rokoko'nun aşırı süsüne ve ahlaki hafifliğine sert tepki gösterdi)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333304"),
                SourceMovementId = NeoclassicismId,
                TargetMovementId = RococoId,
                RelationType = MovementRelationType.ReactedAgainst,
                Description = "Aydınlanma düşünürleri ve Neoklasikler Rokoko'yu yozlaşmış aristokratik zevk olarak görüp reddetmiştir.",
                CreatedAt = SeedTimestamp
            },
            // 5. Romantizm -> Neoklasizm (Tepki: Romantizm Neoklasizm'in soğuk rasyonalizmine başkaldırdı)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333305"),
                SourceMovementId = RomanticismId,
                TargetMovementId = NeoclassicismId,
                RelationType = MovementRelationType.ReactedAgainst,
                Description = "Romantizm katı akademik kuralları ve Antikite taklitçiliğini bırakıp bireysel duygu ve doğa coşkusunu yüceltti.",
                CreatedAt = SeedTimestamp
            },
            // 6. Realizm -> Romantizm (Tepki: Realizm Romantizm'in gerçeklikten kaçışına tepki gösterdi)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333306"),
                SourceMovementId = RealismId,
                TargetMovementId = RomanticismId,
                RelationType = MovementRelationType.ReactedAgainst,
                Description = "Courbet öncülüğündeki Realistler Romantik mitolojiyi ve aşırı duygusallığı reddedip 'göremediğim meleği çizmem' demiştir.",
                CreatedAt = SeedTimestamp
            },
            // 7. Realizm -> Empresyonizm (Etkiledi: Gerçeği ve anı resmetme felsefesi)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333307"),
                SourceMovementId = RealismId,
                TargetMovementId = ImpressionismId,
                RelationType = MovementRelationType.Influenced,
                Description = "Realizmin modern kentsel hayatı ve gündeliği resmetme ısrarı Empresyonistleri sokağa ve açık havaya taşımıştır.",
                CreatedAt = SeedTimestamp
            },
            // 8. Empresyonizm -> Neoklasizm (Tepki: Akademiye ve atölye dogmalarına karşı isyan)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333308"),
                SourceMovementId = ImpressionismId,
                TargetMovementId = NeoclassicismId,
                RelationType = MovementRelationType.ReactedAgainst,
                Description = "Empresyonistler Salon de Paris'in Neoklasik akademik kurallarına, donuk stüdyo aydınlatmasına açıkça meydan okumuştur.",
                CreatedAt = SeedTimestamp
            },
            // 9. Empresyonizm -> Post-Empresyonizm (Evrildi: Palet ve ışık bilgisinin devri)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333309"),
                SourceMovementId = ImpressionismId,
                TargetMovementId = PostImpressionismId,
                RelationType = MovementRelationType.EvolvedInto,
                Description = "Post-Empresyonistler serbest fırça ve parlak renk teorisini doğrudan Empresyonist öncüllerinden öğrenerek evrildi.",
                CreatedAt = SeedTimestamp
            },
            // 10. Post-Empresyonizm -> Empresyonizm (Tepki: Biçimsizlik eleştirisi - Çift yönlü ilişki örneği!)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333310"),
                SourceMovementId = PostImpressionismId,
                TargetMovementId = ImpressionismId,
                RelationType = MovementRelationType.ReactedAgainst,
                Description = "Cézanne Empresyonizm'i 'müzelerdeki sanat gibi sağlam ve kalıcı kılmak' isteyerek saf anlık ışık oyununa tepki gösterdi.",
                CreatedAt = SeedTimestamp
            },
            // 11. Post-Empresyonizm -> Kübizm (Etkiledi: Cézanne'ın geometrisi)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333311"),
                SourceMovementId = PostImpressionismId,
                TargetMovementId = CubismId,
                RelationType = MovementRelationType.Influenced,
                Description = "Paul Cézanne'ın 'doğayı silindir, küre ve koni olarak ele alınız' prensibi Kübizm'in yapıtaşı oldu.",
                CreatedAt = SeedTimestamp
            },
            // 12. Kübizm -> Sürrealizm (Etkiledi: Mekan ve perspektifi parçalama gücü)
            new ArtMovementRelation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333312"),
                SourceMovementId = CubismId,
                TargetMovementId = SurrealismId,
                RelationType = MovementRelationType.Influenced,
                Description = "Kübizmin nesneyi geleneksel uzamdan koparması Sürrealistlerin rüya ve bilinçaltı mekan kurgularına zemin hazırladı.",
                CreatedAt = SeedTimestamp
            }
        );
    }

    private static void SeedArtists(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>().HasData(
            new Artist
            {
                Id = DaVinciId,
                FullName = "Leonardo da Vinci",
                Biography = "İtalyan hezârfen, ressam, anatomist ve mucit. Yüksek Rönesans'ın evrensel dehası.",
                BirthYear = 1452,
                DeathYear = 1519,
                Nationality = "İtalyan",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = CaravaggioId,
                FullName = "Michelangelo Merisi da Caravaggio",
                Biography = "Işık ve gölgeyi dramatik bir tezatla kullanan (chiaroscuro), Barok resim okulunun kurucu dehası.",
                BirthYear = 1571,
                DeathYear = 1610,
                Nationality = "İtalyan",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = DavidId,
                FullName = "Jacques-Louis David",
                Biography = "Fransız Devrimi'nin ve Neoklasik resmin baş aktörü, antik kahramanlık ve erdem ideali ressamı.",
                BirthYear = 1748,
                DeathYear = 1825,
                Nationality = "Fransız",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = FriedrichId,
                FullName = "Caspar David Friedrich",
                Biography = "Doğanın ezici ihtişamı ve insanın faniliği üzerine meditasyonlar üreten Alman Romantik manzara ustası.",
                BirthYear = 1774,
                DeathYear = 1840,
                Nationality = "Alman",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = CourbetId,
                FullName = "Gustave Courbet",
                Biography = "Realizm akımının öncüsü ve teorisyeni; burjuva akademizmine karşı işçileri ve kırsal hayatı yücelten devrimci.",
                BirthYear = 1819,
                DeathYear = 1877,
                Nationality = "Fransız",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = MonetId,
                FullName = "Claude Monet",
                Biography = "Empresyonizm akımına adını veren 'İzlenim: Gün Doğumu' tablosunun yaratıcısı, ışık ve renk araştırmacısı.",
                BirthYear = 1840,
                DeathYear = 1926,
                Nationality = "Fransız",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = VanGoghId,
                FullName = "Vincent van Gogh",
                Biography = "Duygusal yoğunluğu cesur fırça darbeleri ve çiğ renklerle dışavuran Hollandalı Post-Empresyonist deha.",
                BirthYear = 1853,
                DeathYear = 1890,
                Nationality = "Hollandalı",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = CezanneId,
                FullName = "Paul Cézanne",
                Biography = "Modern sanatın babası olarak kabul edilen, doğadaki geometrik düzeni arayan Post-Empresyonist usta.",
                BirthYear = 1839,
                DeathYear = 1906,
                Nationality = "Fransız",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = PicassoId,
                FullName = "Pablo Picasso",
                Biography = "Kübizmin kurucusu, 20. yüzyıl görsel sanatlarını baştan tanımlayan İspanyol ressam ve heykeltıraş.",
                BirthYear = 1881,
                DeathYear = 1973,
                Nationality = "İspanyol",
                CreatedAt = SeedTimestamp
            },
            new Artist
            {
                Id = DaliId,
                FullName = "Salvador Dalí",
                Biography = "Paranoyak-eleştirel yöntemle rüya imgelerini ve bilinçaltı fantezilerini hiper-gerçekçi işleyen Sürrealist ikon.",
                BirthYear = 1904,
                DeathYear = 1989,
                Nationality = "İspanyol",
                CreatedAt = SeedTimestamp
            }
        );
    }

    private static void SeedArtistMovements(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtistMovement>().HasData(
            new ArtistMovement { ArtistId = DaVinciId, ArtMovementId = RenaissanceId, Role = ArtistMovementRole.KeyFigure, StartYear = 1470, EndYear = 1519, IsPrimary = true },
            new ArtistMovement { ArtistId = CaravaggioId, ArtMovementId = BaroqueId, Role = ArtistMovementRole.Founder, StartYear = 1592, EndYear = 1610, IsPrimary = true },
            new ArtistMovement { ArtistId = DavidId, ArtMovementId = NeoclassicismId, Role = ArtistMovementRole.KeyFigure, StartYear = 1775, EndYear = 1825, IsPrimary = true },
            new ArtistMovement { ArtistId = FriedrichId, ArtMovementId = RomanticismId, Role = ArtistMovementRole.KeyFigure, StartYear = 1798, EndYear = 1840, IsPrimary = true },
            new ArtistMovement { ArtistId = CourbetId, ArtMovementId = RealismId, Role = ArtistMovementRole.Founder, StartYear = 1848, EndYear = 1877, IsPrimary = true },
            new ArtistMovement { ArtistId = MonetId, ArtMovementId = ImpressionismId, Role = ArtistMovementRole.Founder, StartYear = 1865, EndYear = 1926, IsPrimary = true },
            new ArtistMovement { ArtistId = VanGoghId, ArtMovementId = PostImpressionismId, Role = ArtistMovementRole.KeyFigure, StartYear = 1886, EndYear = 1890, IsPrimary = true },
            new ArtistMovement { ArtistId = CezanneId, ArtMovementId = PostImpressionismId, Role = ArtistMovementRole.Pioneer, StartYear = 1875, EndYear = 1906, IsPrimary = true },
            new ArtistMovement { ArtistId = PicassoId, ArtMovementId = CubismId, Role = ArtistMovementRole.Founder, StartYear = 1907, EndYear = 1919, IsPrimary = true },
            new ArtistMovement { ArtistId = DaliId, ArtMovementId = SurrealismId, Role = ArtistMovementRole.KeyFigure, StartYear = 1929, EndYear = 1940, IsPrimary = true }
        );
    }

    private static void SeedArtworks(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artwork>().HasData(
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444401"),
                ArtistId = DaVinciId,
                PrimaryMovementId = RenaissanceId,
                Title = "Mona Lisa",
                CreationYear = 1503,
                Medium = "Kavak pano üzerine yağlıboya",
                CurrentLocation = "Musée du Louvre, Paris",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444402"),
                ArtistId = CaravaggioId,
                PrimaryMovementId = BaroqueId,
                Title = "The Calling of Saint Matthew",
                CreationYear = 1600,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "San Luigi dei Francesi, Roma",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444403"),
                ArtistId = DavidId,
                PrimaryMovementId = NeoclassicismId,
                Title = "The Death of Marat",
                CreationYear = 1793,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Musées Royaux des Beaux-Arts, Brüksel",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444404"),
                ArtistId = FriedrichId,
                PrimaryMovementId = RomanticismId,
                Title = "Wanderer above the Sea of Fog",
                CreationYear = 1818,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Hamburger Kunsthalle, Hamburg",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444405"),
                ArtistId = CourbetId,
                PrimaryMovementId = RealismId,
                Title = "The Stone Breakers",
                CreationYear = 1849,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Gemäldegalerie Alte Meister, Dresden (Kayıp/Yok oldu)",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444406"),
                ArtistId = MonetId,
                PrimaryMovementId = ImpressionismId,
                Title = "Impression, Sunrise",
                CreationYear = 1872,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Musée Marmottan Monet, Paris",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444407"),
                ArtistId = VanGoghId,
                PrimaryMovementId = PostImpressionismId,
                Title = "The Starry Night",
                CreationYear = 1889,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Museum of Modern Art (MoMA), New York",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444408"),
                ArtistId = CezanneId,
                PrimaryMovementId = PostImpressionismId,
                Title = "Mont Sainte-Victoire seen from Bellevue",
                CreationYear = 1885,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Barnes Foundation, Philadelphia",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444409"),
                ArtistId = PicassoId,
                PrimaryMovementId = CubismId,
                Title = "Les Demoiselles d'Avignon",
                CreationYear = 1907,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Museum of Modern Art (MoMA), New York",
                CreatedAt = SeedTimestamp
            },
            new Artwork
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444410"),
                ArtistId = DaliId,
                PrimaryMovementId = SurrealismId,
                Title = "The Persistence of Memory",
                CreationYear = 1931,
                Medium = "Tuval üzerine yağlıboya",
                CurrentLocation = "Museum of Modern Art (MoMA), New York",
                CreatedAt = SeedTimestamp
            }
        );
    }
}
