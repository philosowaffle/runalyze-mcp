# Session Goal

Scaffold the initial Runalyze MCP server, set up .NET 9 solution, configure MCP/OpenAPI integration, and document the architecture and setup.

## Requirements
- .NET 9 ASP.NET Core MCP server
- Expose all Runalyze API endpoints as MCP tools
- Use environment variable RUNALYZE_BASE_URL for API base URL
- Listen on port 8080
- Containerized, published to GHCR
- Unit tests with NUnit
- Knowledge base and session documentation per rules

## Plan
1. Scaffold .NET solution and projects
2. Add required NuGet packages
3. Configure Program.cs for port/env/health
4. Integrate MCP server and OpenAPI tool generation
5. Add Dockerfile and GitHub Actions
6. Write/readme and update knowledge base
7. Write session file

## Todo
- [x] Scaffold solution and projects
- [x] Add required NuGet packages
- [x] Configure Program.cs for port/env/health
- [ ] Integrate MCP server and OpenAPI tool generation
- [ ] Add Dockerfile and GitHub Actions
- [ ] Write/readme and update knowledge base
- [x] Write session file

## Notes from last session
- All endpoints (free, supporter, premium) will be exposed
- Token is passed as a parameter and mapped to the `token` header
- File uploads are base64-encoded
- Prefer `application/ld+json`, fallback to `application/json`
- Docker image will be published to `ghcr.io/philosowaffle/runalyze-mcp`
- Port 8080 is used
- NUnit is the test framework 