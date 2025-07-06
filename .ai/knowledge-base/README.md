# Knowledge Base

## Overview
This knowledge base contains comprehensive documentation for maintaining, fixing, and enhancing the Runalyze MCP application. It serves as a reference guide for developers, maintainers, and contributors.

## Knowledge Base Structure

### 📋 [01-system-architecture.md](01-system-architecture.md)
**System Architecture Documentation**
- High-level system overview and component interactions
- Data flow diagrams and MCP server workflow
- Technology stack and deployment models
- Security and scalability considerations
- File system organization and error handling

### 🔌 [02-api-reference.md](02-api-reference.md)
**MCP Server API Reference**
- Complete MCP tool documentation (47 endpoints)
- Request/response schemas and examples
- Authentication and security considerations
- Error handling and status codes
- Integration examples for MCP clients

### 🛠️ [03-development-setup.md](03-development-setup.md)
**Development Environment Setup**
- Prerequisites and required software
- Step-by-step setup instructions
- Docker and Docker Compose configuration
- Testing and debugging setup
- Development workflow and troubleshooting

### 🌐 [05-external-api-integration.md](05-external-api-integration.md)
**Runalyze API Integration Guide**
- Runalyze API authentication and endpoints
- Custom HTTP client implementation details
- Rate limiting and error handling strategies
- Performance optimization techniques
- Security and monitoring considerations

### 🧪 [06-testing-strategy.md](06-testing-strategy.md)
**Testing Strategy and Framework**
- Unit testing approach with NUnit
- Test coverage goals and metrics (61 tests)
- Mocking and test data management
- Integration testing strategies
- Performance and load testing

## Quick Reference

### Key Project Information
- **Project**: Runalyze MCP Server
- **Technology**: .NET 9, ASP.NET Core, Model Context Protocol
- **External API**: Runalyze Personal API (47 endpoints)
- **Container**: Docker with GitHub Container Registry
- **Testing**: NUnit with comprehensive coverage (61 tests)
- **Status**: ✅ Complete MCP integration, ✅ Docker containerization, ✅ Documentation

### Core Components
- **RunalyzeApiClient**: Custom HTTP client for Runalyze API
- **MCP Server**: Model Context Protocol server implementation
- **ToolDefinitions**: All 47 Runalyze endpoints as MCP tools
- **McpToolHandler**: Tool execution and response handling
- **Health Endpoint**: Server health monitoring
- **Dependency Injection**: HttpClientFactory integration

### External Dependencies
- **Runalyze API**: Token-based authentication, REST endpoints
- **ModelContextProtocol.AspNetCore**: MCP server framework
- **NSwag.Core**: OpenAPI specification handling
- **NUnit**: Unit testing framework

## Project Status

### ✅ Completed Phases
1. **Foundation**: .NET 9 solution, packages, basic configuration
2. **MCP Server Integration**: All 47 endpoints exposed as tools
3. **Testing**: 61 comprehensive unit tests
4. **Containerization**: Docker multi-stage build
5. **Documentation**: Comprehensive README and knowledge base

### 🔄 Current Phase
- **Docker Compose**: Production and development configurations
- **End User Documentation**: Complete setup and usage guides

### 📋 Next Phases
- **CI/CD Pipeline**: GitHub Actions for PR validation and releases
- **Development Experience**: VSCode tasks and launch profiles
- **Final Polish**: Code review, release notes, initial release

## Common Tasks

### Development Workflow
1. **Setup**: Follow [03-development-setup.md](03-development-setup.md)
2. **API Integration**: Reference [05-external-api-integration.md](05-external-api-integration.md)
3. **Testing**: Use [06-testing-strategy.md](06-testing-strategy.md)
4. **Architecture**: Review [01-system-architecture.md](01-system-architecture.md)
5. **API Reference**: Check [02-api-reference.md](02-api-reference.md)

### Deployment Workflow
1. **Local Development**: `dotnet run --project RunalyzeMcp`
2. **Docker Development**: `docker-compose --profile dev up -d`
3. **Production**: `docker-compose up -d`
4. **Testing**: `dotnet test` (61 tests)

### Maintenance Tasks
- Monitor Runalyze API changes and update client
- Maintain test coverage and update tests
- Update documentation for new features
- Review and update security considerations

## Docker Configuration

### Production Deployment
```bash
# Using Docker Compose
docker-compose up -d

# Using Docker directly
docker run -p 8080:8080 ghcr.io/philosowaffle/runalyze-mcp:latest
```

### Development Environment
```bash
# Development profile with hot reload
docker-compose --profile dev up -d

# View logs
docker-compose logs -f runalyze-mcp-dev
```

### Health Checks
- Automatic health monitoring via `/health` endpoint
- 30-second intervals with 3 retry attempts
- Traefik integration for reverse proxy support

## Contributing to Knowledge Base

### Adding New Documentation
1. Create new markdown files following the naming convention
2. Update this README.md with links to new documentation
3. Ensure cross-references between related documents
4. Use consistent formatting and structure

### Updating Existing Documentation
1. Keep information current with code changes
2. Add new troubleshooting scenarios as they arise
3. Update API documentation when endpoints change
4. Maintain accuracy of configuration examples

### Documentation Standards
- Use clear, descriptive headings
- Include code examples where appropriate
- Provide step-by-step instructions
- Cross-reference related information
- Keep language concise but comprehensive

## Version History

### Current Version (Latest)
- **RunalyzeApiClient**: Complete implementation with all 47 endpoints
- **MCP Server**: Full integration with all tools exposed
- **Test Coverage**: 61 tests covering all functionality
- **Documentation**: Comprehensive API reference and integration guide
- **Architecture**: .NET 9 ASP.NET Core with MCP server
- **Containerization**: Docker multi-stage build with Docker Compose
- **Status**: Ready for CI/CD pipeline and production deployment

### Key Achievements
- ✅ All Runalyze API endpoints implemented and tested
- ✅ Complete MCP server integration
- ✅ Comprehensive unit test coverage
- ✅ Docker containerization with optimization
- ✅ Production-ready documentation
- ✅ Development environment configuration
