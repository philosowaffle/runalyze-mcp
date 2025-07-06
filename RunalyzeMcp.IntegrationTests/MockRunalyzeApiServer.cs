using WireMock.Server;
using WireMock.Settings;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Matchers;
using System.Text.Json;

namespace RunalyzeMcp.IntegrationTests;

public class MockRunalyzeApiServer : IDisposable
{
    private readonly WireMockServer _server;
    private bool _disposed = false;

    public MockRunalyzeApiServer()
    {
        _server = WireMockServer.Start(new WireMockServerSettings
        {
            Port = 0, // Dynamic port
            StartAdminInterface = true
        });

        SetupEndpoints();
    }

    public string BaseUrl => _server.Url;

    public void Start()
    {
        // Server is already started in constructor
        // This method is for compatibility with test setup
    }

    public void Stop()
    {
        Dispose();
    }

    private void SetupEndpoints()
    {
        SetupActivityEndpoints();
        SetupDownloadEndpoints();
        SetupStatisticsEndpoints();
        SetupEquipmentEndpoints();
        SetupHealthEndpoints();
        SetupMetricsEndpoints();
        SetupRaceResultEndpoints();
        SetupTagEndpoints();
    }

    private void SetupActivityEndpoints()
    {
        // POST /api/v1/activities (Upload activity)
        _server.Given(Request.Create()
            .WithPath("/api/v1/activities")
            .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { id = 12345, status = "uploaded" })));

        // GET /api/v1/activities/{id} (Get activity)
        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/activities/\d+$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { id = 12345, name = "Test Activity", distance = 5000 })));

        // GET /api/v1/activities (Get activities list)
        _server.Given(Request.Create()
            .WithPath("/api/v1/activities")
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new[] { new { id = 12345, name = "Test Activity" } })));

        // GET /api/v1/activities/uploads/{id} (Get upload status)
        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/activities/uploads/\d+$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { id = 12345, status = "processed" })));
    }

    private void SetupDownloadEndpoints()
    {
        // Download endpoints (FIT, FITLOG, GPX, KML, Social Image, TCX)
        var downloadPaths = new[]
        {
            @"^/api/v1/activity/\d+/fit$",
            @"^/api/v1/activity/\d+/fitlog$",
            @"^/api/v1/activity/\d+/gpx$",
            @"^/api/v1/activity/\d+/kml$",
            @"^/api/v1/activity/\d+/social-image$",
            @"^/api/v1/activity/\d+/tcx$"
        };

        foreach (var path in downloadPaths)
        {
            _server.Given(Request.Create()
                .WithPath(new RegexMatcher(path))
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/octet-stream")
                    .WithBody("mock-file-content"));
        }
    }

    private void SetupStatisticsEndpoints()
    {
        // GET /api/v1/statistics (Get statistics)
        _server.Given(Request.Create()
            .WithPath("/api/v1/statistics")
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { totalActivities = 100, totalDistance = 1000000 })));
    }

    private void SetupEquipmentEndpoints()
    {
        // GET /api/v1/equipment (Get equipment)
        _server.Given(Request.Create()
            .WithPath("/api/v1/equipment")
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new[] { new { id = 1, name = "Test Equipment" } })));

        // GET /api/v1/equipment/{id} (Get equipment by ID)
        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/equipment/\d+$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { id = 1, name = "Test Equipment", type = "Shoes" })));

        // GET /api/v1/equipment/category (Get equipment categories)
        _server.Given(Request.Create()
            .WithPath("/api/v1/equipment/category")
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new[] { new { id = 1, name = "Shoes" } })));

        // GET /api/v1/equipment/category/{id} (Get equipment category by ID)
        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/equipment/category/\d+$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { id = 1, name = "Shoes", description = "Running shoes" })));
    }

    private void SetupHealthEndpoints()
    {
        // POST /api/v1/health/bulk-upload (Bulk upload health data)
        _server.Given(Request.Create()
            .WithPath("/api/v1/health/bulk-upload")
            .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { status = "uploaded", recordsProcessed = 10 })));
    }

    private void SetupMetricsEndpoints()
    {
        var metricTypes = new[]
        {
            "blood-glucose", "blood-pressure", "body-composition", "body-temperature",
            "daily-note", "hrv", "heart-rate-max", "heart-rate-rest", "mental", "sleep"
        };

        foreach (var metricType in metricTypes)
        {
            // GET /api/v1/metrics/{type} (Get metrics)
            _server.Given(Request.Create()
                .WithPath($"/api/v1/metrics/{metricType}")
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(JsonSerializer.Serialize(new[] { new { id = 1, value = 100, date = DateTime.UtcNow.ToString("yyyy-MM-dd") } })));

            // POST /api/v1/metrics/{type} (Create metric)
            _server.Given(Request.Create()
                .WithPath($"/api/v1/metrics/{metricType}")
                .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(JsonSerializer.Serialize(new { id = 1, status = "created" })));

            // GET /api/v1/metrics/{type}/{id} (Get metric by ID)
            _server.Given(Request.Create()
                .WithPath(new RegexMatcher($@"^/api/v1/metrics/{metricType}/\d+$"))
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(JsonSerializer.Serialize(new { id = 1, value = 100, date = DateTime.UtcNow.ToString("yyyy-MM-dd") })));
        }

        // Special endpoints for date-based metrics
        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/metrics/daily-note/\d{4}-\d{2}-\d{2}$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { date = DateTime.UtcNow.ToString("yyyy-MM-dd"), note = "Test note" })));

        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/metrics/mental/\d{4}-\d{2}-\d{2}$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { date = DateTime.UtcNow.ToString("yyyy-MM-dd"), value = 5 })));
    }

    private void SetupRaceResultEndpoints()
    {
        // GET /api/v1/raceresults (Get race results)
        _server.Given(Request.Create()
            .WithPath("/api/v1/raceresults")
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new[] { new { id = 1, name = "Test Race", time = "01:30:00" } })));

        // GET /api/v1/raceresults/activity/{id} (Get race result by activity)
        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/raceresults/activity/\d+$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { id = 1, activityId = 12345, name = "Test Race", time = "01:30:00" })));
    }

    private void SetupTagEndpoints()
    {
        // GET /api/v1/tags (Get tags)
        _server.Given(Request.Create()
            .WithPath("/api/v1/tags")
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new[] { new { id = 1, name = "Test Tag" } })));

        // GET /api/v1/tags/{id} (Get tag by ID)
        _server.Given(Request.Create()
            .WithPath(new RegexMatcher(@"^/api/v1/tags/\d+$"))
            .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(new { id = 1, name = "Test Tag", description = "Test description" })));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _server?.Stop();
            _server?.Dispose();
            _disposed = true;
        }
    }
}