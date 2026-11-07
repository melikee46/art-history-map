# Art History Map (Sanat Tarihi Zaman Çizelgesi & Etkileşim Haritası)

Sanat akımlarının (Rönesans, Barok, Romantizm, Empresyonizm, Kübizm vb.) yüzyıllar boyunca birbirini nasıl etkilediğini, hangi akımların öncüllerine felsefi ve estetik bir tepki/başkaldırı olarak doğduğunu ve hangi akımların eşzamanlı olarak paralel var olduğunu ortaya koyan etkileşimli bir zaman çizelgesi ve yönlü ağ haritası platformudur. Sanatçıların akımlar içindeki kurucu veya kilit rollerini ve üretilen eserleri tekil bir ağaç yerine zengin bir graf ontolojisiyle sunarak sanat tarihi araştırmacıları, küratörler ve meraklılar için karmaşık tarihsel bağıntıları görselleştirilebilir ve sorgulanabilir hale getirmeyi amaçlar.

---

## 1. Proje Durumu

| Katman / Modül | Durum | Açıklama |
| :--- | :---: | :--- |
| **Veritabanı Katmanı (Domain & Infrastructure)** | **TAMAMLANDI** | Domain modelleri, Fluent API konfigürasyonları, Migration, 11 akımlı seed data ve User-Secrets entegrasyonu hazır. |
| **Uygulama & Web API Katmanı** | *Planlanıyor* | RESTful/Minimal API endpoint'leri, Graph DTO'ları (`nodes`, `edges`), sayfalama ve filtreleme servisleri. |
| **Frontend Katmanı** | *Planlanıyor* | React tabanlı etkileşimli zaman çizelgesi ve D3.js / Cytoscape.js destekli yönlü ağ haritası görselleştirmesi. |
| **CI / CD & Dağıtım** | *Planlanıyor* | GitHub Actions derleme boru hatları ve konteyner orkestrasyonu. |

---

## 2. Teknoloji Yığını

* **Çalışma Zamanı & Dil:** .NET 10 (C# 13)
* **ORM:** Entity Framework Core 10 (`Microsoft.EntityFrameworkCore.Design`, `Microsoft.Extensions.Configuration.UserSecrets`)
* **Veritabanı:** PostgreSQL 16
* **Veritabanı Sağlayıcısı:** `Npgsql.EntityFrameworkCore.PostgreSQL` 10
* **Mimari Yaklaşım:** Sıfırdan temiz katmanlı mimari (**ABP Framework KULLANILMAMAKTADIR**).

---

## 3. Mimari ve Dizin Yapısı

Katmanlar arası bağımlılık kuralı gereğince `ArtHistoryMap.Domain` en içte yer alır ve hiçbir dış kütüphaneye veya veritabanı teknolojisine bağımlı değildir. `ArtHistoryMap.Infrastructure` ise Domain katmanını referans alarak EF Core eşlemelerini ve veritabanı işlemlerini yürütür.

```text
               +-----------------------------+
               |    ArtHistoryMap.Domain     |  <-- Saf C# İş Kuralları & Varlıklar
               +-----------------------------+
                              ^
                              | (bağımlılık yönü)
               +-----------------------------+
               | ArtHistoryMap.Infrastructure|  <-- EF Core, PostgreSQL, Migration, Seed
               +-----------------------------+
```

### Dizin Ağacı ve Dosya Sorumlulukları

```text
art-history-map/
├── ArtHistoryMap.slnx                                   # .NET 10 Solution dosyası
├── .gitignore                                           # .NET, IDE, OS ve güvenlik kısıtları dosyası
├── README.md                                            # Mimari ve kullanım belgelendirmesi
└── src/
    ├── ArtHistoryMap.Domain/                            # DOMAIN KATMANI
    │   ├── ArtHistoryMap.Domain.csproj                  # Domain proje konfigürasyonu
    │   ├── Common/
    │   │   └── BaseEntity.cs                            # Guid Id, CreatedAt ve UpdatedAt denetim taban sınıfı
    │   ├── Enums/
    │   │   ├── MovementRelationType.cs                  # Yönlü ilişki tipleri: Influenced, ReactedAgainst, EvolvedInto, ConcurrentWith
    │   │   └── ArtistMovementRole.cs                    # Sanatçı akım rolleri: Founder, KeyFigure, Pioneer, Follower, Associate
    │   └── Entities/
    │       ├── ArtMovement.cs                           # Sanat akımı ana varlığı (Outgoing/Incoming ilişki koleksiyonları)
    │       ├── ArtMovementRelation.cs                   # Yönlü graf kenarı (SourceMovement -> TargetMovement)
    │       ├── Artist.cs                                # Sanatçı varlığı (Biyografi, doğum/ölüm yılları, eserler)
    │       ├── ArtistMovement.cs                        # Açık ara tablo (Rol, aktif yıllar, IsPrimary aidiyeti)
    │       └── Artwork.cs                               # Sanat eseri varlığı (Sanatçı ve opsiyonel akım bağı)
    │
    └── ArtHistoryMap.Infrastructure/                    # INFRASTRUCTURE KATMANI
        ├── ArtHistoryMap.Infrastructure.csproj          # PostgreSQL, EF Core Design ve UserSecrets paketleri
        ├── Context/
        │   ├── ArtHistoryDbContext.cs                   # DbContext, DbSet tanımları, SaveChangesAsync interceptor
        │   └── ArtHistoryDbContextFactory.cs            # CLI araçları için User-Secrets destekli güvenli DbContext fabrikası
        ├── EntityConfigurations/                        # FLUENT API YAPILANDIRMALARI
        │   ├── ArtMovementConfiguration.cs              # Akım kuralları, kolon boyutları ve Name tekil indeksi
        │   ├── ArtMovementRelationConfiguration.cs      # Self-referencing FK, Cascade, Check constraint ve composite index
        │   ├── ArtistConfiguration.cs                   # Sanatçı kuralları ve FullName indeksi
        │   ├── ArtistMovementConfiguration.cs           # Composite Primary Key (ArtistId, ArtMovementId) konfigürasyonu
        │   └── ArtworkConfiguration.cs                  # Restrict (sanatçı silme engeli) ve SetNull (akım silme) yapılandırması
        ├── Seed/
        │   └── ArtHistoryDataSeeder.cs                  # 11 gerçek akım, 12 yönlü ilişki, 10 sanatçı ve 10 eser verisi
        └── Migrations/                                  # EF CORE MIGRATION ÇIKTILARI
            ├── 20261107133347_InitialArtHistorySchema.cs# Veritabanı şemasını oluşturan C# migration kodu
            ├── 20261107133347_InitialArtHistorySchema.Designer.cs
            └── ArtHistoryDbContextModelSnapshot.cs      # Veritabanı model anlık görüntüsü
```

---

## 4. Veri Modeli ve İlişki Şeması (ASCII Grafı)

```text
   +-------------------------------------------------------------+
   |                        ArtMovement                          |
   |-------------------------------------------------------------|
   | Id (PK, uuid)                                               |
   | Name (UK, varchar 150)                                      |
   | StartYear / EndYear (int, nullable)                         |
   +-------------------------------------------------------------+
         | 1 (Source)                             | 1 (Target)
         |                                        |
         |         +------------------------+     |
         +-------->|  ArtMovementRelation   |<----+
                   |------------------------|
                   | Id (PK, uuid)          |
                   | SourceMovementId (FK)  |
                   | TargetMovementId (FK)  |
                   | RelationType (varchar) |
                   | Description (varchar)  |
                   +------------------------+
                     UK: (Source, Target, RelationType)
                     CK: SourceMovementId <> TargetMovementId

   +-------------------+        +--------------------+        +-------------------+
   |      Artist       | 1    N |   ArtistMovement   | N    1 |   ArtMovement     |
   |-------------------|------->|--------------------|<-------|-------------------|
   | Id (PK, uuid)     |        | ArtistId (PK, FK)  |        | Id (PK, uuid)     |
   | FullName (varchar)|        | ArtMovementId(PK,FK|        +-------------------+
   +-------------------+        | Role (varchar)     |                  ^
         | 1                    | StartYear/EndYear  |                  | 1 (opsiyonel)
         |                      | IsPrimary (bool)   |                  |
         |                      +--------------------+                  |
         | N                                                            | N (SetNull)
         v                                                              |
   +--------------------------------------------------------------------+
   |                              Artwork                               |
   |--------------------------------------------------------------------|
   | Id (PK, uuid)                                                      |
   | Title (varchar 250)                                                |
   | ArtistId (FK, Restrict)                                            |
   | PrimaryMovementId (FK, Nullable)                                   |
   +--------------------------------------------------------------------+
```

---

## 5. Tasarım Kararları ve Gerekçeleri

### 1. Yönlü İlişki (`SourceMovement` -> `TargetMovement`) ve Kısıtlar
* **Neden bunu seçtim?** Sanat tarihindeki etkileşim simetrik bir ilişki değildir; yönlü bir çizgedir (directed graph). *"Realizm, Empresyonizm'i etkiledi"* önermesi ile tersi aynı anlama gelmez. `SourceMovementId` eylemi başlatan/öncül olanı, `TargetMovementId` ise etkilenen veya tepki gösteren akımı temsil eder.
* **Birden Fazla İlişki Tipi:** İki akım arasında birden çok ilişki kurulabilir. Örneğin Post-Empresyonizm, Empresyonizm'den teknik olarak evrilmiş (`EvolvedInto`), ancak aynı zamanda onun anlık ışık takıntısına tepki göstererek (`ReactedAgainst`) yapısal forma dönmüştür. Bu nedenle tekillik `(SourceMovementId, TargetMovementId, RelationType)` composite indeksi ile sağlanmıştır.
* **Self-Loop Engeli:** Bir akımın mantıksal olarak kendini etkilemesi veritabanı seviyesinde `CHECK ("SourceMovementId" <> "TargetMovementId")` kısıtıyla engellenmiştir.

### 2. `RelationType`: Neden Enum + String Eşlemesi?
* **Neden bunu seçtim?** C# tarafında derleme zamanı tip güvenliği ve intellisense sağlarken, EF Core `HasConversion<string>()` sayesinde veritabanında `varchar(50)` olarak saklanır.
* **Neyi neden eledim?**
  * *Lookup Tablosu Elendi:* İlişki türleri (Etkileme, Tepki, Evrilme, Çağdaşlık) domain kuralları gereği sabittir. Dinamik ekleme gerekmeyeceği için her sorguda ek bir JOIN maliyeti getirecek lookup tablosu elenmiştir.
  * *Serbest Metin (Free Text) Elendi:* Yazım hatalarına (typo) açık olması ve tip güvenliği sağlamaması nedeniyle elenmiştir.

### 3. `ArtistMovement`: Neden Açık (Explicit) Join Varlığı?
* **Neden bunu seçtim?** Sanatçının bir akımla ilişkisi yalnızca ikili bir varlık/yokluk ilişkisi değildir. Picasso Kübizm'in kurucusudur (`Founder`), Monet Empresyonizm'in kilit figürüdür (`KeyFigure`). `Role`, `StartYear`, `EndYear` ve sanatçının ana kimliğini belirten `IsPrimary` alanlarını saklamak için zengin bir ara tablo zorunludur.
* **Neyi neden eledim?** EF Core'un örtük (shadow) `many-to-many` eşlemesi ara tabloya ek sütun eklemeye izin vermediği için elenmiştir.

### 4. Cascade Delete Stratejisi
* **`ArtMovement` -> `ArtMovementRelation` (Cascade):** Akım silindiğinde ona bağlı graf çizgilerinin anlamsız kalması nedeniyle kenarlar otomatik temizlenir.
* **`Artist` -> `Artwork` (Restrict):** Bir sanatçı kaydı kazaen silindiğinde, ona bağlı eserlerin veritabanından sessizce yok olması (`accidental cascading data loss`) kabul edilemez bir risktir. `DeleteBehavior.Restrict` ile foreign key seviyesinde silme engellenir; önce eserlerin bilinçli olarak yönetilmesi mecbur kılınır.
* **`ArtMovement` -> `Artwork` (SetNull):** Akım silinirse eserin akım etiketi `NULL` yapılır; sanatçı ve eser kaydı asla silinmez.

### 5. Anahtar Tipi: Neden Guid (UUID)?
* **Dürüst Gerekçe:** Guid seçimi "güvenlik" veya şifreleme amacıyla değil; **dış sistemlerle entegrasyon kolaylığı ve tahmin edilemezlik** amacıyla yapılmıştır. İleride veriler graf veritabanlarına (Neo4j vb.) aktarıldığında veya istemcilere dağıtıldığında sıralı integer ID'lerin (`1, 2, 3`) yaratacağı ID enumeration (sistemdeki toplam kayıt adedini tahmin etme) riskini önlemek ve distributed ortamda çakışmasız ID üretebilmek için seçilmiştir.

---

## 6. Güvenlik ve Veri Bütünlüğü

* **Sıfır Hardcoded Sır Politikası:** Proje kaynak kodunda hiçbir parola veya bağlantı dizesi gömülü değildir. Geliştirme ortamında bağlantı dizesi tamamen `.NET Secret Manager` (`dotnet user-secrets`) üzerinden yönetilir.
* **SQL Injection Koruması:** Ham SQL birleştirmeleri (`string concatenation`) kesinlikle kullanılmamıştır. Tüm sorgu ve eklemeler EF Core'un parametrizeli sorgu üreticisi üzerinden icra edilir.
* **Veritabanı Seviyesinde Bütünlük:** Kod seviyesindeki validasyonların bypass edilme ihtimaline karşı `CHECK` kısıtı, `Composite Unique Index` ve `Foreign Key Constraints` doğrudan PostgreSQL şemasına gömülmüştür.

---

## 7. Kurulum ve Çalıştırma Adımları (macOS)

Aşağıdaki adımları terminalde sırasıyla çalıştırarak ortamı ayağa kaldırabilirsiniz.

### Gereksinimler
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* `dotnet-ef` aracı (`dotnet tool install --global dotnet-ef`)

### Adım 1: PostgreSQL Konteynerini Başlatma
Veritabanı adını `arthistory` olarak belirleyip portu yalnızca yerel makineye (`127.0.0.1`) bağlayarak başlatın:

```bash
docker run -d \
  --name arthistory-postgres \
  -e POSTGRES_DB=arthistory \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=<SIFRENIZ> \
  -p 127.0.0.1:5432:5432 \
  --restart unless-stopped \
  postgres:16-alpine
```

### Adım 2: User-Secrets Üzerinden Connection String Tanımlama
Geliştirme makinenizde parolanızı güvenli depoya kaydedin (kod tabanına yazılmaz):

```bash
cd /Users/melikearslan/Desktop/art-history-map

# User-secrets deposunu ilklendirin (gerekirse)
dotnet user-secrets init --project src/ArtHistoryMap.Infrastructure

# Bağlantı dizesini kaydedin
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=arthistory;Username=postgres;Password=<SIFRENIZ>" --project src/ArtHistoryMap.Infrastructure
```

### Adım 3: Veritabanını Güncelleme (Migration & Seed)
Migration'ı veritabanına uygulayın:

```bash
dotnet ef database update \
  --project src/ArtHistoryMap.Infrastructure \
  --startup-project src/ArtHistoryMap.Infrastructure
```

> **Seed Verisi Nasıl Yüklenir?**  
> Kod yapısı incelendiğinde ([`ArtHistoryDbContext.cs`](file:///Users/melikearslan/Desktop/art-history-map/src/ArtHistoryMap.Infrastructure/Context/ArtHistoryDbContext.cs)), seed verilerinin `OnModelCreating` aşamasında `ArtHistoryDataSeeder.SeedData(modelBuilder)` metodu aracılığıyla migration snapshot'ına entegre edildiği görülür. Yani ayrı bir runtime seeder çalıştırmanıza gerek yoktur; `dotnet ef database update` komutunu çalıştırdığınız anda 11 sanat akımı, 12 yönlü ilişki, 10 sanatçı ve 10 sanat eseri otomatik olarak veritabanına basılır.

### Adım 4: Veritabanını Doğrulama
Docker konteyneri içindeki `psql` aracıyla yüklenen verileri kontrol edin:

```bash
# Akım sayısını ve ilk birkaç kaydı doğrulayın
docker exec -it arthistory-postgres psql -U postgres -d arthistory -c "SELECT \"Name\", \"StartYear\", \"EndYear\" FROM art_movements ORDER BY \"StartYear\" LIMIT 5;"

# Yönlü graf ilişkilerini doğrulayın
docker exec -it arthistory-postgres psql -U postgres -d arthistory -c "
SELECT 
    sm.\"Name\" AS \"Kaynak_Akim\", 
    r.\"RelationType\" AS \"Iliski_Turu\", 
    tm.\"Name\" AS \"Hedef_Akim\"
FROM art_movement_relations r
JOIN art_movements sm ON r.\"SourceMovementId\" = sm.\"Id\"
JOIN art_movements tm ON r.\"TargetMovementId\" = tm.\"Id\"
LIMIT 5;
"
```

---

## 8. Sık Karşılaşılan Hatalar ve Çözümleri

1. **`System.InvalidOperationException: Veritabanı bağlantı dizesi (Connection String) bulunamadı!`**  
   * **Neden:** `dotnet user-secrets` tanımlanmamış veya ortam değişkeni verilmemiştir.  
   * **Çözüm:** Adım 2'deki `dotnet user-secrets set` komutunu doğru parola ile çalıştırın.
2. **`Npgsql.NpgsqlException: Connection refused (127.0.0.1:5432)`**  
   * **Neden:** Docker servisi kapalıdır veya konteyner ayakta değildir.  
   * **Çözüm:** `docker ps` ile konteyner durumunu kontrol edin; durmuşsa `docker start arthistory-postgres` komutunu çalıştırın.
3. **`Bind for 0.0.0.0:5432 failed: port is already allocated`**  
   * **Neden:** Makinenizde yerel olarak çalışan başka bir PostgreSQL örneği (Postgres.app veya Homebrew postgresql servisi) 5432 portunu işgal etmektedir.  
   * **Çözüm:** `brew services stop postgresql` ile yerel servisi durdurun veya Docker port eşlemesini `-p 127.0.0.1:5433:5432` yapıp connection string portunu `5433` olarak güncelleyin.
4. **`28P01: password authentication failed for user "postgres"`**  
   * **Neden:** Docker başlatırken verilen `POSTGRES_PASSWORD` ile User-Secrets'a girilen parola eşleşmemektedir.  
   * **Çözüm:** Parolayı her iki tarafta da aynı yaparak user-secrets kaydını yenileyin.

---

## 9. Gelecek Yol Haritası (Roadmap)

* [ ] **API Katmanı (`ArtHistoryMap.Api`):**
  * Akımlar, Sanatçılar ve Eserler için RESTful CRUD uç noktaları.
  * Cytoscape.js ve D3.js graf motorları için optimize edilmiş `GET /api/movements/graph` (`nodes` & `edges`) endpoint'i.
  * Keyset ve Offset tabanlı sayfalama (pagination) ve kronolojik sıralama.
  * Rate limiting, CORS ilkeleri ve Swagger/OpenAPI dokümantasyonu.
* [ ] **Frontend Katmanı:**
  * React + TypeScript + Tailwind CSS arayüzü.
  * Yıllara göre sürüklenebilir interaktif zaman çizelgesi (Timeline slider).
  * Akımlar arası etkileşim ağını (etkilenme/tepki çizgilerini) gösteren yönlü vektör haritası.
* [ ] **DevOps & Dağıtım:**
  * Çok aşamalı (multi-stage) Dockerfile üretimi.
  * GitHub Actions üzerinden CI derleme ve test otomasyonu.

---

## 10. Lisans

*(Lisans türü henüz kararlaştırılmamıştır — geliştiriciye danışılacaktır: MIT / Apache 2.0 / Proprietary).*
