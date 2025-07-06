using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RunalyzeMcp.IntegrationTests;

[TestFixture]
public class ToolDiscoveryTests : IntegrationTestBase
{
    [Test]
    public async Task ListTools_ShouldReturnAllTools()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/list"
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(responseContent, Is.Not.Empty);
        
        // Parse the response to verify it contains tools
        var jsonDoc = JsonDocument.Parse(responseContent);
        Assert.That(jsonDoc.RootElement.TryGetProperty("result", out var result), Is.True);
        Assert.That(result.TryGetProperty("tools", out var tools), Is.True);
        
        // Verify we have all 47 tools
        var toolsArray = tools.EnumerateArray().ToArray();
        Assert.That(toolsArray.Length, Is.EqualTo(47));
    }

    [Test]
    public async Task ListTools_ShouldReturnActivityTools()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/list"
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        var jsonDoc = JsonDocument.Parse(responseContent);
        var result = jsonDoc.RootElement.GetProperty("result");
        var tools = result.GetProperty("tools");
        
        var toolNames = tools.EnumerateArray()
            .Select(t => t.GetProperty("name").GetString())
            .ToArray();

        // Verify activity tools are present
        var expectedActivityTools = new[]
        {
            "api_activity_upload",
            "api_activity_download",
            "api_v1activitiesuploads_id_get",
            "api_v1activities_id_get",
            "api_v1activities_get"
        };

        foreach (var expectedTool in expectedActivityTools)
        {
            Assert.That(toolNames, Does.Contain(expectedTool));
        }
    }

    [Test]
    public async Task ListTools_ShouldReturnDownloadTools()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/list"
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        var jsonDoc = JsonDocument.Parse(responseContent);
        var result = jsonDoc.RootElement.GetProperty("result");
        var tools = result.GetProperty("tools");
        
        var toolNames = tools.EnumerateArray()
            .Select(t => t.GetProperty("name").GetString())
            .ToArray();

        // Verify download tools are present
        var expectedDownloadTools = new[]
        {
            "api_v1activity_id_fit_get",
            "api_v1activity_id_fitlog_get",
            "api_v1activity_id_gpx_get",
            "api_v1activity_id_kml_get",
            "api_v1activity_id_social_image_get",
            "api_v1activity_id_tcx_get"
        };

        foreach (var expectedTool in expectedDownloadTools)
        {
            Assert.That(toolNames, Does.Contain(expectedTool));
        }
    }

    [Test]
    public async Task ListTools_ShouldReturnMetricsTools()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/list"
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        var jsonDoc = JsonDocument.Parse(responseContent);
        var result = jsonDoc.RootElement.GetProperty("result");
        var tools = result.GetProperty("tools");
        
        var toolNames = tools.EnumerateArray()
            .Select(t => t.GetProperty("name").GetString())
            .ToArray();

        // Verify some metrics tools are present
        var expectedMetricsTools = new[]
        {
            "api_v1metrics_blood_glucose_get",
            "api_v1metrics_blood_pressure_get",
            "api_v1metrics_sleep_get",
            "api_v1metrics_hrv_get"
        };

        foreach (var expectedTool in expectedMetricsTools)
        {
            Assert.That(toolNames, Does.Contain(expectedTool));
        }
    }

    [Test]
    public async Task ListTools_ShouldReturnValidToolSchemas()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/list"
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        var jsonDoc = JsonDocument.Parse(responseContent);
        var result = jsonDoc.RootElement.GetProperty("result");
        var tools = result.GetProperty("tools");
        
        // Verify each tool has required properties
        foreach (var tool in tools.EnumerateArray())
        {
            Assert.That(tool.TryGetProperty("name", out _), Is.True);
            Assert.That(tool.TryGetProperty("description", out _), Is.True);
            Assert.That(tool.TryGetProperty("inputSchema", out _), Is.True);
            
            // Verify tool name is not empty
            var toolName = tool.GetProperty("name").GetString();
            Assert.That(toolName, Is.Not.Null.And.Not.Empty);
            
            // Verify description is not empty
            var description = tool.GetProperty("description").GetString();
            Assert.That(description, Is.Not.Null.And.Not.Empty);
        }
    }

    [Test]
    public async Task ListTools_ShouldReturnEquipmentAndTagTools()
    {
        // Arrange
        var mcpRequest = new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/list"
        };

        var json = JsonSerializer.Serialize(mcpRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/sse", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        var jsonDoc = JsonDocument.Parse(responseContent);
        var result = jsonDoc.RootElement.GetProperty("result");
        var tools = result.GetProperty("tools");
        
        var toolNames = tools.EnumerateArray()
            .Select(t => t.GetProperty("name").GetString())
            .ToArray();

        // Verify equipment and tag tools are present
        var expectedTools = new[]
        {
            "api_v1equipment_get",
            "api_v1equipment_id_get",
            "api_v1equipment_category_get",
            "api_v1equipment_category_id_get",
            "api_v1tags_get",
            "api_v1tags_id_get",
            "api_v1raceresults_get",
            "api_v1raceresults_activity_id_get",
            "api_v1statistics_get",
            "api_v1health_bulk_upload_post"
        };

        foreach (var expectedTool in expectedTools)
        {
            Assert.That(toolNames, Does.Contain(expectedTool));
        }
    }
}