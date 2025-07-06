# Development Guide

This guide provides information about developing the Runalyze MCP Server using Visual Studio Code.

## Prerequisites

### Required Software
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Recommended VSCode Extensions
The workspace includes recommended extensions that will be suggested when you open the project:

- **C# Dev Kit** - Complete C# development experience
- **C#** - C# language support
- **Docker** - Docker container management
- **Remote - Containers** - Development inside containers
- **REST Client** - Testing HTTP endpoints
- **PowerShell** - PowerShell scripting support

## Getting Started

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd runalyze-mcp
   ```

2. **Open in VSCode**
   ```bash
   code .
   ```

3. **Install recommended extensions**
   VSCode will prompt you to install the recommended extensions when you open the project.

4. **Build the project**
   - Press `Ctrl+Shift+P` (or `Cmd+Shift+P` on Mac)
   - Type "Tasks: Run Task"
   - Select "build"

## Development Tasks

The project includes several preconfigured tasks accessible via `Ctrl+Shift+P` → "Tasks: Run Task":

### Build Tasks
- **build** - Build the entire solution
- **clean** - Clean build outputs
- **run-local** - Build and run the application locally

### Testing Tasks
- **test** - Run all unit tests
- **test-with-coverage** - Run tests with code coverage reporting

### Docker Tasks
- **docker-build** - Build Docker image with "dev" tag
- **docker-run** - Build and run Docker container
- **docker-compose-up** - Start production Docker Compose services
- **docker-compose-down** - Stop Docker Compose services
- **docker-compose-dev-up** - Start development Docker Compose services
- **docker-logs** - View Docker container logs

## Debugging

### Local Debugging
Several debug configurations are available via `F5` or the Debug panel:

1. **Debug MCP Server** - Standard debugging with development settings
2. **Debug MCP Server (with Custom Base URL)** - Debugging with custom Runalyze API URL
3. **Debug MCP Server (Production Mode)** - Debugging with production settings

### Docker Debugging
For debugging applications running in Docker containers:

1. **Attach to Docker Container** - Attach to production container
2. **Attach to Docker Container (Dev Profile)** - Attach to development container

### Debug Configuration
The debug configurations include:
- Automatic building before debugging
- Environment variable configuration
- Health check integration
- Source map configuration for containers

## Local Development Workflow

### 1. Standard Development
```bash
# Build and run locally
dotnet run --project RunalyzeMcp

# Or use VSCode task
Ctrl+Shift+P → "Tasks: Run Task" → "run-local"
```

### 2. Docker Development
```bash
# Start development environment
docker-compose --profile dev up -d

# Or use VSCode task
Ctrl+Shift+P → "Tasks: Run Task" → "docker-compose-dev-up"
```

### 3. Testing
```bash
# Run tests
dotnet test

# Or use VSCode task
Ctrl+Shift+P → "Tasks: Run Task" → "test"
```

## Environment Configuration

### Development Environment Variables
The following environment variables are used in development:

```bash
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://localhost:8080
RUNALYZE_BASE_URL=https://runalyze.com/api/v1
```

### Port Configuration
- **Local development**: Port 8080
- **Docker development**: Port 8081 (mapped to container port 8080)
- **Docker production**: Port 8080

## Health Checks

The application includes a health check endpoint at `/health` that returns "OK" when the service is running properly.

### Testing Health Check
```bash
# Local development
curl http://localhost:8080/health

# Docker development
curl http://localhost:8081/health
```

## Code Quality

### Formatting
The workspace is configured with:
- Format on save
- Format on paste
- Format on type
- Automatic import organization
- Fix all issues on save

### Linting
- Roslyn analyzers enabled
- EditorConfig support
- Semantic highlighting
- IntelliSense enhancements

## Project Structure

```
runalyze-mcp/
├── .vscode/                 # VSCode configuration
│   ├── extensions.json      # Recommended extensions
│   ├── launch.json         # Debug configurations
│   ├── settings.json       # Workspace settings
│   └── tasks.json          # Build tasks
├── docs/                   # Documentation
├── RunalyzeMcp/            # Main application
│   ├── Program.cs          # Application entry point
│   ├── RunalyzeApiClient.cs # API client implementation
│   ├── ToolDefinitions.cs  # MCP tool definitions
│   └── McpToolHandler.cs   # Tool execution logic
├── RunalyzeMcp.Tests/      # Unit tests
├── docker-compose.yml      # Docker Compose configuration
├── Dockerfile              # Docker image definition
└── README.md              # Project documentation
```

## Common Development Scenarios

### Adding New API Endpoints
1. Update `RunalyzeApiClient.cs` with new methods
2. Add corresponding tool definitions in `ToolDefinitions.cs`
3. Update `McpToolHandler.cs` to handle the new tools
4. Write unit tests in `RunalyzeMcp.Tests`
5. Test locally and with Docker

### Debugging Issues
1. Set breakpoints in VSCode
2. Use the appropriate debug configuration
3. Check logs in the Debug Console
4. Use the integrated terminal for additional commands

### Testing Changes
1. Run unit tests: `Ctrl+Shift+P` → "Tasks: Run Task" → "test"
2. Test locally: `F5` → "Debug MCP Server"
3. Test with Docker: `F5` → "Attach to Docker Container"

## Troubleshooting

### Common Issues

**Build Errors**
- Ensure .NET 9 SDK is installed
- Clean and rebuild: `Ctrl+Shift+P` → "Tasks: Run Task" → "clean"

**Debug Issues**
- Check that the application is not already running
- Verify environment variables are set correctly
- Ensure port 8080 is available

**Docker Issues**
- Ensure Docker is running
- Check Docker Compose logs: `docker-compose logs`
- Verify port mappings

### Getting Help
- Check the main [README.md](../README.md) for general information
- Review the [Knowledge Base](.ai/knowledge-base/README.md) for detailed documentation
- Use the integrated terminal for debugging commands
- Check the Output panel for build and debug information

## Contributing

When contributing to the project:

1. Follow the existing code style and formatting
2. Write unit tests for new functionality
3. Update documentation as needed
4. Test locally before submitting pull requests
5. Use meaningful commit messages

## Next Steps

After setting up your development environment:
1. Review the [API Reference](.ai/knowledge-base/02-api-reference.md)
2. Check the [Testing Strategy](.ai/knowledge-base/06-testing-strategy.md)
3. Read the [System Architecture](.ai/knowledge-base/01-system-architecture.md)
4. Explore the [External API Integration](.ai/knowledge-base/05-external-api-integration.md)