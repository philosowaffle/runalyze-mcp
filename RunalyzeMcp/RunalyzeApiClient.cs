using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RunalyzeMcp
{
    public class RunalyzeApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RunalyzeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = Environment.GetEnvironmentVariable("RUNALYZE_BASE_URL") ?? "https://runalyze.com";
        }

        private void AddTokenHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Remove("token");
            _httpClient.DefaultRequestHeaders.Add("token", token);
        }

        private void AddAcceptHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ld+json"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void AddAcceptHeadersForCsv()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ld+json"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Activity Endpoints

        /// <summary>
        /// Upload activity file (POST /api/v1/activities/uploads)
        /// </summary>
        public async Task<HttpResponseMessage> UploadActivityAsync(string token, string base64File, string? title = null, string? note = null, string? route = null, int? elevationUp = null, int? elevationDown = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/activities/uploads";
            using var content = new MultipartFormDataContent();
            var fileBytes = Convert.FromBase64String(base64File);
            content.Add(new ByteArrayContent(fileBytes), "file", "activity.fit");
            if (!string.IsNullOrEmpty(title)) content.Add(new StringContent(title), "title");
            if (!string.IsNullOrEmpty(note)) content.Add(new StringContent(note), "note");
            if (!string.IsNullOrEmpty(route)) content.Add(new StringContent(route), "route");
            if (elevationUp.HasValue) content.Add(new StringContent(elevationUp.Value.ToString()), "elevation_up_file");
            if (elevationDown.HasValue) content.Add(new StringContent(elevationDown.Value.ToString()), "elevation_down_file");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get activity upload status (GET /api/v1/activities/uploads/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetActivityUploadStatusAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/activities/uploads/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get activity collection (GET /api/v1/activity)
        /// </summary>
        public async Task<HttpResponseMessage> GetActivitiesAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeadersForCsv();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/activity";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get specific activity (GET /api/v1/activity/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetActivityAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeadersForCsv();

            var url = $"{_baseUrl}/api/v1/activity/{id}";
            return await _httpClient.GetAsync(url);
        }

        #endregion

        #region Activity Download Endpoints

        /// <summary>
        /// Download original FIT file (GET /api/v1/activity/{id}/fit)
        /// </summary>
        public async Task<HttpResponseMessage> DownloadFitOriginalAsync(string token, string id)
        {
            AddTokenHeader(token);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

            var url = $"{_baseUrl}/api/v1/activity/{id}/fit";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Download FITLOG file (GET /api/v1/activity/{id}/fitlog)
        /// </summary>
        public async Task<HttpResponseMessage> DownloadFitlogAsync(string token, string id)
        {
            AddTokenHeader(token);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

            var url = $"{_baseUrl}/api/v1/activity/{id}/fitlog";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Download GPX file (GET /api/v1/activity/{id}/gpx)
        /// </summary>
        public async Task<HttpResponseMessage> DownloadGpxAsync(string token, string id)
        {
            AddTokenHeader(token);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/gpx+xml"));

            var url = $"{_baseUrl}/api/v1/activity/{id}/gpx";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Download KML file (GET /api/v1/activity/{id}/kml)
        /// </summary>
        public async Task<HttpResponseMessage> DownloadKmlAsync(string token, string id)
        {
            AddTokenHeader(token);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.google-earth.kml+xml"));

            var url = $"{_baseUrl}/api/v1/activity/{id}/kml";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Download social image (GET /api/v1/activity/{id}/social-image)
        /// </summary>
        public async Task<HttpResponseMessage> DownloadSocialImageAsync(string token, string id)
        {
            AddTokenHeader(token);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/png"));

            var url = $"{_baseUrl}/api/v1/activity/{id}/social-image";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Download TCX file (GET /api/v1/activity/{id}/tcx)
        /// </summary>
        public async Task<HttpResponseMessage> DownloadTcxAsync(string token, string id)
        {
            AddTokenHeader(token);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.garmin.tcx+xml"));

            var url = $"{_baseUrl}/api/v1/activity/{id}/tcx";
            return await _httpClient.GetAsync(url);
        }

        #endregion

        #region Statistics Endpoints

        /// <summary>
        /// Get current statistics (GET /api/v1/statistics/current)
        /// </summary>
        public async Task<HttpResponseMessage> GetCurrentStatisticsAsync(string token)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/statistics/current";
            return await _httpClient.GetAsync(url);
        }

        #endregion

        #region Equipment Endpoints

        /// <summary>
        /// Get equipment collection (GET /api/v1/equipment)
        /// </summary>
        public async Task<HttpResponseMessage> GetEquipmentAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/equipment";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get specific equipment (GET /api/v1/equipment/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetEquipmentByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/equipment/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get equipment categories (GET /api/v1/equipment/category)
        /// </summary>
        public async Task<HttpResponseMessage> GetEquipmentCategoriesAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/equipment/category";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get specific equipment category (GET /api/v1/equipment/category/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetEquipmentCategoryByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/equipment/category/{id}";
            return await _httpClient.GetAsync(url);
        }

        #endregion

        #region Health Endpoints

        /// <summary>
        /// Bulk upload health data (POST /api/v1/health/bulk-upload)
        /// </summary>
        public async Task<HttpResponseMessage> BulkUploadHealthAsync(string token, string base64File)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/health/bulk-upload";
            using var content = new MultipartFormDataContent();
            var fileBytes = Convert.FromBase64String(base64File);
            content.Add(new ByteArrayContent(fileBytes), "file", "health.csv");

            return await _httpClient.PostAsync(url, content);
        }

        #endregion

        #region Metrics Endpoints

        /// <summary>
        /// Get blood glucose metrics (GET /api/v1/metrics/blood-glucose)
        /// </summary>
        public async Task<HttpResponseMessage> GetBloodGlucoseMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/blood-glucose";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create blood glucose metric (POST /api/v1/metrics/blood-glucose)
        /// </summary>
        public async Task<HttpResponseMessage> CreateBloodGlucoseMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/blood-glucose";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific blood glucose metric (GET /api/v1/metrics/blood-glucose/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetBloodGlucoseMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/blood-glucose/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get blood pressure metrics (GET /api/v1/metrics/blood-pressure)
        /// </summary>
        public async Task<HttpResponseMessage> GetBloodPressureMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/blood-pressure";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create blood pressure metric (POST /api/v1/metrics/blood-pressure)
        /// </summary>
        public async Task<HttpResponseMessage> CreateBloodPressureMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/blood-pressure";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific blood pressure metric (GET /api/v1/metrics/blood-pressure/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetBloodPressureMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/blood-pressure/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get body composition metrics (GET /api/v1/metrics/body-composition)
        /// </summary>
        public async Task<HttpResponseMessage> GetBodyCompositionMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/body-composition";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create body composition metric (POST /api/v1/metrics/body-composition)
        /// </summary>
        public async Task<HttpResponseMessage> CreateBodyCompositionMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/body-composition";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific body composition metric (GET /api/v1/metrics/body-composition/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetBodyCompositionMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/body-composition/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get body temperature metrics (GET /api/v1/metrics/body-temperature)
        /// </summary>
        public async Task<HttpResponseMessage> GetBodyTemperatureMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/body-temperature";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create body temperature metric (POST /api/v1/metrics/body-temperature)
        /// </summary>
        public async Task<HttpResponseMessage> CreateBodyTemperatureMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/body-temperature";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific body temperature metric (GET /api/v1/metrics/body-temperature/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetBodyTemperatureMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/body-temperature/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get daily note metrics (GET /api/v1/metrics/daily-note)
        /// </summary>
        public async Task<HttpResponseMessage> GetDailyNoteMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/daily-note";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create daily note metric (POST /api/v1/metrics/daily-note)
        /// </summary>
        public async Task<HttpResponseMessage> CreateDailyNoteMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/daily-note";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get daily note by date (GET /api/v1/metrics/daily-note/{date})
        /// </summary>
        public async Task<HttpResponseMessage> GetDailyNoteByDateAsync(string token, string date)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/daily-note/{date}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get HRV metrics (GET /api/v1/metrics/hrv)
        /// </summary>
        public async Task<HttpResponseMessage> GetHrvMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/hrv";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create HRV metric (POST /api/v1/metrics/hrv)
        /// </summary>
        public async Task<HttpResponseMessage> CreateHrvMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/hrv";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific HRV metric (GET /api/v1/metrics/hrv/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetHrvMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/hrv/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get heart rate max metrics (GET /api/v1/metrics/heart-rate-max)
        /// </summary>
        public async Task<HttpResponseMessage> GetHeartRateMaxMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/heart-rate-max";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create heart rate max metric (POST /api/v1/metrics/heart-rate-max)
        /// </summary>
        public async Task<HttpResponseMessage> CreateHeartRateMaxMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/heart-rate-max";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific heart rate max metric (GET /api/v1/metrics/heart-rate-max/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetHeartRateMaxMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/heart-rate-max/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get heart rate rest metrics (GET /api/v1/metrics/heart-rate-rest)
        /// </summary>
        public async Task<HttpResponseMessage> GetHeartRateRestMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/heart-rate-rest";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create heart rate rest metric (POST /api/v1/metrics/heart-rate-rest)
        /// </summary>
        public async Task<HttpResponseMessage> CreateHeartRateRestMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/heart-rate-rest";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific heart rate rest metric (GET /api/v1/metrics/heart-rate-rest/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetHeartRateRestMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/heart-rate-rest/{id}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get mental metrics (GET /api/v1/metrics/mental)
        /// </summary>
        public async Task<HttpResponseMessage> GetMentalMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/mental";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create mental metric (POST /api/v1/metrics/mental)
        /// </summary>
        public async Task<HttpResponseMessage> CreateMentalMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/mental";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get mental metric by date (GET /api/v1/metrics/mental/{date})
        /// </summary>
        public async Task<HttpResponseMessage> GetMentalMetricByDateAsync(string token, string date)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/mental/{date}";
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get sleep metrics (GET /api/v1/metrics/sleep)
        /// </summary>
        public async Task<HttpResponseMessage> GetSleepMetricsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/metrics/sleep";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Create sleep metric (POST /api/v1/metrics/sleep)
        /// </summary>
        public async Task<HttpResponseMessage> CreateSleepMetricAsync(string token, object metricData)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/sleep";
            var json = JsonSerializer.Serialize(metricData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Get specific sleep metric (GET /api/v1/metrics/sleep/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetSleepMetricByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/metrics/sleep/{id}";
            return await _httpClient.GetAsync(url);
        }

        #endregion

        #region Race Result Endpoints

        /// <summary>
        /// Get race results (GET /api/v1/race-result)
        /// </summary>
        public async Task<HttpResponseMessage> GetRaceResultsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/race-result";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get race result by activity (GET /api/v1/race-result/activity/{activityId})
        /// </summary>
        public async Task<HttpResponseMessage> GetRaceResultByActivityAsync(string token, string activityId)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/race-result/activity/{activityId}";
            return await _httpClient.GetAsync(url);
        }

        #endregion

        #region Tag Endpoints

        /// <summary>
        /// Get tags (GET /api/v1/tag)
        /// </summary>
        public async Task<HttpResponseMessage> GetTagsAsync(string token, int? page = null, string? orderById = null)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var queryParams = new List<string>();
            if (page.HasValue) queryParams.Add($"page={page.Value}");
            if (!string.IsNullOrEmpty(orderById)) queryParams.Add($"order[id]={orderById}");

            var url = $"{_baseUrl}/api/v1/tag";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Get specific tag (GET /api/v1/tag/{id})
        /// </summary>
        public async Task<HttpResponseMessage> GetTagByIdAsync(string token, string id)
        {
            AddTokenHeader(token);
            AddAcceptHeaders();

            var url = $"{_baseUrl}/api/v1/tag/{id}";
            return await _httpClient.GetAsync(url);
        }

        #endregion
    }
} 