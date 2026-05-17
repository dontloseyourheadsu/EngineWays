using System.Formats.Asn1;
using System.Globalization;
using System.IO.Compression;
using CsvHelper;
using CsvHelper.Configuration;
using EngineWays.Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EngineWays.IngestionWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private const string GtfsUrl = "https://transit.land/api/v1/feeds/f-9g-mexicocity~metro/download_latest_feed_version";

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Ensure database is created
        await dbContext.Database.EnsureCreatedAsync(stoppingToken);

        if (await dbContext.Stops.AnyAsync(stoppingToken))
        {
            _logger.LogInformation("GTFS data already exists in database. Skipping ingestion.");
            return;
        }

        _logger.LogInformation("GTFS data not found. Starting download from {Url}", GtfsUrl);

        try
        {
            using var httpClient = new HttpClient();
            var zipData = await httpClient.GetByteArrayAsync(GtfsUrl, stoppingToken);
            
            var tempPath = Path.Combine(Path.GetTempPath(), "EngineWays_GTFS");
            if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);

            var zipPath = Path.Combine(tempPath, "gtfs.zip");
            await File.WriteAllBytesAsync(zipPath, zipData, stoppingToken);
            
            _logger.LogInformation("Extracting GTFS data...");
            ZipFile.ExtractToDirectory(zipPath, tempPath);

            await IngestStops(dbContext, tempPath, stoppingToken);
            await IngestRoutes(dbContext, tempPath, stoppingToken);
            await IngestTrips(dbContext, tempPath, stoppingToken);

            _logger.LogInformation("GTFS ingestion completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during GTFS ingestion.");
        }
    }

    private async Task IngestStops(AppDbContext db, string path, CancellationToken ct)
    {
        var filePath = Path.Combine(path, "stops.txt");
        if (!File.Exists(filePath)) return;

        _logger.LogInformation("Ingesting stops...");
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, MissingFieldFound = null });
        
        var stops = csv.GetRecords<GtfsStop>().ToList();
        await db.Stops.AddRangeAsync(stops, ct);
        await db.SaveChangesAsync(ct);
        _logger.LogInformation("Ingested {Count} stops.", stops.Count);
    }

    private async Task IngestRoutes(AppDbContext db, string path, CancellationToken ct)
    {
        var filePath = Path.Combine(path, "routes.txt");
        if (!File.Exists(filePath)) return;

        _logger.LogInformation("Ingesting routes...");
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, MissingFieldFound = null });
        
        var routes = csv.GetRecords<GtfsRoute>().ToList();
        await db.Routes.AddRangeAsync(routes, ct);
        await db.SaveChangesAsync(ct);
        _logger.LogInformation("Ingested {Count} routes.", routes.Count);
    }

    private async Task IngestTrips(AppDbContext db, string path, CancellationToken ct)
    {
        var filePath = Path.Combine(path, "trips.txt");
        if (!File.Exists(filePath)) return;

        _logger.LogInformation("Ingesting trips...");
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, MissingFieldFound = null });
        
        var trips = csv.GetRecords<GtfsTrip>().ToList();
        await db.Trips.AddRangeAsync(trips, ct);
        await db.SaveChangesAsync(ct);
        _logger.LogInformation("Ingested {Count} trips.", trips.Count);
    }
}
