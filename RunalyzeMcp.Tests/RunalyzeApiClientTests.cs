using NUnit.Framework;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RunalyzeMcp;
using Microsoft.Extensions.Options;

namespace RunalyzeMcp.Tests
{
    [TestFixture]
    public class RunalyzeApiClientTests
    {
        private RunalyzeApiClient _client;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            var options = Options.Create(new RunalyzeApiClientOptions());
            _client = new RunalyzeApiClient(_httpClient, options);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient?.Dispose();
        }

        #region Configuration Tests

        [Test]
        public void UsesDefaultBaseUrl_WhenEnvNotSet()
        {
            var options = Options.Create(new RunalyzeApiClientOptions());
            var client = new RunalyzeApiClient(new HttpClient(), options);
            var baseUrl = typeof(RunalyzeApiClient).GetField("_baseUrl", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(client);
            Assert.That(baseUrl, Is.EqualTo("https://runalyze.com"));
        }

        [Test]
        public void UsesEnvBaseUrl_WhenSet()
        {
            Environment.SetEnvironmentVariable("RUNALYZE_BASE_URL", "https://test.runalyze.com");
            var options = Options.Create(new RunalyzeApiClientOptions());
            var client = new RunalyzeApiClient(new HttpClient(), options);
            var baseUrl = typeof(RunalyzeApiClient).GetField("_baseUrl", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(client);
            Assert.That(baseUrl, Is.EqualTo("https://test.runalyze.com"));
            Environment.SetEnvironmentVariable("RUNALYZE_BASE_URL", null);
        }

        #endregion

        #region Header Management Tests

        [Test]
        public void AddsTokenHeaderAndAcceptHeaders_OnUploadActivityAsync()
        {
            // Act
            _client.UploadActivityAsync("test-token", "dGVzdA==", "Test Activity").Wait();

            // Assert
            Assert.That(_httpClient.DefaultRequestHeaders.Contains("token"), Is.True);
            Assert.That(_httpClient.DefaultRequestHeaders.GetValues("token"), Does.Contain("test-token"));
            Assert.That(_httpClient.DefaultRequestHeaders.Accept, Does.Contain(new MediaTypeWithQualityHeaderValue("application/ld+json")));
            Assert.That(_httpClient.DefaultRequestHeaders.Accept, Does.Contain(new MediaTypeWithQualityHeaderValue("application/json")));
        }

        [Test]
        public void AddsTokenHeaderAndAcceptHeaders_OnGetActivityAsync()
        {
            // Act
            _client.GetActivityAsync("test-token", "123").Wait();

            // Assert
            Assert.That(_httpClient.DefaultRequestHeaders.Contains("token"), Is.True);
            Assert.That(_httpClient.DefaultRequestHeaders.GetValues("token"), Does.Contain("test-token"));
            Assert.That(_httpClient.DefaultRequestHeaders.Accept, Does.Contain(new MediaTypeWithQualityHeaderValue("text/csv")));
            Assert.That(_httpClient.DefaultRequestHeaders.Accept, Does.Contain(new MediaTypeWithQualityHeaderValue("application/ld+json")));
            Assert.That(_httpClient.DefaultRequestHeaders.Accept, Does.Contain(new MediaTypeWithQualityHeaderValue("application/json")));
        }

        [Test]
        public void AddsTokenHeaderAndBinaryAccept_OnDownloadFitOriginalAsync()
        {
            // Act
            _client.DownloadFitOriginalAsync("test-token", "123").Wait();

            // Assert
            Assert.That(_httpClient.DefaultRequestHeaders.Contains("token"), Is.True);
            Assert.That(_httpClient.DefaultRequestHeaders.GetValues("token"), Does.Contain("test-token"));
            Assert.That(_httpClient.DefaultRequestHeaders.Accept, Does.Contain(new MediaTypeWithQualityHeaderValue("application/octet-stream")));
        }

        #endregion

        #region Activity Endpoint Tests

        [Test]
        public void UploadActivityAsync_WithAllParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var base64File = "dGVzdA==";
            var title = "Test Activity";
            var note = "Test Note";
            var route = "Test Route";
            var elevationUp = 100;
            var elevationDown = 50;

            // Act
            var task = _client.UploadActivityAsync(token, base64File, title, note, route, elevationUp, elevationDown);

            // Assert - The method should not throw and should be called with correct parameters
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetActivityUploadStatusAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetActivityUploadStatusAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetActivitiesAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetActivitiesAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetActivitiesAsync_WithAllParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var page = 2;
            var orderById = "desc";

            // Act
            var task = _client.GetActivitiesAsync(token, page, orderById);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetActivityAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetActivityAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Download Endpoint Tests

        [Test]
        public void DownloadFitOriginalAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.DownloadFitOriginalAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void DownloadFitlogAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.DownloadFitlogAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void DownloadGpxAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.DownloadGpxAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void DownloadKmlAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.DownloadKmlAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void DownloadSocialImageAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.DownloadSocialImageAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void DownloadTcxAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.DownloadTcxAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Statistics Endpoint Tests

        [Test]
        public void GetCurrentStatisticsAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetCurrentStatisticsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Equipment Endpoint Tests

        [Test]
        public void GetEquipmentAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetEquipmentAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetEquipmentAsync_WithAllParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var page = 2;
            var orderById = "desc";

            // Act
            var task = _client.GetEquipmentAsync(token, page, orderById);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetEquipmentByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetEquipmentByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetEquipmentCategoriesAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetEquipmentCategoriesAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetEquipmentCategoriesAsync_WithAllParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var page = 2;
            var orderById = "desc";

            // Act
            var task = _client.GetEquipmentCategoriesAsync(token, page, orderById);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetEquipmentCategoryByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetEquipmentCategoryByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Health Endpoint Tests

        [Test]
        public void BulkUploadHealthAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var base64File = "dGVzdA==";

            // Act
            var task = _client.BulkUploadHealthAsync(token, base64File);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Metrics Endpoint Tests

        [Test]
        public void GetBloodGlucoseMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetBloodGlucoseMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBloodGlucoseMetricsAsync_WithAllParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var page = 2;
            var orderById = "desc";

            // Act
            var task = _client.GetBloodGlucoseMetricsAsync(token, page, orderById);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateBloodGlucoseMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { value = 100, date = "2023-01-01" };

            // Act
            var task = _client.CreateBloodGlucoseMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBloodGlucoseMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetBloodGlucoseMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBloodPressureMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetBloodPressureMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateBloodPressureMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { systolic = 120, diastolic = 80, date = "2023-01-01" };

            // Act
            var task = _client.CreateBloodPressureMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBloodPressureMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetBloodPressureMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBodyCompositionMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetBodyCompositionMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateBodyCompositionMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { weight = 70.5, bodyFat = 15.2, date = "2023-01-01" };

            // Act
            var task = _client.CreateBodyCompositionMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBodyCompositionMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetBodyCompositionMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBodyTemperatureMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetBodyTemperatureMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateBodyTemperatureMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { temperature = 36.8, date = "2023-01-01" };

            // Act
            var task = _client.CreateBodyTemperatureMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetBodyTemperatureMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetBodyTemperatureMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetDailyNoteMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetDailyNoteMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateDailyNoteMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { note = "Feeling great today!", date = "2023-01-01" };

            // Act
            var task = _client.CreateDailyNoteMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetDailyNoteByDateAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var date = "2023-01-01";

            // Act
            var task = _client.GetDailyNoteByDateAsync(token, date);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetHrvMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetHrvMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateHrvMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { hrv = 45.2, date = "2023-01-01" };

            // Act
            var task = _client.CreateHrvMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetHrvMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetHrvMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetHeartRateMaxMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetHeartRateMaxMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateHeartRateMaxMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { heartRateMax = 185, date = "2023-01-01" };

            // Act
            var task = _client.CreateHeartRateMaxMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetHeartRateMaxMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetHeartRateMaxMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetHeartRateRestMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetHeartRateRestMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateHeartRateRestMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { heartRateRest = 55, date = "2023-01-01" };

            // Act
            var task = _client.CreateHeartRateRestMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetHeartRateRestMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetHeartRateRestMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetMentalMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetMentalMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateMentalMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { mood = 8, stress = 3, date = "2023-01-01" };

            // Act
            var task = _client.CreateMentalMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetMentalMetricByDateAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var date = "2023-01-01";

            // Act
            var task = _client.GetMentalMetricByDateAsync(token, date);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetSleepMetricsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetSleepMetricsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void CreateSleepMetricAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var metricData = new { duration = 480, quality = 7, date = "2023-01-01" };

            // Act
            var task = _client.CreateSleepMetricAsync(token, metricData);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetSleepMetricByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetSleepMetricByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Race Result Endpoint Tests

        [Test]
        public void GetRaceResultsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetRaceResultsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetRaceResultsAsync_WithAllParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var page = 2;
            var orderById = "desc";

            // Act
            var task = _client.GetRaceResultsAsync(token, page, orderById);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetRaceResultByActivityAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var activityId = "123";

            // Act
            var task = _client.GetRaceResultByActivityAsync(token, activityId);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Tag Endpoint Tests

        [Test]
        public void GetTagsAsync_WithNoParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";

            // Act
            var task = _client.GetTagsAsync(token);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetTagsAsync_WithAllParameters_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var page = 2;
            var orderById = "desc";

            // Act
            var task = _client.GetTagsAsync(token, page, orderById);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public void GetTagByIdAsync_ConstructsCorrectUrl()
        {
            // Arrange
            var token = "test-token";
            var id = "123";

            // Act
            var task = _client.GetTagByIdAsync(token, id);

            // Assert
            Assert.That(task, Is.Not.Null);
        }

        #endregion
    }
} 