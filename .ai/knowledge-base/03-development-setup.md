# Development Setup: Runalyze MCP

## Prerequisites
- .NET 9 SDK
- Docker (for containerization)

## Building
```sh
dotnet build
```

## Running Locally
```sh
dotnet run --project RunalyzeMcp
```
- The server listens on port 8080 by default.
- Override the Runalyze API base URL with:
  - `set RUNALYZE_BASE_URL=https://custom-url`

## Testing
```sh
dotnet test
```
- Tests are written with NUnit and code coverage is collected with coverlet.

## Docker
- Build the image:
  ```sh
  docker build -t runalyze-mcp .
  ```
- Run the container:
  ```sh
  docker run -p 8080:8080 -e RUNALYZE_BASE_URL=https://custom-url runalyze-mcp
  ``` 