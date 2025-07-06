# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore RunalyzeMcp/RunalyzeMcp.csproj
RUN dotnet publish RunalyzeMcp/RunalyzeMcp.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Environment variable for base URL
ENV RUNALYZE_BASE_URL=https://runalyze.com

# Expose MCP server port
EXPOSE 8080

# Labels for GHCR
LABEL org.opencontainers.image.source="https://github.com/philosowaffle/runalyze-mcp"

# Run the MCP server
ENTRYPOINT ["dotnet", "RunalyzeMcp.dll"] 