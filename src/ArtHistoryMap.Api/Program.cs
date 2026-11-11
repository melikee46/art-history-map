using System.Threading.RateLimiting;
using ArtHistoryMap.Api.Middleware;
using ArtHistoryMap.Api.Services;
using ArtHistoryMap.Infrastructure.Context;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Connection String & PostgreSQL DbContext Entegrasyonu
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Veritabanı bağlantı dizesi (ConnectionStrings:DefaultConnection) bulunamadı!\n" +
        "Güvenlik gereği kaynak kodda parola tutulmamaktadır.\n" +
        "Lütfen bağlantıyı tanımlayınız: dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"...\" --project src/ArtHistoryMap.Api");
}

builder.Services.AddDbContext<ArtHistoryDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.MigrationsAssembly(typeof(ArtHistoryDbContext).Assembly.FullName);
    });
});

// 2. Domain & Application Servisleri (DI)
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IArtworkService, ArtworkService>();

// 3. Exception Handling (RFC 7807 ProblemDetails)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// 4. Rate Limiting (Dakikada 120 İstek Sabit Pencere)
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddFixedWindowLimiter("global_fixed", opt =>
    {
        opt.PermitLimit = 120;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueLimit = 0;
    });
});

// 5. CORS Yapılandırması (React, Vite, Next.js vb. istemciler için)
var allowedOrigins = builder.Configuration.GetSection("Frontend:Origins").Get<string[]>() 
    ?? new[] { "http://localhost:3000", "http://localhost:5173" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .WithMethods("GET")
            .AllowAnyHeader();
    });
});

// 6. Controllers & OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// HTTP Pipeline
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("FrontendPolicy");
app.UseRateLimiter();

app.MapControllers().RequireRateLimiting("global_fixed");

app.Run();
