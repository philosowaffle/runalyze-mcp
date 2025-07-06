# Runalyze MCP Requirements Overview

This document captures the initial requirements and design choices that will be implemented in the Runalyze MCP Server.

## Problem

As a user I would like to be able to interact with my training data stored on the [Runalyze platform](https://runalyze.com) using the LLM of my choice.  As a user, allowing an LLM to access this data will give me many benefits such as improved insights into my training history and recommendations for future training improvements.

## Solution

To solve for this problem, I will build an [MCP server(https://modelcontextprotocol.io/specification/2025-06-18) that exposes the [Runalyze Personal API](https://runalyze.com/help/article/personal-api?_locale=de) as a tool for LLM's to use.

## Functional Requirements

1. The MCP server should expose all Runalyze API Endpoints as tools
2. The MCP server should be an HTTP server, not a local MCP server
3. The MCP server should be containerized and hostable via a docker container
4. The MCP server should accept the users Runalyze API key as an input parameter

## Non Functional Requirements

1. Unit Tests with code coverage
2. End User Documentation
3. VSCode tasks and launch profiles for development cases
4. GitHub actions for PR valiadtions and publishing releases

## Technology

1. C#, .NET 9 application using the [C# MCP SKD](https://github.com/modelcontextprotocol/csharp-sdk)
2. Docker for containerization
3. GitHub actions for building and publishing docker images

## External References

1. Runalyze
    1. [Runalyze Personal API Description](https://runalyze.com/help/article/personal-api?_locale=de)
    2. [Runalyze Open API Spec](https://runalyze.com/doc/api/premium)
2. MCP
    1. [MCP Spec](https://modelcontextprotocol.io/specification/2025-06-18)
    2. [MCP Server Features](https://modelcontextprotocol.io/specification/2025-06-18/server/index)
3. DotNet MCP
    1. [C# MCP SKD](https://github.com/modelcontextprotocol/csharp-sdk)
    2. [Dotnet MCP Getting Started](https://learn.microsoft.com/en-us/dotnet/ai/get-started-mcp)
    3. [Http Based MCP Server](https://github.com/modelcontextprotocol/csharp-sdk/blob/main/src/ModelContextProtocol.AspNetCore/README.md)

