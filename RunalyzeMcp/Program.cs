using ModelContextProtocol.AspNetCore;
using ModelContextProtocol.Protocol;
using ModelContextProtocol;
using RunalyzeMcp;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure the server to listen on port 8080
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Register RunalyzeApiClient as singleton
builder.Services.Configure<RunalyzeApiClientOptions>(options => { });
builder.Services.AddHttpClient<RunalyzeApiClient>();

// Configure MCP server
builder.Services
    .AddMcpServer(options =>
    {
        options.ServerInfo = new Implementation { Name = "RunalyzeMCP", Version = "1.0.0" };
        options.Capabilities = new ServerCapabilities
        {
            Tools = new ToolsCapability
            {
                ListToolsHandler = (request, cancellationToken) =>
                {
                    return ValueTask.FromResult(new ListToolsResult { Tools = ToolDefinitions.AllTools });
                },

                CallToolHandler = async (request, cancellationToken) =>
                {
                    if (request.Services == null)
                        throw new McpException("Services not available");
                    
                    var apiClient = request.Services.GetRequiredService<RunalyzeApiClient>();
                    var toolName = request.Params?.Name;
                    var arguments = request.Params?.Arguments ?? new Dictionary<string, JsonElement>();

                    return await McpToolHandler.HandleToolCallAsync(apiClient, toolName, arguments, cancellationToken);
                }
            }
        };
    })
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

app.MapGet("/health", () => "OK");
app.MapMcp();

app.Run();

// Make Program class accessible for integration tests
public partial class Program { }
