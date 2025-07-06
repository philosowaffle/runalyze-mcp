# External API Integration: Runalyze API

## Overview
The Runalyze API integration provides comprehensive access to the Runalyze Personal API through a custom HTTP client implementation. This integration supports all documented endpoints including activities, metrics, equipment, and health data.

## RunalyzeApiClient

A custom HTTP client that implements the Runalyze API specification, registered as a singleton with dependency injection.

### Configuration
- **Base URL**: Configurable via `RUNALYZE_BASE_URL` environment variable, defaults to `https://runalyze.com`
- **Authentication**: Token-based via `token` header parameter
- **Content Types**: Prefers `application/ld+json`, falls back to `application/json`
- **File Uploads**: Accepts base64-encoded strings for file uploads

### Registration
```csharp
builder.Services.AddHttpClient<RunalyzeApiClient>();
```

## API Endpoints

### Activity Endpoints

#### UploadActivityAsync
Uploads an activity file to Runalyze.

**Signature:**
```csharp
Task<HttpResponseMessage> UploadActivityAsync(
    string token, 
    string base64File, 
    string? title = null, 
    string? note = null, 
    string? route = null, 
    int? elevationUp = null, 
    int? elevationDown = null)
```

**Parameters:**
- `token`: API authentication token
- `base64File`: Base64-encoded activity file (FIT, GPX, etc.)
- `title`: Optional activity title
- `note`: Optional activity notes
- `route`: Optional route information
- `elevationUp`: Optional elevation gain
- `elevationDown`: Optional elevation loss

#### GetActivityUploadStatusAsync
Retrieves the status of an activity upload.

**Signature:**
```csharp
Task<HttpResponseMessage> GetActivityUploadStatusAsync(string token, string id)
```

#### GetActivitiesAsync
Retrieves a collection of activities.

**Signature:**
```csharp
Task<HttpResponseMessage> GetActivitiesAsync(
    string token, 
    int? page = null, 
    string? orderById = null)
```

#### GetActivityAsync
Retrieves a specific activity by ID.

**Signature:**
```csharp
Task<HttpResponseMessage> GetActivityAsync(string token, string id)
```

### Activity Download Endpoints

#### DownloadFitOriginalAsync
Downloads the original FIT file for an activity.

**Signature:**
```csharp
Task<HttpResponseMessage> DownloadFitOriginalAsync(string token, string id)
```

#### DownloadFitlogAsync
Downloads the FITLOG file for an activity.

**Signature:**
```csharp
Task<HttpResponseMessage> DownloadFitlogAsync(string token, string id)
```

#### DownloadGpxAsync
Downloads the GPX file for an activity.

**Signature:**
```csharp
Task<HttpResponseMessage> DownloadGpxAsync(string token, string id)
```

#### DownloadKmlAsync
Downloads the KML file for an activity.

**Signature:**
```csharp
Task<HttpResponseMessage> DownloadKmlAsync(string token, string id)
```

#### DownloadSocialImageAsync
Downloads the social image for an activity.

**Signature:**
```csharp
Task<HttpResponseMessage> DownloadSocialImageAsync(string token, string id)
```

#### DownloadTcxAsync
Downloads the TCX file for an activity.

**Signature:**
```csharp
Task<HttpResponseMessage> DownloadTcxAsync(string token, string id)
```

### Statistics Endpoints

#### GetCurrentStatisticsAsync
Retrieves current user statistics.

**Signature:**
```csharp
Task<HttpResponseMessage> GetCurrentStatisticsAsync(string token)
```

### Equipment Endpoints

#### GetEquipmentAsync
Retrieves a collection of equipment.

**Signature:**
```csharp
Task<HttpResponseMessage> GetEquipmentAsync(
    string token, 
    int? page = null, 
    string? orderById = null)
```

#### GetEquipmentByIdAsync
Retrieves specific equipment by ID.

**Signature:**
```csharp
Task<HttpResponseMessage> GetEquipmentByIdAsync(string token, string id)
```

#### GetEquipmentCategoriesAsync
Retrieves equipment categories.

**Signature:**
```csharp
Task<HttpResponseMessage> GetEquipmentCategoriesAsync(
    string token, 
    int? page = null, 
    string? orderById = null)
```

#### GetEquipmentCategoryByIdAsync
Retrieves specific equipment category by ID.

**Signature:**
```csharp
Task<HttpResponseMessage> GetEquipmentCategoryByIdAsync(string token, string id)
```

### Health Endpoints

#### BulkUploadHealthAsync
Bulk uploads health data from a CSV file.

**Signature:**
```csharp
Task<HttpResponseMessage> BulkUploadHealthAsync(string token, string base64File)
```

### Metrics Endpoints

#### Blood Glucose Metrics
- `GetBloodGlucoseMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateBloodGlucoseMetricAsync(token, metricData)` - Create new metric
- `GetBloodGlucoseMetricByIdAsync(token, id)` - Get by ID

#### Blood Pressure Metrics
- `GetBloodPressureMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateBloodPressureMetricAsync(token, metricData)` - Create new metric
- `GetBloodPressureMetricByIdAsync(token, id)` - Get by ID

#### Body Composition Metrics
- `GetBodyCompositionMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateBodyCompositionMetricAsync(token, metricData)` - Create new metric
- `GetBodyCompositionMetricByIdAsync(token, id)` - Get by ID

#### Body Temperature Metrics
- `GetBodyTemperatureMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateBodyTemperatureMetricAsync(token, metricData)` - Create new metric
- `GetBodyTemperatureMetricByIdAsync(token, id)` - Get by ID

#### Daily Note Metrics
- `GetDailyNoteMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateDailyNoteMetricAsync(token, metricData)` - Create new metric
- `GetDailyNoteByDateAsync(token, date)` - Get by date

#### HRV Metrics
- `GetHrvMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateHrvMetricAsync(token, metricData)` - Create new metric
- `GetHrvMetricByIdAsync(token, id)` - Get by ID

#### Heart Rate Max Metrics
- `GetHeartRateMaxMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateHeartRateMaxMetricAsync(token, metricData)` - Create new metric
- `GetHeartRateMaxMetricByIdAsync(token, id)` - Get by ID

#### Heart Rate Rest Metrics
- `GetHeartRateRestMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateHeartRateRestMetricAsync(token, metricData)` - Create new metric
- `GetHeartRateRestMetricByIdAsync(token, id)` - Get by ID

#### Mental Metrics
- `GetMentalMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateMentalMetricAsync(token, metricData)` - Create new metric
- `GetMentalMetricByDateAsync(token, date)` - Get by date

#### Sleep Metrics
- `GetSleepMetricsAsync(token, page?, orderById?)` - Get collection
- `CreateSleepMetricAsync(token, metricData)` - Create new metric
- `GetSleepMetricByIdAsync(token, id)` - Get by ID

### Race Result Endpoints

#### GetRaceResultsAsync
Retrieves race results.

**Signature:**
```csharp
Task<HttpResponseMessage> GetRaceResultsAsync(
    string token, 
    int? page = null, 
    string? orderById = null)
```

#### GetRaceResultByActivityAsync
Retrieves race result for a specific activity.

**Signature:**
```csharp
Task<HttpResponseMessage> GetRaceResultByActivityAsync(string token, string activityId)
```

### Tag Endpoints

#### GetTagsAsync
Retrieves tags.

**Signature:**
```csharp
Task<HttpResponseMessage> GetTagsAsync(
    string token, 
    int? page = null, 
    string? orderById = null)
```

#### GetTagByIdAsync
Retrieves specific tag by ID.

**Signature:**
```csharp
Task<HttpResponseMessage> GetTagByIdAsync(string token, string id)
```

## Common Parameters

### Pagination Parameters
- `page`: Page number for paginated results (default: 1)
- `orderById`: Sort order for ID field ("asc" or "desc", default: "asc")

### Metric Data Structure
For POST endpoints that create metrics, pass an object with the appropriate properties:
```csharp
var metricData = new { 
    value = 100,           // Metric value
    date = "2023-01-01",   // Date in YYYY-MM-DD format
    // Additional properties as required by specific metric type
};
```

## Authentication

### Token-Based Authentication
The Runalyze API uses token-based authentication via HTTP headers:

```csharp
// Token is automatically added to all requests
var response = await client.GetActivityAsync("your-api-token", "activity-id");
```

### Obtaining API Tokens
1. Log in to your Runalyze account
2. Navigate to Settings > API
3. Generate a new API token
4. Use the token in all API requests

## Rate Limiting and Error Handling

### Rate Limiting
- The Runalyze API may have rate limits
- Implement exponential backoff for retries
- Monitor response headers for rate limit information

### Error Handling
The client does not throw exceptions for HTTP errors. Check the `IsSuccessStatusCode` property of the response to determine success:

```csharp
var response = await client.GetActivityAsync(token, id);
if (response.IsSuccessStatusCode)
{
    var content = await response.Content.ReadAsStringAsync();
    // Process successful response
}
else
{
    // Handle error response
    var errorContent = await response.Content.ReadAsStringAsync();
    var statusCode = response.StatusCode;
}
```

### Common HTTP Status Codes
- `200 OK`: Request successful
- `201 Created`: Resource created successfully
- `400 Bad Request`: Invalid request parameters
- `401 Unauthorized`: Invalid or missing token
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `422 Unprocessable Entity`: Validation errors
- `429 Too Many Requests`: Rate limit exceeded
- `500 Internal Server Error`: Server error

## Performance Optimization

### Connection Management
- Uses `HttpClientFactory` for proper connection lifecycle management
- Automatic connection pooling and reuse
- Configurable timeout settings

### Content Negotiation
- Prefers `application/ld+json` for structured data
- Falls back to `application/json` when LD+JSON not available
- Supports `text/csv` for activity collections

### File Upload Optimization
- Base64 encoding for file uploads
- Multipart form data for efficient transmission
- Support for large activity files

## Security Considerations

### Token Security
- Store tokens securely (environment variables, secure configuration)
- Rotate tokens regularly
- Never log or expose tokens in error messages

### Data Privacy
- All data transmitted over HTTPS
- User consent required for data access
- Compliance with data protection regulations

### Input Validation
- Validate all input parameters before API calls
- Sanitize file uploads
- Check response data for malicious content

## Monitoring and Logging

### Request Logging
```csharp
// Log API requests (without sensitive data)
_logger.LogInformation("Making Runalyze API request: {Endpoint}", endpoint);
```

### Response Monitoring
```csharp
// Monitor response times and success rates
var stopwatch = Stopwatch.StartNew();
var response = await client.GetActivityAsync(token, id);
stopwatch.Stop();

_logger.LogInformation("API request completed in {ElapsedMs}ms with status {StatusCode}", 
    stopwatch.ElapsedMilliseconds, response.StatusCode);
```

### Error Tracking
```csharp
if (!response.IsSuccessStatusCode)
{
    _logger.LogError("Runalyze API error: {StatusCode} - {Content}", 
        response.StatusCode, await response.Content.ReadAsStringAsync());
}
```

## Integration Examples

### Basic Activity Upload
```csharp
public async Task<string> UploadActivityFile(string token, byte[] fileBytes, string title)
{
    var base64File = Convert.ToBase64String(fileBytes);
    var response = await _runalyzeClient.UploadActivityAsync(token, base64File, title);
    
    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();
        // Parse response to get activity ID
        return activityId;
    }
    
    throw new Exception($"Failed to upload activity: {response.StatusCode}");
}
```

### Batch Metric Creation
```csharp
public async Task CreateMultipleMetrics(string token, IEnumerable<MetricData> metrics)
{
    foreach (var metric in metrics)
    {
        var response = await _runalyzeClient.CreateBloodGlucoseMetricAsync(token, metric);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Failed to create metric: {Error}", 
                await response.Content.ReadAsStringAsync());
        }
    }
}
```

### Activity Download Pipeline
```csharp
public async Task<byte[]> DownloadActivityFile(string token, string activityId, string format)
{
    HttpResponseMessage response = format.ToLower() switch
    {
        "fit" => await _runalyzeClient.DownloadFitOriginalAsync(token, activityId),
        "gpx" => await _runalyzeClient.DownloadGpxAsync(token, activityId),
        "tcx" => await _runalyzeClient.DownloadTcxAsync(token, activityId),
        _ => throw new ArgumentException($"Unsupported format: {format}")
    };
    
    if (response.IsSuccessStatusCode)
    {
        return await response.Content.ReadAsByteArrayAsync();
    }
    
    throw new Exception($"Failed to download activity: {response.StatusCode}");
}
``` 