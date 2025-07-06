using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace RunalyzeMcp.IntegrationTests
{
    [TestFixture]
    public class McpIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        private MockRunalyzeApiServer _mockServer;

        [SetUp]
        public void SetUp()
        {
            _mockServer = new MockRunalyzeApiServer();
            _mockServer.Start();

            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // Override the base URL to point to our mock server
                        services.Configure<RunalyzeApiClientOptions>(options =>
                        {
                            options.BaseUrl = _mockServer.BaseUrl;
                        });
                        
                        // Set test environment variables
                        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
                    });
                });
            
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
            _factory?.Dispose();
            _mockServer?.Stop();
            _mockServer?.Dispose();
        }

        [Test]
        public async Task HealthEndpoint_Should_Return_OK()
        {
            // Act
            var response = await _client.GetAsync("/health");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Is.EqualTo("OK"));
        }

        [Test]
        public async Task McpToolsList_Should_Return_All_Tools()
        {
            // Arrange
            var jsonRpcRequest = new
            {
                jsonrpc = "2.0",
                id = 1,
                method = "tools/list"
            };

            var jsonContent = JsonSerializer.Serialize(jsonRpcRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/", content);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Parse the Server-Sent Events response
            var lines = responseContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var dataLines = lines.Where(line => line.StartsWith("data: ")).ToArray();
            
            Assert.That(dataLines.Length, Is.GreaterThan(0), "Should have data lines in SSE response");
            
            // Parse the JSON from the data line
            var jsonData = dataLines[0].Substring(6); // Remove "data: " prefix
            var mcpResponse = JsonSerializer.Deserialize<JsonElement>(jsonData);
            
            // Verify it's a successful response (not an error)
            Assert.That(mcpResponse.TryGetProperty("error", out _), Is.False, 
                $"Response should not contain error. Full response: {jsonData}");
            
            // Verify it has the expected structure
            Assert.That(mcpResponse.TryGetProperty("result", out var result), Is.True);
            Assert.That(result.TryGetProperty("tools", out var tools), Is.True);
            
            // Verify we have the expected number of tools
            var toolsArray = tools.EnumerateArray().ToArray();
            Assert.That(toolsArray.Length, Is.EqualTo(49), "Should have 49 tools total");
        }

        [Test]
        public async Task McpToolCall_Should_Execute_Tool_Successfully()
        {
            // Arrange
            var jsonRpcRequest = new
            {
                jsonrpc = "2.0",
                id = 1,
                method = "tools/call",
                @params = new
                {
                    name = "api_v1statistics_get",
                    arguments = new Dictionary<string, object>
                    {
                        { "token", "test-token" }
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(jsonRpcRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/", content);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Parse the Server-Sent Events response
            var lines = responseContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var dataLines = lines.Where(line => line.StartsWith("data: ")).ToArray();
            
            Assert.That(dataLines.Length, Is.GreaterThan(0), "Should have data lines in SSE response");
            
            // Parse the JSON from the data line
            var jsonData = dataLines[0].Substring(6); // Remove "data: " prefix
            var mcpResponse = JsonSerializer.Deserialize<JsonElement>(jsonData);
            
            // Verify it's a successful response (not an error)
            Assert.That(mcpResponse.TryGetProperty("error", out _), Is.False, 
                $"Response should not contain error. Full response: {jsonData}");
            
            // Verify it has the expected structure
            Assert.That(mcpResponse.TryGetProperty("result", out var result), Is.True);
            Assert.That(result.TryGetProperty("content", out var content_result), Is.True);
        }

        [Test]
        public async Task McpToolCall_With_Invalid_Tool_Should_Return_Error()
        {
            // Arrange
            var jsonRpcRequest = new
            {
                jsonrpc = "2.0",
                id = 1,
                method = "tools/call",
                @params = new
                {
                    name = "invalid_tool_name",
                    arguments = new Dictionary<string, object>
                    {
                        { "token", "test-token" }
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(jsonRpcRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/", content);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Parse the Server-Sent Events response
            var lines = responseContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var dataLines = lines.Where(line => line.StartsWith("data: ")).ToArray();
            
            Assert.That(dataLines.Length, Is.GreaterThan(0), "Should have data lines in SSE response");
            
            // Parse the JSON from the data line
            var jsonData = dataLines[0].Substring(6); // Remove "data: " prefix
            var mcpResponse = JsonSerializer.Deserialize<JsonElement>(jsonData);
            
            // Verify it's an error response
            Assert.That(mcpResponse.TryGetProperty("error", out var error), Is.True);
            Assert.That(error.TryGetProperty("message", out var message), Is.True);
            Assert.That(message.GetString(), Does.Contain("tool"));
        }

        [Test]
        public async Task McpToolCall_With_Missing_Token_Should_Return_Error()
        {
            // Arrange
            var jsonRpcRequest = new
            {
                jsonrpc = "2.0",
                id = 1,
                method = "tools/call",
                @params = new
                {
                    name = "api_v1statistics_get",
                    arguments = new Dictionary<string, object>()
                }
            };

            var jsonContent = JsonSerializer.Serialize(jsonRpcRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/", content);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Parse the Server-Sent Events response
            var lines = responseContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var dataLines = lines.Where(line => line.StartsWith("data: ")).ToArray();
            
            Assert.That(dataLines.Length, Is.GreaterThan(0), "Should have data lines in SSE response");
            
            // Parse the JSON from the data line
            var jsonData = dataLines[0].Substring(6); // Remove "data: " prefix
            var mcpResponse = JsonSerializer.Deserialize<JsonElement>(jsonData);
            
            // Verify it's an error response
            Assert.That(mcpResponse.TryGetProperty("error", out var error), Is.True);
            Assert.That(error.TryGetProperty("message", out var message), Is.True);
            Assert.That(message.GetString(), Does.Contain("token"));
        }
    }
}