# MCP Server API Reference

> **Note:** All 47 Runalyze API endpoints are available as MCP tools. Tool definitions are in `RunalyzeMcp/ToolDefinitions.cs` and handlers are in `RunalyzeMcp/McpToolHandler.cs`. For details on the external Runalyze API, see [05-external-api-integration.md](05-external-api-integration.md).

# API Reference: Runalyze MCP Server

## Overview
The Runalyze MCP Server exposes the Runalyze API as Model Context Protocol (MCP) tools, allowing AI assistants and other MCP clients to interact with Runalyze data programmatically.

## MCP Server Configuration

### Server Information
- **Protocol**: Model Context Protocol (MCP)
- **Transport**: HTTP/JSON-RPC
- **Port**: 8080 (configurable)
- **Base URL**: Configurable via `RUNALYZE_BASE_URL` environment variable

### Health Endpoint
```
GET /health
```
Returns server health status.

**Response:**
```json
"OK"
```

## MCP Tools

### Tool Namespace
All tools are exposed under the namespace: `Runalyze API`

### Tool Naming Convention
Tools use the OpenAPI `operationId` values as their names:
- `api_activity_upload`
- `api_v1activitiesuploads_id_get`
- `api_v1activity_get_collection`
- etc.

### Common Parameters

#### Authentication
All tools require a `token` parameter for Runalyze API authentication:
```json
{
  "token": "your-runalyze-api-token"
}
```

#### Pagination (for collection endpoints)
```json
{
  "page": 1,
  "order[id]": "asc"
}
```

#### File Uploads
File uploads accept base64-encoded strings:
```json
{
  "base64File": "dGVzdA=="
}
```

## Activity Tools

### api_activity_upload
Uploads an activity file to Runalyze.

**Parameters:**
```json
{
  "token": "string",
  "base64File": "string",
  "title": "string (optional)",
  "note": "string (optional)",
  "route": "string (optional)",
  "elevationUp": "integer (optional)",
  "elevationDown": "integer (optional)"
}
```

**Response:**
```json
{
  "id": "activity-id",
  "status": "processing"
}
```

### api_v1activitiesuploads_id_get
Retrieves the status of an activity upload.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

### api_v1activity_get_collection
Retrieves a collection of activities.

**Parameters:**
```json
{
  "token": "string",
  "page": "integer (optional)",
  "order[id]": "string (optional)"
}
```

### api_v1activity_id_get
Retrieves a specific activity by ID.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

## Download Tools

### download_fit_original
Downloads the original FIT file for an activity.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

**Response:** Binary file content

### download_fitlog
Downloads the FITLOG file for an activity.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

### download_gpx
Downloads the GPX file for an activity.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

### download_kml
Downloads the KML file for an activity.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

### download_social_image
Downloads the social image for an activity.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

### download_tcx
Downloads the TCX file for an activity.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

## Statistics Tools

### api_v1statisticscurrent_get
Retrieves current user statistics.

**Parameters:**
```json
{
  "token": "string"
}
```

## Equipment Tools

### api_v1equipment_get_collection
Retrieves a collection of equipment.

**Parameters:**
```json
{
  "token": "string",
  "page": "integer (optional)",
  "order[id]": "string (optional)"
}
```

### api_v1equipment_id_get
Retrieves specific equipment by ID.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

### api_v1equipmentcategory_get_collection
Retrieves equipment categories.

**Parameters:**
```json
{
  "token": "string",
  "page": "integer (optional)",
  "order[id]": "string (optional)"
}
```

### api_v1equipmentcategory_id_get
Retrieves specific equipment category by ID.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

## Health Tools

### api_health_bulk_upload
Bulk uploads health data from a CSV file.

**Parameters:**
```json
{
  "token": "string",
  "base64File": "string"
}
```

## Metrics Tools

### Blood Glucose Metrics
- `api_v1metricsbloodGlucose_get_collection` - Get collection
- `api_v1metricsbloodGlucose_post` - Create new metric
- `api_v1metricsbloodGlucose_id_get` - Get by ID

### Blood Pressure Metrics
- `api_v1metricsbloodPressure_get_collection` - Get collection
- `api_v1metricsbloodPressure_post` - Create new metric
- `api_v1metricsbloodPressure_id_get` - Get by ID

### Body Composition Metrics
- `api_v1metricsbodyComposition_get_collection` - Get collection
- `api_v1metricsbodyComposition_post` - Create new metric
- `api_v1metricsbodyComposition_id_get` - Get by ID

### Body Temperature Metrics
- `api_v1metricsbodyTemperature_get_collection` - Get collection
- `api_v1metricsbodyTemperature_post` - Create new metric
- `api_v1metricsbodyTemperature_id_get` - Get by ID

### Daily Note Metrics
- `api_v1metricsdailyNote_get_collection` - Get collection
- `api_v1metricsdailyNote_post` - Create new metric
- `api_v1metricsdailyNote_date_get` - Get by date

### HRV Metrics
- `api_v1metricshrv_get_collection` - Get collection
- `api_v1metricshrv_post` - Create new metric
- `api_v1metricshrv_id_get` - Get by ID

### Heart Rate Max Metrics
- `api_v1metricsheartRateMax_get_collection` - Get collection
- `api_v1metricsheartRateMax_post` - Create new metric
- `api_v1metricsheartRateMax_id_get` - Get by ID

### Heart Rate Rest Metrics
- `api_v1metricsheartRateRest_get_collection` - Get collection
- `api_v1metricsheartRateRest_post` - Create new metric
- `api_v1metricsheartRateRest_id_get` - Get by ID

### Mental Metrics
- `api_v1metricsmental_get_collection` - Get collection
- `api_v1metricsmental_post` - Create new metric
- `api_v1metricsmental_date_get` - Get by date

### Sleep Metrics
- `api_v1metricssleep_get_collection` - Get collection
- `api_v1metricssleep_post` - Create new metric
- `api_v1metricssleep_id_get` - Get by ID

## Race Result Tools

### api_v1raceresult_get_collection
Retrieves race results.

**Parameters:**
```json
{
  "token": "string",
  "page": "integer (optional)",
  "order[id]": "string (optional)"
}
```

### api_v1raceresult_activity_id_get
Retrieves race result for a specific activity.

**Parameters:**
```json
{
  "token": "string",
  "activityId": "string"
}
```

## Tag Tools

### api_v1tag_get_collection
Retrieves tags.

**Parameters:**
```json
{
  "token": "string",
  "page": "integer (optional)",
  "order[id]": "string (optional)"
}
```

### api_v1tag_id_get
Retrieves specific tag by ID.

**Parameters:**
```json
{
  "token": "string",
  "id": "string"
}
```

## Error Handling

### MCP Error Responses
```json
{
  "error": {
    "code": 400,
    "message": "Invalid parameters",
    "data": {
      "details": "Additional error information"
    }
  }
}
```

### Common Error Codes
- `400 Bad Request`: Invalid parameters or request format
- `401 Unauthorized`: Invalid or missing token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Tool or resource not found
- `422 Unprocessable Entity`: Validation errors
- `500 Internal Server Error`: Server error

## Content Types

### Request Content Types
- `application/json`: Standard JSON requests
- `multipart/form-data`: File uploads (handled internally)

### Response Content Types
- `application/ld+json`: Preferred for structured data
- `application/json`: Fallback for JSON responses
- `text/csv`: For activity collections
- `application/octet-stream`: For file downloads
- `image/png`: For social images

## Rate Limiting

The MCP server respects Runalyze API rate limits and may return rate limit errors when the underlying API is throttled.

## Security

### Token Security
- Tokens are passed through to the Runalyze API
- No token storage or caching on the MCP server
- Tokens should be obtained from Runalyze user settings

### Data Privacy
- All data is transmitted securely over HTTPS
- No data is stored or logged by the MCP server
- User consent is required for Runalyze API access

## Usage Examples

### Upload Activity
```json
{
  "method": "tools/call",
  "params": {
    "name": "api_activity_upload",
    "arguments": {
      "token": "your-api-token",
      "base64File": "dGVzdA==",
      "title": "Morning Run"
    }
  }
}
```

### Get Activities
```json
{
  "method": "tools/call",
  "params": {
    "name": "api_v1activity_get_collection",
    "arguments": {
      "token": "your-api-token",
      "page": 1,
      "order[id]": "desc"
    }
  }
}
```

### Download GPX File
```json
{
  "method": "tools/call",
  "params": {
    "name": "download_gpx",
    "arguments": {
      "token": "your-api-token",
      "id": "activity-id"
    }
  }
} 