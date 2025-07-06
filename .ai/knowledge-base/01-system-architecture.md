# System Architecture: Runalyze MCP

## Overview
Runalyze MCP is a .NET 9 ASP.NET Core application that exposes the Runalyze API as Model Context Protocol (MCP) tools. It is designed to be containerized and run as an HTTP server, listening on port 8080 by default.

## Key Components
- **ASP.NET Core Web API**: Hosts the MCP server and tool endpoints.
- **MCP Integration**: Uses ModelContextProtocol.AspNetCore to expose OpenAPI-defined tools.
- **Runalyze API**: All endpoints (free, supporter, premium) are exposed as tools, requiring a `token` parameter for authentication.
- **Configuration**: The base URL for the Runalyze API can be overridden using the `RUNALYZE_BASE_URL` environment variable.
- **Testing**: Unit tests are written using NUnit.
- **Containerization**: The server is designed to run in Docker and is published to GitHub Container Registry.

## Data Flow
- User/LLM calls MCP tool endpoint
- MCP server proxies request to Runalyze API, passing the user-supplied token
- Response is returned in `application/ld+json` if supported, else `application/json`

## Deployment
- Exposes HTTP on port 8080
- Docker image published to `ghcr.io/philosowaffle/runalyze-mcp`

## Tool Definitions

All MCP tool definitions are now located in `RunalyzeMcp/ToolDefinitions.cs` as a static list. These are referenced in `Program.cs` for MCP server registration. This approach enables centralized and maintainable management of all tool schemas and metadata, ensuring consistency and ease of updates.

The MCP server uses these definitions to dynamically register all supported tools at startup.

## Tool Handler

The tool execution logic is implemented in `RunalyzeMcp/McpToolHandler.cs` as a static class. This contains the `HandleToolCallAsync` method that processes all MCP tool calls and delegates to the appropriate Runalyze API endpoints. This separation improves code organization and maintainability. 