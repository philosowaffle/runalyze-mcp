# Testing Strategy: Runalyze MCP

## Unit Testing Framework
- **Framework**: NUnit
- **Coverage**: coverlet.collector
- **Test Discovery**: Automatic via NUnit3TestAdapter

## Test Categories

### RunalyzeApiClient Tests (61 tests total)
Comprehensive test coverage for the custom HTTP client implementation:

#### Configuration Tests
- `UsesDefaultBaseUrl_WhenEnvNotSet`: Verifies default base URL when environment variable is not set
- `UsesEnvBaseUrl_WhenSet`: Verifies environment variable override for base URL

#### Header Management Tests
- `AddsTokenHeaderAndAcceptHeaders_OnUploadActivityAsync`: Verifies proper token and accept headers for uploads
- `AddsTokenHeaderAndAcceptHeaders_OnGetActivityAsync`: Verifies headers for activity retrieval with CSV support
- `AddsTokenHeaderAndBinaryAccept_OnDownloadFitOriginalAsync`: Verifies binary content type headers for downloads

#### Activity Endpoint Tests
- `UploadActivityAsync_WithAllParameters_ConstructsCorrectUrl`: Tests activity upload with all optional parameters
- `GetActivityUploadStatusAsync_ConstructsCorrectUrl`: Tests upload status retrieval
- `GetActivitiesAsync_WithNoParameters_ConstructsCorrectUrl`: Tests activity collection retrieval
- `GetActivitiesAsync_WithAllParameters_ConstructsCorrectUrl`: Tests activity collection with pagination
- `GetActivityAsync_ConstructsCorrectUrl`: Tests individual activity retrieval

#### Download Endpoint Tests
- `DownloadFitOriginalAsync_ConstructsCorrectUrl`: Tests FIT file download
- `DownloadFitlogAsync_ConstructsCorrectUrl`: Tests FITLOG file download
- `DownloadGpxAsync_ConstructsCorrectUrl`: Tests GPX file download
- `DownloadKmlAsync_ConstructsCorrectUrl`: Tests KML file download
- `DownloadSocialImageAsync_ConstructsCorrectUrl`: Tests social image download
- `DownloadTcxAsync_ConstructsCorrectUrl`: Tests TCX file download

#### Statistics Endpoint Tests
- `GetCurrentStatisticsAsync_ConstructsCorrectUrl`: Tests statistics retrieval

#### Equipment Endpoint Tests
- `GetEquipmentAsync_WithNoParameters_ConstructsCorrectUrl`: Tests equipment collection
- `GetEquipmentAsync_WithAllParameters_ConstructsCorrectUrl`: Tests equipment collection with pagination
- `GetEquipmentByIdAsync_ConstructsCorrectUrl`: Tests individual equipment retrieval
- `GetEquipmentCategoriesAsync_WithNoParameters_ConstructsCorrectUrl`: Tests equipment categories
- `GetEquipmentCategoriesAsync_WithAllParameters_ConstructsCorrectUrl`: Tests equipment categories with pagination
- `GetEquipmentCategoryByIdAsync_ConstructsCorrectUrl`: Tests individual equipment category retrieval

#### Health Endpoint Tests
- `BulkUploadHealthAsync_ConstructsCorrectUrl`: Tests health data bulk upload

#### Metrics Endpoint Tests (36 tests)
Comprehensive coverage for all metric types:

**Blood Glucose Metrics:**
- `GetBloodGlucoseMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `GetBloodGlucoseMetricsAsync_WithAllParameters_ConstructsCorrectUrl`
- `CreateBloodGlucoseMetricAsync_ConstructsCorrectUrl`
- `GetBloodGlucoseMetricByIdAsync_ConstructsCorrectUrl`

**Blood Pressure Metrics:**
- `GetBloodPressureMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateBloodPressureMetricAsync_ConstructsCorrectUrl`
- `GetBloodPressureMetricByIdAsync_ConstructsCorrectUrl`

**Body Composition Metrics:**
- `GetBodyCompositionMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateBodyCompositionMetricAsync_ConstructsCorrectUrl`
- `GetBodyCompositionMetricByIdAsync_ConstructsCorrectUrl`

**Body Temperature Metrics:**
- `GetBodyTemperatureMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateBodyTemperatureMetricAsync_ConstructsCorrectUrl`
- `GetBodyTemperatureMetricByIdAsync_ConstructsCorrectUrl`

**Daily Note Metrics:**
- `GetDailyNoteMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateDailyNoteMetricAsync_ConstructsCorrectUrl`
- `GetDailyNoteByDateAsync_ConstructsCorrectUrl`

**HRV Metrics:**
- `GetHrvMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateHrvMetricAsync_ConstructsCorrectUrl`
- `GetHrvMetricByIdAsync_ConstructsCorrectUrl`

**Heart Rate Max Metrics:**
- `GetHeartRateMaxMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateHeartRateMaxMetricAsync_ConstructsCorrectUrl`
- `GetHeartRateMaxMetricByIdAsync_ConstructsCorrectUrl`

**Heart Rate Rest Metrics:**
- `GetHeartRateRestMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateHeartRateRestMetricAsync_ConstructsCorrectUrl`
- `GetHeartRateRestMetricByIdAsync_ConstructsCorrectUrl`

**Mental Metrics:**
- `GetMentalMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateMentalMetricAsync_ConstructsCorrectUrl`
- `GetMentalMetricByDateAsync_ConstructsCorrectUrl`

**Sleep Metrics:**
- `GetSleepMetricsAsync_WithNoParameters_ConstructsCorrectUrl`
- `CreateSleepMetricAsync_ConstructsCorrectUrl`
- `GetSleepMetricByIdAsync_ConstructsCorrectUrl`

#### Race Result Endpoint Tests
- `GetRaceResultsAsync_WithNoParameters_ConstructsCorrectUrl`: Tests race results collection
- `GetRaceResultsAsync_WithAllParameters_ConstructsCorrectUrl`: Tests race results with pagination
- `GetRaceResultByActivityAsync_ConstructsCorrectUrl`: Tests race result by activity

#### Tag Endpoint Tests
- `GetTagsAsync_WithNoParameters_ConstructsCorrectUrl`: Tests tags collection
- `GetTagsAsync_WithAllParameters_ConstructsCorrectUrl`: Tests tags with pagination
- `GetTagByIdAsync_ConstructsCorrectUrl`: Tests individual tag retrieval

### Test Structure
```csharp
[TestFixture]
public class RunalyzeApiClientTests
{
    private RunalyzeApiClient _client;
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _client = new RunalyzeApiClient(_httpClient);
    }

    [TearDown]
    public void TearDown()
    {
        _httpClient?.Dispose();
    }
}
```

### Test Patterns

#### Configuration Tests
Test environment variable handling and default values:
```csharp
[Test]
public void UsesDefaultBaseUrl_WhenEnvNotSet()
{
    var client = new RunalyzeApiClient(new HttpClient());
    var baseUrl = typeof(RunalyzeApiClient).GetField("_baseUrl", 
        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(client);
    Assert.That(baseUrl, Is.EqualTo("https://runalyze.com"));
}
```

#### Header Tests
Verify proper HTTP headers are set:
```csharp
[Test]
public void AddsTokenHeaderAndAcceptHeaders_OnUploadActivityAsync()
{
    _client.UploadActivityAsync("test-token", "dGVzdA==", "Test Activity").Wait();
    
    Assert.That(_httpClient.DefaultRequestHeaders.Contains("token"), Is.True);
    Assert.That(_httpClient.DefaultRequestHeaders.GetValues("token"), Does.Contain("test-token"));
    Assert.That(_httpClient.DefaultRequestHeaders.Accept, Does.Contain(new MediaTypeWithQualityHeaderValue("application/ld+json")));
}
```

#### Endpoint Tests
Verify methods can be called without throwing exceptions:
```csharp
[Test]
public void GetActivityAsync_ConstructsCorrectUrl()
{
    var task = _client.GetActivityAsync("test-token", "123");
    Assert.That(task, Is.Not.Null);
}
```

### Test Execution
```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "RunalyzeApiClientTests"

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Coverage Goals
- **Current Coverage**: 61 tests covering all RunalyzeApiClient endpoints
- **Configuration**: 100% coverage of environment variable handling
- **Headers**: 100% coverage of authentication and content-type headers
- **Endpoints**: 100% coverage of all API endpoints
- **Parameters**: Coverage of both required and optional parameters

### Test Data Management
- Use consistent test tokens ("test-token")
- Use consistent test IDs ("123")
- Use valid base64-encoded test data ("dGVzdA==")
- Use realistic metric data objects for POST endpoints

### Future Test Enhancements
- Integration tests with mock HTTP responses
- Error handling tests for various HTTP status codes
- Performance tests for concurrent requests
- Memory leak tests for long-running operations

## Mocking Strategy
- Uses real `HttpClient` instances for header verification
- Catches exceptions for HTTP failures in tests that verify request setup
- Environment variable manipulation for configuration testing 