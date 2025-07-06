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
- Complete MCP tool documentation
- Request/response schemas and examples
- Authentication and security considerations
- Error handling and status codes
- Integration examples for MCP clients

### 🛠️ [03-development-setup.md](03-development-setup.md)
**Development Environment Setup**
- Prerequisites and required software
- Step-by-step setup instructions
- IDE configuration (Visual Studio, VS Code)
- Docker development environment
- Testing and debugging setup

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
- Test coverage goals and metrics
- Mocking and test data management
- Integration testing strategies
- Performance and load testing

## Quick Reference

### Key Project Information
- **Project**: Runalyze MCP Server
- **Technology**: .NET 9, ASP.NET Core, Model Context Protocol
- **External API**: Runalyze Personal API
- **Container**: Docker with GitHub Container Registry
- **Testing**: NUnit with comprehensive coverage

### Core Components
- **RunalyzeApiClient**: Custom HTTP client for Runalyze API
- **MCP Server**: Model Context Protocol server implementation
- **Health Endpoint**: Server health monitoring
- **Dependency Injection**: HttpClientFactory integration

### External Dependencies
- **Runalyze API**: Token-based authentication, REST endpoints
- **ModelContextProtocol.AspNetCore**: MCP server framework
- **NSwag.Core**: OpenAPI specification handling
- **NUnit**: Unit testing framework

## Common Tasks

### Development Workflow
1. **Setup**: Follow [03-development-setup.md](03-development-setup.md)
2. **API Integration**: Reference [05-external-api-integration.md](05-external-api-integration.md)
3. **Testing**: Use [06-testing-strategy.md](06-testing-strategy.md)
4. **Architecture**: Review [01-system-architecture.md](01-system-architecture.md)
5. **API Reference**: Check [02-api-reference.md](02-api-reference.md)

### Maintenance Tasks
- Monitor Runalyze API changes and update client
- Maintain test coverage and update tests
- Update documentation for new features
- Review and update security considerations

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

### Current Version
- **RunalyzeApiClient**: Complete implementation with all endpoints
- **Test Coverage**: 61 tests covering all functionality
- **Documentation**: Comprehensive API reference and integration guide
- **Architecture**: .NET 9 ASP.NET Core with MCP server
