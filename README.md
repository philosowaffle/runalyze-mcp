Runalyze MCP Server
====================

This project exposes the Runalyze Personal API as Model Context Protocol (MCP) tools using .NET 9 and the official MCP SDK.

## Tool Definitions

All MCP tool definitions are located in `RunalyzeMcp/ToolDefinitions.cs` as a static list. These are referenced in `Program.cs` for MCP server registration.

## MCP Server

The MCP server is configured in `Program.cs` and uses the tool definitions from `ToolDefinitions.AllTools` for registration. The server exposes all Runalyze API endpoints as MCP tools, with token-based authentication and base64-encoded file uploads.

## Usage

- Configure the `RUNALYZE_BASE_URL` environment variable to override the default Runalyze API base URL.
- Build and run the server with `dotnet run --project RunalyzeMcp`.

See the knowledge base for more details on API integration and tool usage.