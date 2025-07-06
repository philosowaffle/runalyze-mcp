using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RunalyzeMcp.IntegrationTests;

[TestFixture]
public class ToolExecutionTests : IntegrationTestBase
{
    [Test]
    public async Task ExecuteTool_GetActivities_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1activities_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token"
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_GetActivityById_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1activities_id_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token",
                    ["id"] = 12345
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_GetStatistics_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1statistics_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token"
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_GetEquipment_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1equipment_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token"
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_GetBloodGlucoseMetrics_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1metrics_blood_glucose_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token"
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_CreateSleepMetric_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1metrics_sleep_post",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token",
                    ["metricData"] = new Dictionary<string, object>
                    {
                        ["value"] = 8.5,
                        ["date"] = "2023-01-01"
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_DownloadFitFile_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1activity_id_fit_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token",
                    ["id"] = 12345
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_GetTags_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1tags_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token"
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_GetRaceResults_ShouldReturnSuccess()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1raceresults_get",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token"
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out _), Is.False);
    }

    [Test]
    public async Task ExecuteTool_WithInvalidToolName_ShouldReturnError()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "invalid_tool_name",
                arguments = new Dictionary<string, object>
                {
                    ["token"] = "test-token"
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out var error), Is.True);
    }

    [Test]
    public async Task ExecuteTool_WithMissingRequiredParameter_ShouldReturnError()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/call",
            @params = new
            {
                name = "api_v1activities_get",
                arguments = new Dictionary<string, object>
                {
                    // Missing required token parameter
                }
            }
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var jsonDoc = JsonDocument.Parse(responseContent);
        // The response should contain an error for missing required parameters
        Assert.That(jsonDoc.RootElement.TryGetProperty("error", out var error), Is.True);
    }
}