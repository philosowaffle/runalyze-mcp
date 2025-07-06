# Development Setup: Runalyze MCP

## Prerequisites
- .NET 9 SDK
- Docker (for containerization)
- Runalyze account with API access

## Project Structure
```
runalyze-mcp/
├── RunalyzeMcp/                 # Main application
│   ├── Program.cs               # Application entry point and MCP server configuration
│   ├── RunalyzeApiClient.cs     # HTTP client for Runalyze API
│   ├── ToolDefinitions.cs       # MCP tool definitions (47 endpoints)
│   └── McpToolHandler.cs        # MCP tool call handler
├── RunalyzeMcp.Tests/           # Unit tests (61 tests)
├── Dockerfile                   # Multi-stage Docker build
├── docker-compose.yml           # Docker Compose configuration
└── README.md                    # Comprehensive documentation
```

## Building

### Local Development
```bash
# Build the solution
dotnet build

# Build and run
dotnet run --project RunalyzeMcp

# Build and run tests
dotnet test
```

### Docker Build
```bash
# Build Docker image
docker build -t runalyze-mcp .

# Build with specific tag
docker build -t ghcr.io/philosowaffle/runalyze-mcp:latest .
```

## Running Locally

### Direct Execution
```bash
# Run the application
dotnet run --project RunalyzeMcp

# Run with custom environment variables
set RUNALYZE_BASE_URL=https://custom-url
dotnet run --project RunalyzeMcp
```

### Docker
```bash
# Run with Docker
docker run -p 8080:8080 \
  -e RUNALYZE_BASE_URL=https://runalyze.com/api/v1 \
  ghcr.io/philosowaffle/runalyze-mcp:latest

# Run with custom configuration
docker run -p 8080:8080 \
  -e RUNALYZE_BASE_URL=https://custom-url \
  -e ASPNETCORE_ENVIRONMENT=Development \
  runalyze-mcp
```

### Docker Compose
```bash
# Run production service
docker-compose up -d

# Run development service
docker-compose --profile dev up -d

# View logs
docker-compose logs -f runalyze-mcp

# Stop services
docker-compose down
```

## Testing

### Unit Tests
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test RunalyzeMcp.Tests

# Run with verbose output
dotnet test --verbosity normal
```

### Test Coverage
- **61 tests** covering all Runalyze API endpoints
- **Configuration tests** for environment variables and base URL
- **HTTP client tests** for headers, authentication, and responses
- **Endpoint tests** for all 47 API endpoints
- **Error handling tests** for various failure scenarios

### Test Categories
- ConfigurationTests
- HeaderTests
- ActivityTests
- DownloadTests
- StatisticsTests
- EquipmentTests
- HealthTests
- MetricsTests
- RaceResultTests
- TagTests

## Environment Configuration

### Environment Variables
| Variable | Default | Description |
|----------|---------|-------------|
| `RUNALYZE_BASE_URL` | `https://runalyze.com/api/v1` | Base URL for Runalyze API |
| `ASPNETCORE_ENVIRONMENT` | `Production` | ASP.NET Core environment |
| `ASPNETCORE_URLS` | `http://+:8080` | Server listening URLs |

### Development vs Production
- **Development**: Detailed error messages, hot reload support
- **Production**: Optimized performance, minimal logging

## MCP Server Configuration

### Tool Definitions
All 47 Runalyze API endpoints are exposed as MCP tools in `RunalyzeMcp/ToolDefinitions.cs`:
- Activity management (upload, status, collection, individual)
- File downloads (FIT, GPX, KML, TCX)
- Statistics and equipment
- Health data uploads
- Metrics CRUD operations
- Race results and tags

### Tool Handler
Tool execution logic is implemented in `RunalyzeMcp/McpToolHandler.cs`:
- Processes all MCP tool calls
- Handles parameter validation
- Manages HTTP requests to Runalyze API
- Formats responses for MCP clients

## Docker Configuration

### Multi-stage Build
The Dockerfile uses a multi-stage build for optimization:
1. **Build stage**: Compiles the .NET application
2. **Runtime stage**: Creates minimal runtime image
3. **Final image**: ~100MB optimized container

### Health Checks
Docker Compose includes health checks:
- Checks `/health` endpoint every 30 seconds
- 10-second timeout with 3 retries
- 40-second start period for initialization

### Traefik Integration
Docker Compose includes Traefik labels for reverse proxy integration:
- Automatic service discovery
- Host-based routing
- Load balancing configuration

## Development Workflow

### 1. Local Development
```bash
# Clone and setup
git clone https://github.com/philosowaffle/runalyze-mcp.git
cd runalyze-mcp

# Build and test
dotnet build
dotnet test

# Run locally
dotnet run --project RunalyzeMcp
```

### 2. Docker Development
```bash
# Build and run with Docker Compose
docker-compose --profile dev up -d

# View logs
docker-compose logs -f runalyze-mcp-dev

# Rebuild and restart
docker-compose --profile dev up -d --build
```

### 3. Testing
```bash
# Run tests locally
dotnet test

# Run tests in Docker
docker-compose --profile dev run runalyze-mcp-dev dotnet test
```

## Troubleshooting

### Common Issues
1. **Port conflicts**: Change port in docker-compose.yml or use different port
2. **Environment variables**: Ensure RUNALYZE_BASE_URL is set correctly
3. **Docker build failures**: Check .NET 9 SDK installation
4. **Test failures**: Verify Runalyze API accessibility

### Debug Mode
```bash
# Run with debug logging
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --project RunalyzeMcp

# Docker debug mode
docker-compose --profile dev up -d
docker-compose logs -f runalyze-mcp-dev
```

## Visual Studio Code Development

### VSCode Configuration
The project includes comprehensive VSCode configuration for enhanced development experience:

#### Configuration Files
- `.vscode/tasks.json` - Build, test, and Docker tasks
- `.vscode/launch.json` - Debug configurations
- `.vscode/settings.json` - Workspace-specific settings
- `.vscode/extensions.json` - Recommended extensions

#### Recommended Extensions
- **C# Dev Kit** - Complete C# development experience
- **C#** - C# language support
- **Docker** - Docker container management
- **Remote - Containers** - Development inside containers
- **REST Client** - Testing HTTP endpoints
- **PowerShell** - PowerShell scripting support

#### Available Tasks (`Ctrl+Shift+P` → "Tasks: Run Task")
- **build** - Build the entire solution
- **clean** - Clean build outputs
- **run-local** - Build and run the application locally
- **test** - Run all unit tests
- **test-with-coverage** - Run tests with code coverage
- **docker-build** - Build Docker image with "dev" tag
- **docker-run** - Build and run Docker container
- **docker-compose-up** - Start production services
- **docker-compose-down** - Stop services
- **docker-compose-dev-up** - Start development services
- **docker-logs** - View container logs

#### Debug Configurations (F5 or Debug panel)
- **Debug MCP Server** - Standard debugging with development settings
- **Debug MCP Server (with Custom Base URL)** - Debug with custom Runalyze API URL
- **Debug MCP Server (Production Mode)** - Debug with production settings
- **Attach to Docker Container** - Debug production container
- **Attach to Docker Container (Dev Profile)** - Debug development container

### Development Workflow with VSCode
1. **Open project**: `code .`
2. **Install extensions**: Accept recommended extensions prompt
3. **Build project**: `Ctrl+Shift+P` → "Tasks: Run Task" → "build"
4. **Run tests**: `Ctrl+Shift+P` → "Tasks: Run Task" → "test"
5. **Debug locally**: `F5` → "Debug MCP Server"
6. **Docker development**: `Ctrl+Shift+P` → "Tasks: Run Task" → "docker-compose-dev-up"

### Code Quality Features
- Format on save, paste, and type
- Automatic import organization
- Fix all issues on save
- Roslyn analyzers enabled
- Semantic highlighting
- IntelliSense enhancements

## Next Steps
- [x] Complete MCP server integration (47 endpoints)
- [x] Comprehensive unit testing (61 tests)
- [x] Docker containerization
- [x] Docker Compose configuration
- [x] VSCode development tasks and debug configurations
- [ ] GitHub Actions CI/CD pipeline
- [ ] End-user documentation updates 