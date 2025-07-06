using ModelContextProtocol.AspNetCore;
using ModelContextProtocol.Protocol;
using ModelContextProtocol;
using RunalyzeMcp;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure the server to listen on port 8080
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Register RunalyzeApiClient as singleton
builder.Services.AddHttpClient<RunalyzeApiClient>();

// Configure MCP server
builder.Services.AddMcpServer(options =>
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
                var apiClient = request.Services.GetRequiredService<RunalyzeApiClient>();
                var toolName = request.Params?.Name;
                var arguments = request.Params?.Arguments ?? new Dictionary<string, JsonElement>();

                if (!arguments.TryGetValue("token", out var tokenObj) || tokenObj.ValueKind != JsonValueKind.String)
                {
                    throw new McpException("Missing required 'token' parameter");
                }
                var token = tokenObj.GetString()!;

                try
                {
                    HttpResponseMessage response;
                    switch (toolName)
                    {
                        case "api_activity_upload":
                            if (!arguments.TryGetValue("file", out var fileObj) || fileObj.ValueKind != JsonValueKind.String ||
                                !arguments.TryGetValue("filename", out var filenameObj) || filenameObj.ValueKind != JsonValueKind.String)
                            {
                                throw new McpException("Missing required 'file' or 'filename' parameter");
                            }
                            response = await apiClient.UploadActivityAsync(token, fileObj.GetString()!, filenameObj.GetString());
                            break;

                        case "api_activity_download":
                            if (!arguments.TryGetValue("id", out var downloadIdObj) || downloadIdObj.ValueKind != JsonValueKind.Number)
                            {
                                throw new McpException("Missing required 'id' parameter");
                            }
                            response = await apiClient.DownloadFitOriginalAsync(token, downloadIdObj.GetInt32().ToString());
                            break;

                        case "api_v1activitiesuploads_id_get":
                            if (!arguments.TryGetValue("id", out var uploadIdObj) || uploadIdObj.ValueKind != JsonValueKind.Number)
                            {
                                throw new McpException("Missing required 'id' parameter");
                            }
                            response = await apiClient.GetActivityUploadStatusAsync(token, uploadIdObj.GetInt32().ToString());
                            break;

                        case "api_v1activities_id_get":
                            if (!arguments.TryGetValue("id", out var activityIdObj) || activityIdObj.ValueKind != JsonValueKind.Number)
                            {
                                throw new McpException("Missing required 'id' parameter");
                            }
                            response = await apiClient.GetActivityAsync(token, activityIdObj.GetInt32().ToString());
                            break;

                        case "api_v1activities_get":
                            int? page = null;
                            if (arguments.TryGetValue("limit", out var pageObj) && pageObj.ValueKind == JsonValueKind.Number)
                                page = pageObj.GetInt32();
                            response = await apiClient.GetActivitiesAsync(token, page);
                            break;

                        case "api_v1statistics_get":
                            response = await apiClient.GetCurrentStatisticsAsync(token);
                            break;

                        case "api_v1equipment_get":
                            response = await apiClient.GetEquipmentAsync(token);
                            break;

                        case "api_v1health_get":
                            response = await apiClient.GetBloodGlucoseMetricsAsync(token);
                            break;

                        case "api_v1metrics_get":
                            response = await apiClient.GetBloodGlucoseMetricsAsync(token);
                            break;

                        case "api_v1raceresults_get":
                            response = await apiClient.GetRaceResultsAsync(token);
                            break;

                        case "api_v1tags_get":
                            response = await apiClient.GetTagsAsync(token);
                            break;

                        default:
                            throw new McpException($"Unknown tool: '{toolName}'");
                    }

                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
                    return new CallToolResult
                    {
                        Content = [new TextContentBlock { Text = content, Type = "text" }]
                    };
                }
                catch (Exception ex)
                {
                    throw new McpException($"Tool execution failed: {ex.Message}");
                }
            }
        }
    };
});

var app = builder.Build();

// Basic health check endpoint
app.MapGet("/health", () => "OK");

app.Run();
