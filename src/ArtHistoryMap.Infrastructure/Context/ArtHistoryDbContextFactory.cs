using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ArtHistoryMap.Infrastructure.Context;

/// <summary>
/// EF Core CLI araçlarının tasarım zamanında DbContext üretebilmesi için gereken fabrika sınıfı.
/// 
/// GÜVENLİK ZORUNLULUĞU:
/// Connection string kaynak kodda hardcode EDİLMEZ.
/// Yalnızca 'dotnet user-secrets' veya 'ConnectionStrings__DefaultConnection' ortam değişkeninden okunur.
/// Değer bulunamazsa kesinlikle varsayılan bir parola fallback'i kullanılmaz, exception fırlatılır.
/// </summary>
public class ArtHistoryDbContextFactory : IDesignTimeDbContextFactory<ArtHistoryDbContext>
{
    public ArtHistoryDbContext CreateDbContext(string[] args)
    {
        // User Secrets'ı bağlayan ConfigurationBuilder
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<ArtHistoryDbContext>(optional: true)
            .Build();

        // 1. Önce UserSecrets içinde "ConnectionStrings:DefaultConnection" aranır
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // 2. Eğer UserSecrets'ta yoksa Ortam Değişkeni (Environment Variable) kontrol edilir
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
        }

        // 3. Hiçbir konfigürasyon bulunamazsa güvenli şekilde hata fırlatılır
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Veritabanı bağlantı dizesi (Connection String) bulunamadı!\n" +
                "Güvenlik gereği kaynak kodda varsayılan parola tutulmamaktadır.\n" +
                "Lütfen bağlantı dizesini şu komutla User-Secrets deposuna ekleyin:\n" +
                "dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"Host=localhost;Port=5432;Database=arthistory;Username=postgres;Password=<SIFRENIZ>\" --project src/ArtHistoryMap.Infrastructure");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ArtHistoryDbContext>();
        optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly(typeof(ArtHistoryDbContext).Assembly.FullName);
        });

        return new ArtHistoryDbContext(optionsBuilder.Options);
    }
}
