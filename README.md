# Runalyze MCP Server

A .NET 9 ASP.NET Core application that exposes the [Runalyze Personal API](https://runalyze.com/help/article/personal-api) as Model Context Protocol (MCP) tools. This server enables AI assistants and other MCP clients to interact with Runalyze's comprehensive fitness tracking platform.

## Features

- **Complete API Coverage**: All 47 Runalyze API endpoints exposed as MCP tools
- **Token-based Authentication**: Secure API access using Runalyze personal tokens
- **File Upload Support**: Base64-encoded file uploads for activities and health data
- **Content Negotiation**: Prefers `application/ld+json` with fallback to `application/json`
- **Docker Support**: Containerized deployment with multi-stage builds
- **Comprehensive Testing**: 61 unit tests with full endpoint coverage

## Quick Start

### Using Docker (Recommended)

```bash
# Pull the latest image
docker pull ghcr.io/philosowaffle/runalyze-mcp:latest

# Run with your Runalyze API token
docker run -p 8080:8080 \
  -e RUNALYZE_BASE_URL=https://runalyze.com/api/v1 \
  ghcr.io/philosowaffle/runalyze-mcp:latest
```

### Using Docker Compose

```yaml
# docker-compose.yml
version: '3.8'
services:
  runalyze-mcp:
    image: ghcr.io/philosowaffle/runalyze-mcp:latest
    ports:
      - "8080:8080"
    environment:
      - RUNALYZE_BASE_URL=https://runalyze.com/api/v1
    restart: unless-stopped
```

```bash
docker-compose up -d
```

### Local Development

```bash
# Clone the repository
git clone https://github.com/philosowaffle/runalyze-mcp.git
cd runalyze-mcp

# Build and run
dotnet run --project RunalyzeMcp

# Or build and run tests
dotnet test
dotnet run --project RunalyzeMcp
```

## Configuration

### Environment Variables

| Variable | Default | Description |
|----------|---------|-------------|
| `RUNALYZE_BASE_URL` | `https://runalyze.com/api/v1` | Base URL for Runalyze API |
| `ASPNETCORE_ENVIRONMENT` | `Production` | ASP.NET Core environment |
| `ASPNETCORE_URLS` | `http://+:8080` | Server listening URLs |

### Runalyze API Token

To use the MCP server, you need a Runalyze personal API token:

1. Log in to your Runalyze account
2. Go to Settings → Personal API
3. Generate a new token
4. Use this token as the `token` parameter in all MCP tool calls

## MCP Tools

The server exposes all Runalyze API endpoints as MCP tools. Tool names match the OpenAPI `operationId` values:

### Activity Management
- `api_activity_upload` - Upload activity files (FIT, GPX, TCX, etc.)
- `api_activity_status` - Check upload status
- `api_v1activities_get` - List activities
- `api_v1activities_id_get` - Get specific activity

### File Downloads
- `api_v1activities_id_fit_get` - Download FIT file
- `api_v1activities_id_gpx_get` - Download GPX file
- `api_v1activities_id_kml_get` - Download KML file
- `api_v1activities_id_tcx_get` - Download TCX file

### Statistics
- `api_v1statistics_get` - Get user statistics

### Equipment
- `api_v1equipment_get` - List equipment
- `api_v1equipment_id_get` - Get specific equipment
- `api_v1equipment_categories_get` - List equipment categories

### Health Data
- `api_v1health_bulk_upload` - Upload health data

### Metrics
- `api_v1metrics_get` - List metrics
- `api_v1metrics_id_get` - Get specific metric
- `api_v1metrics_post` - Create metric
- `api_v1metrics_id_put` - Update metric
- `api_v1metrics_id_delete` - Delete metric

### Race Results
- `api_v1race_results_get` - List race results
- `api_v1race_results_id_get` - Get specific race result

### Tags
- `api_v1tags_get` - List tags
- `api_v1tags_id_get` - Get specific tag

## Usage Examples

### Connecting with an MCP Client

```json
{
  "mcpServers": {
    "runalyze": {
      "command": "docker",
      "args": ["run", "--rm", "-p", "8080:8080", "ghcr.io/philosowaffle/runalyze-mcp:latest"],
      "env": {
        "RUNALYZE_BASE_URL": "https://runalyze.com/api/v1"
      }
    }
  }
}
```

### Tool Call Example

```json
{
  "name": "api_v1activities_get",
  "arguments": {
    "token": "your-runalyze-api-token",
    "limit": 10,
    "offset": 0
  }
}
```

### File Upload Example

```json
{
  "name": "api_activity_upload",
  "arguments": {
    "token": "your-runalyze-api-token",
    "base64File": "base64-encoded-file-content",
    "filename": "activity.fit"
  }
}
```

## Development

### Prerequisites

- .NET 9 SDK
- Docker (optional)
- Runalyze account with API access

### Building

```bash
# Build the solution
dotnet build

# Run tests
dotnet test

# Build Docker image
docker build -t runalyze-mcp .
```

### Project Structure

```
runalyze-mcp/
├── RunalyzeMcp/                 # Main application
│   ├── Program.cs               # Application entry point
│   ├── RunalyzeApiClient.cs     # HTTP client for Runalyze API
│   ├── ToolDefinitions.cs       # MCP tool definitions
│   └── McpToolHandler.cs        # MCP tool call handler
├── RunalyzeMcp.Tests/           # Unit tests
├── Dockerfile                   # Multi-stage Docker build
├── docker-compose.yml           # Docker Compose configuration
└── README.md                    # This file
```

## Testing

The project includes comprehensive unit tests covering:

- Configuration and environment variables
- HTTP client behavior
- All API endpoints
- Error handling
- Authentication

Run tests with:

```bash
dotnet test
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- [Runalyze API Documentation](https://runalyze.com/help/article/personal-api)
- [Model Context Protocol](https://modelcontextprotocol.io/)
- [Issues](https://github.com/philosowaffle/runalyze-mcp/issues)

## Acknowledgments

- [Runalyze](https://runalyze.com/) for providing the comprehensive fitness tracking API
- [Model Context Protocol](https://modelcontextprotocol.io/) for the MCP specification
- [.NET MCP SDK](https://github.com/modelcontextprotocol/dotnet-mcp) for the .NET implementation