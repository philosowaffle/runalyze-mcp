using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace RunalyzeMcp.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    private bool _disposed = false;
    protected readonly WebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly MockRunalyzeApiServer MockServer;

    protected IntegrationTestBase()
    {
        MockServer = new MockRunalyzeApiServer();
        
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    // Set the mock server URL as the base URL for RunalyzeApiClient
                    config.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["RUNALYZE_BASE_URL"] = MockServer.BaseUrl
                    });
                });

                builder.ConfigureServices(services =>
                {
                    // Additional test-specific service configuration can go here
                });

                builder.UseEnvironment("Testing");
            });

        Client = Factory.CreateClient();
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
            Client?.Dispose();
            Factory?.Dispose();
            MockServer?.Dispose();
            _disposed = true;
        }
    }
}