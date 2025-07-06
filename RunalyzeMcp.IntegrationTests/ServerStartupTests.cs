using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RunalyzeMcp.IntegrationTests;

[TestFixture]
public class ServerStartupTests : IntegrationTestBase
{
    [Test]
    public async Task HealthEndpoint_ShouldReturnOk()
    {
        // Act
        var response = await Client.GetAsync("/health");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(content, Is.EqualTo("OK"));
    }

    [Test]
    public async Task Server_ShouldStartWithoutErrors()
    {
        // Act - The server should have started during test setup
        var response = await Client.GetAsync("/health");

        // Assert - If we get here without exceptions, the server started successfully
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task SseEndpoint_ShouldBeAccessible()
    {
        // Act
        var response = await Client.GetAsync("/sse");

        // Assert
        // The SSE MCP endpoint should be accessible (not 404)
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Server_ShouldRespondToInvalidRequests()
    {
        // Act
        var response = await Client.GetAsync("/nonexistent");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Server_ShouldHaveCorrectContentType()
    {
        // Act
        var response = await Client.GetAsync("/health");

        // Assert
        Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("text/plain"));
    }
}