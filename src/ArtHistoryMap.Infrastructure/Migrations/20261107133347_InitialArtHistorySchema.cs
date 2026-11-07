using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArtHistoryMap.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialArtHistorySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "art_movements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(2500)", maxLength: 2500, nullable: false),
                    StartYear = table.Column<int>(type: "integer", nullable: true),
                    EndYear = table.Column<int>(type: "integer", nullable: true),
                    OriginRegion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_art_movements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Biography = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    BirthYear = table.Column<int>(type: "integer", nullable: true),
                    DeathYear = table.Column<int>(type: "integer", nullable: true),
                    Nationality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "art_movement_relations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceMovementId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetMovementId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_art_movement_relations", x => x.Id);
                    table.CheckConstraint("CK_ArtMovementRelation_NoSelfReference", "\"SourceMovementId\" <> \"TargetMovementId\"");
                    table.ForeignKey(
                        name: "FK_art_movement_relations_art_movements_SourceMovementId",
                        column: x => x.SourceMovementId,
                        principalTable: "art_movements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_art_movement_relations_art_movements_TargetMovementId",
                        column: x => x.TargetMovementId,
                        principalTable: "art_movements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artist_movements",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtMovementId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartYear = table.Column<int>(type: "integer", nullable: true),
                    EndYear = table.Column<int>(type: "integer", nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artist_movements", x => new { x.ArtistId, x.ArtMovementId });
                    table.ForeignKey(
                        name: "FK_artist_movements_art_movements_ArtMovementId",
                        column: x => x.ArtMovementId,
                        principalTable: "art_movements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_artist_movements_artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreationYear = table.Column<int>(type: "integer", nullable: true),
                    Medium = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CurrentLocation = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ArtistId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrimaryMovementId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_artworks_art_movements_PrimaryMovementId",
                        column: x => x.PrimaryMovementId,
                        principalTable: "art_movements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_artworks_artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "art_movements",
                columns: new[] { "Id", "CreatedAt", "Description", "EndYear", "Name", "OriginRegion", "StartYear", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111101"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Klasik Antikite'nin yeniden keşfi, hümanizm, lineer perspektif ve anatominin sanata kazandırıldığı köklü dönüşüm dönemi.", 1527, "Rönesans", "Floransa, İtalya", 1400, null },
                    { new Guid("11111111-1111-1111-1111-111111111102"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek Rönesans'ın kusursuz orantı ve simetrisine karşı uzatılmış figürler, tekinsiz kompozisyonlar ve yapay zarafetle şekillenen üslup.", 1600, "Manierizm", "İtalya", 1520, null },
                    { new Guid("11111111-1111-1111-1111-111111111103"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Karşı-Reformasyon desteğiyle gelişen; chiaroscuro (sert ışık-gölge), yoğun hareket, tiyatral drama ve duygusal coşku barındıran akım.", 1750, "Barok", "Roma, İtalya", 1600, null },
                    { new Guid("11111111-1111-1111-1111-111111111104"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Geç Barok'un saray estetiği; pastel tonlar, asimetrik kıvrımlar, neşeli ve hedonist aristokratik temalar.", 1780, "Rokoko", "Paris, Fransa", 1730, null },
                    { new Guid("11111111-1111-1111-1111-111111111105"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Aydınlanma Çağı felsefesiyle Antik Yunan ve Roma'nın ahlaki ciddiyetine, net çizgilerine ve dengeli rasyonalizmine dönüş hareketi.", 1830, "Neoklasizm", "Fransa / İtalya", 1760, null },
                    { new Guid("11111111-1111-1111-1111-111111111106"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Katı akılcılığa ve sanayileşmeye isyan; doğanın yüceliği (sublime), bireysel iç dünya, fırtınalı duygular ve gizem arayışı.", 1850, "Romantizm", "Almanya / İngiltere / Fransa", 1800, null },
                    { new Guid("11111111-1111-1111-1111-111111111107"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tarihsel ve mitolojik süslemeleri reddedip çağdaş yaşamı, işçi sınıfını ve somut gerçekliği olduğu gibi aktaran devrimci akım.", 1880, "Realizm", "Fransa", 1840, null },
                    { new Guid("11111111-1111-1111-1111-111111111108"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Atölyeden açık havaya (en plein air) çıkış; ışığın anlık titreşimlerini ve optik etkilerini hızlı fırça darbeleriyle tuvale geçiren modern hareket.", 1886, "Empresyonizm", "Paris, Fransa", 1865, null },
                    { new Guid("11111111-1111-1111-1111-111111111109"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Empresyonizmin saf ışık kaygısına karşı biçimsel yapı, sembolik renk ve sanatçının öznel ruh halini merkeze alan öncü arayışlar.", 1905, "Post-Empresyonizm", "Fransa", 1886, null },
                    { new Guid("11111111-1111-1111-1111-111111111110"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nesneleri tek bir perspektif yerine geometrik planlara ayırıp aynı anda çoklu bakış açısından sunan 20. yüzyıl avant-garde devrimi.", 1919, "Kübizm", "Paris, Fransa", 1907, null },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bilinçaltı, rüyalar ve psikanalizin rehberliğinde mantığın denetiminden kurtarılmış saf psişik otomatizm.", 1966, "Sürrealizm", "Paris, Fransa", 1924, null }
                });

            migrationBuilder.InsertData(
                table: "artists",
                columns: new[] { "Id", "Biography", "BirthYear", "CreatedAt", "DeathYear", "FullName", "Nationality", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222201"), "İtalyan hezârfen, ressam, anatomist ve mucit. Yüksek Rönesans'ın evrensel dehası.", 1452, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1519, "Leonardo da Vinci", "İtalyan", null },
                    { new Guid("22222222-2222-2222-2222-222222222202"), "Işık ve gölgeyi dramatik bir tezatla kullanan (chiaroscuro), Barok resim okulunun kurucu dehası.", 1571, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1610, "Michelangelo Merisi da Caravaggio", "İtalyan", null },
                    { new Guid("22222222-2222-2222-2222-222222222203"), "Fransız Devrimi'nin ve Neoklasik resmin baş aktörü, antik kahramanlık ve erdem ideali ressamı.", 1748, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1825, "Jacques-Louis David", "Fransız", null },
                    { new Guid("22222222-2222-2222-2222-222222222204"), "Doğanın ezici ihtişamı ve insanın faniliği üzerine meditasyonlar üreten Alman Romantik manzara ustası.", 1774, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1840, "Caspar David Friedrich", "Alman", null },
                    { new Guid("22222222-2222-2222-2222-222222222205"), "Realizm akımının öncüsü ve teorisyeni; burjuva akademizmine karşı işçileri ve kırsal hayatı yücelten devrimci.", 1819, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1877, "Gustave Courbet", "Fransız", null },
                    { new Guid("22222222-2222-2222-2222-222222222206"), "Empresyonizm akımına adını veren 'İzlenim: Gün Doğumu' tablosunun yaratıcısı, ışık ve renk araştırmacısı.", 1840, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1926, "Claude Monet", "Fransız", null },
                    { new Guid("22222222-2222-2222-2222-222222222207"), "Duygusal yoğunluğu cesur fırça darbeleri ve çiğ renklerle dışavuran Hollandalı Post-Empresyonist deha.", 1853, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1890, "Vincent van Gogh", "Hollandalı", null },
                    { new Guid("22222222-2222-2222-2222-222222222208"), "Modern sanatın babası olarak kabul edilen, doğadaki geometrik düzeni arayan Post-Empresyonist usta.", 1839, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1906, "Paul Cézanne", "Fransız", null },
                    { new Guid("22222222-2222-2222-2222-222222222209"), "Kübizmin kurucusu, 20. yüzyıl görsel sanatlarını baştan tanımlayan İspanyol ressam ve heykeltıraş.", 1881, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1973, "Pablo Picasso", "İspanyol", null },
                    { new Guid("22222222-2222-2222-2222-222222222210"), "Paranoyak-eleştirel yöntemle rüya imgelerini ve bilinçaltı fantezilerini hiper-gerçekçi işleyen Sürrealist ikon.", 1904, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1989, "Salvador Dalí", "İspanyol", null }
                });

            migrationBuilder.InsertData(
                table: "art_movement_relations",
                columns: new[] { "Id", "CreatedAt", "Description", "RelationType", "SourceMovementId", "TargetMovementId" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333301"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rönesans'ın anatomi, kompozisyon ve perspektif birikimi Barok ustalarca devralınıp dramatize edilmiştir.", "Influenced", new Guid("11111111-1111-1111-1111-111111111101"), new Guid("11111111-1111-1111-1111-111111111103") },
                    { new Guid("33333333-3333-3333-3333-333333333302"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manieristler Yüksek Rönesans'ın mükemmel simetri ve durağan dengesini yapay ve sıkıcı bularak deformasyona yöneldi.", "ReactedAgainst", new Guid("11111111-1111-1111-1111-111111111102"), new Guid("11111111-1111-1111-1111-111111111101") },
                    { new Guid("33333333-3333-3333-3333-333333333303"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Geç Barok dönemin ağır ihtişamı Fransız saraylarında hafifleyerek pastel renkli Rokoko üslubuna evrilmiştir.", "EvolvedInto", new Guid("11111111-1111-1111-1111-111111111103"), new Guid("11111111-1111-1111-1111-111111111104") },
                    { new Guid("33333333-3333-3333-3333-333333333304"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Aydınlanma düşünürleri ve Neoklasikler Rokoko'yu yozlaşmış aristokratik zevk olarak görüp reddetmiştir.", "ReactedAgainst", new Guid("11111111-1111-1111-1111-111111111105"), new Guid("11111111-1111-1111-1111-111111111104") },
                    { new Guid("33333333-3333-3333-3333-333333333305"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Romantizm katı akademik kuralları ve Antikite taklitçiliğini bırakıp bireysel duygu ve doğa coşkusunu yüceltti.", "ReactedAgainst", new Guid("11111111-1111-1111-1111-111111111106"), new Guid("11111111-1111-1111-1111-111111111105") },
                    { new Guid("33333333-3333-3333-3333-333333333306"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Courbet öncülüğündeki Realistler Romantik mitolojiyi ve aşırı duygusallığı reddedip 'göremediğim meleği çizmem' demiştir.", "ReactedAgainst", new Guid("11111111-1111-1111-1111-111111111107"), new Guid("11111111-1111-1111-1111-111111111106") },
                    { new Guid("33333333-3333-3333-3333-333333333307"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Realizmin modern kentsel hayatı ve gündeliği resmetme ısrarı Empresyonistleri sokağa ve açık havaya taşımıştır.", "Influenced", new Guid("11111111-1111-1111-1111-111111111107"), new Guid("11111111-1111-1111-1111-111111111108") },
                    { new Guid("33333333-3333-3333-3333-333333333308"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Empresyonistler Salon de Paris'in Neoklasik akademik kurallarına, donuk stüdyo aydınlatmasına açıkça meydan okumuştur.", "ReactedAgainst", new Guid("11111111-1111-1111-1111-111111111108"), new Guid("11111111-1111-1111-1111-111111111105") },
                    { new Guid("33333333-3333-3333-3333-333333333309"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Post-Empresyonistler serbest fırça ve parlak renk teorisini doğrudan Empresyonist öncüllerinden öğrenerek evrildi.", "EvolvedInto", new Guid("11111111-1111-1111-1111-111111111108"), new Guid("11111111-1111-1111-1111-111111111109") },
                    { new Guid("33333333-3333-3333-3333-333333333310"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cézanne Empresyonizm'i 'müzelerdeki sanat gibi sağlam ve kalıcı kılmak' isteyerek saf anlık ışık oyununa tepki gösterdi.", "ReactedAgainst", new Guid("11111111-1111-1111-1111-111111111109"), new Guid("11111111-1111-1111-1111-111111111108") },
                    { new Guid("33333333-3333-3333-3333-333333333311"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Paul Cézanne'ın 'doğayı silindir, küre ve koni olarak ele alınız' prensibi Kübizm'in yapıtaşı oldu.", "Influenced", new Guid("11111111-1111-1111-1111-111111111109"), new Guid("11111111-1111-1111-1111-111111111110") },
                    { new Guid("33333333-3333-3333-3333-333333333312"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kübizmin nesneyi geleneksel uzamdan koparması Sürrealistlerin rüya ve bilinçaltı mekan kurgularına zemin hazırladı.", "Influenced", new Guid("11111111-1111-1111-1111-111111111110"), new Guid("11111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "artist_movements",
                columns: new[] { "ArtMovementId", "ArtistId", "EndYear", "IsPrimary", "Role", "StartYear" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111101"), new Guid("22222222-2222-2222-2222-222222222201"), 1519, true, "KeyFigure", 1470 },
                    { new Guid("11111111-1111-1111-1111-111111111103"), new Guid("22222222-2222-2222-2222-222222222202"), 1610, true, "Founder", 1592 },
                    { new Guid("11111111-1111-1111-1111-111111111105"), new Guid("22222222-2222-2222-2222-222222222203"), 1825, true, "KeyFigure", 1775 },
                    { new Guid("11111111-1111-1111-1111-111111111106"), new Guid("22222222-2222-2222-2222-222222222204"), 1840, true, "KeyFigure", 1798 },
                    { new Guid("11111111-1111-1111-1111-111111111107"), new Guid("22222222-2222-2222-2222-222222222205"), 1877, true, "Founder", 1848 },
                    { new Guid("11111111-1111-1111-1111-111111111108"), new Guid("22222222-2222-2222-2222-222222222206"), 1926, true, "Founder", 1865 },
                    { new Guid("11111111-1111-1111-1111-111111111109"), new Guid("22222222-2222-2222-2222-222222222207"), 1890, true, "KeyFigure", 1886 },
                    { new Guid("11111111-1111-1111-1111-111111111109"), new Guid("22222222-2222-2222-2222-222222222208"), 1906, true, "Pioneer", 1875 },
                    { new Guid("11111111-1111-1111-1111-111111111110"), new Guid("22222222-2222-2222-2222-222222222209"), 1919, true, "Founder", 1907 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222210"), 1940, true, "KeyFigure", 1929 }
                });

            migrationBuilder.InsertData(
                table: "artworks",
                columns: new[] { "Id", "ArtistId", "CreatedAt", "CreationYear", "CurrentLocation", "Medium", "PrimaryMovementId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444401"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1503, "Musée du Louvre, Paris", "Kavak pano üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111101"), "Mona Lisa", null },
                    { new Guid("44444444-4444-4444-4444-444444444402"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1600, "San Luigi dei Francesi, Roma", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111103"), "The Calling of Saint Matthew", null },
                    { new Guid("44444444-4444-4444-4444-444444444403"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1793, "Musées Royaux des Beaux-Arts, Brüksel", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111105"), "The Death of Marat", null },
                    { new Guid("44444444-4444-4444-4444-444444444404"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1818, "Hamburger Kunsthalle, Hamburg", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111106"), "Wanderer above the Sea of Fog", null },
                    { new Guid("44444444-4444-4444-4444-444444444405"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1849, "Gemäldegalerie Alte Meister, Dresden (Kayıp/Yok oldu)", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111107"), "The Stone Breakers", null },
                    { new Guid("44444444-4444-4444-4444-444444444406"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1872, "Musée Marmottan Monet, Paris", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111108"), "Impression, Sunrise", null },
                    { new Guid("44444444-4444-4444-4444-444444444407"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1889, "Museum of Modern Art (MoMA), New York", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111109"), "The Starry Night", null },
                    { new Guid("44444444-4444-4444-4444-444444444408"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1885, "Barnes Foundation, Philadelphia", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111109"), "Mont Sainte-Victoire seen from Bellevue", null },
                    { new Guid("44444444-4444-4444-4444-444444444409"), new Guid("22222222-2222-2222-2222-222222222209"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1907, "Museum of Modern Art (MoMA), New York", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111110"), "Les Demoiselles d'Avignon", null },
                    { new Guid("44444444-4444-4444-4444-444444444410"), new Guid("22222222-2222-2222-2222-222222222210"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1931, "Museum of Modern Art (MoMA), New York", "Tuval üzerine yağlıboya", new Guid("11111111-1111-1111-1111-111111111111"), "The Persistence of Memory", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_art_movement_relations_SourceMovementId_TargetMovementId_Re~",
                table: "art_movement_relations",
                columns: new[] { "SourceMovementId", "TargetMovementId", "RelationType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_art_movement_relations_TargetMovementId",
                table: "art_movement_relations",
                column: "TargetMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_art_movements_Name",
                table: "art_movements",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_artist_movements_ArtMovementId",
                table: "artist_movements",
                column: "ArtMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_artists_FullName",
                table: "artists",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_artworks_ArtistId",
                table: "artworks",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_artworks_PrimaryMovementId",
                table: "artworks",
                column: "PrimaryMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_artworks_Title",
                table: "artworks",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "art_movement_relations");

            migrationBuilder.DropTable(
                name: "artist_movements");

            migrationBuilder.DropTable(
                name: "artworks");

            migrationBuilder.DropTable(
                name: "art_movements");

            migrationBuilder.DropTable(
                name: "artists");
        }
    }
}
