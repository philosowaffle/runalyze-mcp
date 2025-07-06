using ModelContextProtocol;
using ModelContextProtocol.Protocol;
using System.Text.Json;
using System.Net.Http;

namespace RunalyzeMcp
{
    public static class McpToolHandler
    {
        public static async Task<CallToolResult> HandleToolCallAsync(RunalyzeApiClient apiClient, string? toolName, IReadOnlyDictionary<string, JsonElement> arguments, CancellationToken cancellationToken)
        {
            if (!arguments.TryGetValue("token", out JsonElement tokenObj) || tokenObj.ValueKind != JsonValueKind.String)
            {
                throw new McpException("Missing required 'token' parameter");
            }
            var token = tokenObj.GetString()!;

            try
            {
                HttpResponseMessage response;
                switch (toolName)
                {
                    case "api_activity_upload":
                        if (!arguments.TryGetValue("file", out JsonElement fileObj) || fileObj.ValueKind != JsonValueKind.String ||
                            !arguments.TryGetValue("filename", out JsonElement filenameObj) || filenameObj.ValueKind != JsonValueKind.String)
                        {
                            throw new McpException("Missing required 'file' or 'filename' parameter");
                        }
                        response = await apiClient.UploadActivityAsync(token, fileObj.GetString()!, filenameObj.GetString());
                        break;

                    case "api_activity_download":
                        if (!arguments.TryGetValue("id", out JsonElement downloadIdObj) || downloadIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.DownloadFitOriginalAsync(token, downloadIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activitiesuploads_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement uploadIdObj) || uploadIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetActivityUploadStatusAsync(token, uploadIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activities_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement activityIdObj) || activityIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetActivityAsync(token, activityIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activities_get":
                        int? page = null;
                        if (arguments.TryGetValue("limit", out JsonElement pageObj) && pageObj.ValueKind == JsonValueKind.Number)
                            page = pageObj.GetInt32();
                        response = await apiClient.GetActivitiesAsync(token, page);
                        break;

                    case "api_v1statistics_get":
                        response = await apiClient.GetCurrentStatisticsAsync(token);
                        break;

                    case "api_v1equipment_get":
                        response = await apiClient.GetEquipmentAsync(token);
                        break;

                    case "api_v1health_get":
                        response = await apiClient.GetBloodGlucoseMetricsAsync(token);
                        break;

                    case "api_v1metrics_get":
                        response = await apiClient.GetBloodGlucoseMetricsAsync(token);
                        break;

                    case "api_v1metrics_blood_glucose_get":
                        int? bgPage = null;
                        string? bgOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement bgPageObj) && bgPageObj.ValueKind == JsonValueKind.Number)
                            bgPage = bgPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement bgOrderObj) && bgOrderObj.ValueKind == JsonValueKind.String)
                            bgOrderById = bgOrderObj.GetString();
                        response = await apiClient.GetBloodGlucoseMetricsAsync(token, bgPage, bgOrderById);
                        break;

                    case "api_v1raceresults_get":
                        response = await apiClient.GetRaceResultsAsync(token);
                        break;

                    case "api_v1tags_get":
                        response = await apiClient.GetTagsAsync(token);
                        break;

                    // Download Endpoints (6 tools)
                    case "api_v1activity_id_fit_get":
                        if (!arguments.TryGetValue("id", out JsonElement fitIdObj) || fitIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.DownloadFitOriginalAsync(token, fitIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activity_id_fitlog_get":
                        if (!arguments.TryGetValue("id", out JsonElement fitlogIdObj) || fitlogIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.DownloadFitlogAsync(token, fitlogIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activity_id_gpx_get":
                        if (!arguments.TryGetValue("id", out JsonElement gpxIdObj) || gpxIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.DownloadGpxAsync(token, gpxIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activity_id_kml_get":
                        if (!arguments.TryGetValue("id", out JsonElement kmlIdObj) || kmlIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.DownloadKmlAsync(token, kmlIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activity_id_social_image_get":
                        if (!arguments.TryGetValue("id", out JsonElement socialImageIdObj) || socialImageIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.DownloadSocialImageAsync(token, socialImageIdObj.GetInt32().ToString());
                        break;

                    case "api_v1activity_id_tcx_get":
                        if (!arguments.TryGetValue("id", out JsonElement tcxIdObj) || tcxIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.DownloadTcxAsync(token, tcxIdObj.GetInt32().ToString());
                        break;

                    // Equipment Endpoints (3 additional tools)
                    case "api_v1equipment_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement equipmentIdObj) || equipmentIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetEquipmentByIdAsync(token, equipmentIdObj.GetInt32().ToString());
                        break;

                    case "api_v1equipment_category_get":
                        int? equipmentPage = null;
                        string? equipmentOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement equipmentPageObj) && equipmentPageObj.ValueKind == JsonValueKind.Number)
                            equipmentPage = equipmentPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement equipmentOrderObj) && equipmentOrderObj.ValueKind == JsonValueKind.String)
                            equipmentOrderById = equipmentOrderObj.GetString();
                        response = await apiClient.GetEquipmentCategoriesAsync(token, equipmentPage, equipmentOrderById);
                        break;

                    case "api_v1equipment_category_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement categoryIdObj) || categoryIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetEquipmentCategoryByIdAsync(token, categoryIdObj.GetInt32().ToString());
                        break;

                    // Health Endpoints (1 tool)
                    case "api_v1health_bulk_upload_post":
                        if (!arguments.TryGetValue("file", out JsonElement healthFileObj) || healthFileObj.ValueKind != JsonValueKind.String)
                        {
                            throw new McpException("Missing required 'file' parameter");
                        }
                        response = await apiClient.BulkUploadHealthAsync(token, healthFileObj.GetString()!);
                        break;

                    // Blood Glucose Metrics (2 additional tools)
                    case "api_v1metrics_blood_glucose_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement bgMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateBloodGlucoseMetricAsync(token, bgMetricDataObj);
                        break;

                    case "api_v1metrics_blood_glucose_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement bgIdObj) || bgIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetBloodGlucoseMetricByIdAsync(token, bgIdObj.GetInt32().ToString());
                        break;

                    // Blood Pressure Metrics (3 tools)
                    case "api_v1metrics_blood_pressure_get":
                        int? bpPage = null;
                        string? bpOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement bpPageObj) && bpPageObj.ValueKind == JsonValueKind.Number)
                            bpPage = bpPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement bpOrderObj) && bpOrderObj.ValueKind == JsonValueKind.String)
                            bpOrderById = bpOrderObj.GetString();
                        response = await apiClient.GetBloodPressureMetricsAsync(token, bpPage, bpOrderById);
                        break;

                    case "api_v1metrics_blood_pressure_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement bpMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateBloodPressureMetricAsync(token, bpMetricDataObj);
                        break;

                    case "api_v1metrics_blood_pressure_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement bpIdObj) || bpIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetBloodPressureMetricByIdAsync(token, bpIdObj.GetInt32().ToString());
                        break;

                    // Body Composition Metrics (3 tools)
                    case "api_v1metrics_body_composition_get":
                        int? bcPage = null;
                        string? bcOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement bcPageObj) && bcPageObj.ValueKind == JsonValueKind.Number)
                            bcPage = bcPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement bcOrderObj) && bcOrderObj.ValueKind == JsonValueKind.String)
                            bcOrderById = bcOrderObj.GetString();
                        response = await apiClient.GetBodyCompositionMetricsAsync(token, bcPage, bcOrderById);
                        break;

                    case "api_v1metrics_body_composition_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement bcMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateBodyCompositionMetricAsync(token, bcMetricDataObj);
                        break;

                    case "api_v1metrics_body_composition_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement bcIdObj) || bcIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetBodyCompositionMetricByIdAsync(token, bcIdObj.GetInt32().ToString());
                        break;

                    // Body Temperature Metrics (3 tools)
                    case "api_v1metrics_body_temperature_get":
                        int? btPage = null;
                        string? btOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement btPageObj) && btPageObj.ValueKind == JsonValueKind.Number)
                            btPage = btPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement btOrderObj) && btOrderObj.ValueKind == JsonValueKind.String)
                            btOrderById = btOrderObj.GetString();
                        response = await apiClient.GetBodyTemperatureMetricsAsync(token, btPage, btOrderById);
                        break;

                    case "api_v1metrics_body_temperature_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement btMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateBodyTemperatureMetricAsync(token, btMetricDataObj);
                        break;

                    case "api_v1metrics_body_temperature_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement btIdObj) || btIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetBodyTemperatureMetricByIdAsync(token, btIdObj.GetInt32().ToString());
                        break;

                    // Daily Note Metrics (3 tools)
                    case "api_v1metrics_daily_note_get":
                        int? dnPage = null;
                        string? dnOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement dnPageObj) && dnPageObj.ValueKind == JsonValueKind.Number)
                            dnPage = dnPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement dnOrderObj) && dnOrderObj.ValueKind == JsonValueKind.String)
                            dnOrderById = dnOrderObj.GetString();
                        response = await apiClient.GetDailyNoteMetricsAsync(token, dnPage, dnOrderById);
                        break;

                    case "api_v1metrics_daily_note_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement dnMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateDailyNoteMetricAsync(token, dnMetricDataObj);
                        break;

                    case "api_v1metrics_daily_note_date_get":
                        if (!arguments.TryGetValue("date", out JsonElement dateObj) || dateObj.ValueKind != JsonValueKind.String)
                        {
                            throw new McpException("Missing required 'date' parameter");
                        }
                        response = await apiClient.GetDailyNoteByDateAsync(token, dateObj.GetString()!);
                        break;

                    // HRV Metrics (3 tools)
                    case "api_v1metrics_hrv_get":
                        int? hrvPage = null;
                        string? hrvOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement hrvPageObj) && hrvPageObj.ValueKind == JsonValueKind.Number)
                            hrvPage = hrvPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement hrvOrderObj) && hrvOrderObj.ValueKind == JsonValueKind.String)
                            hrvOrderById = hrvOrderObj.GetString();
                        response = await apiClient.GetHrvMetricsAsync(token, hrvPage, hrvOrderById);
                        break;

                    case "api_v1metrics_hrv_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement hrvMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateHrvMetricAsync(token, hrvMetricDataObj);
                        break;

                    case "api_v1metrics_hrv_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement hrvIdObj) || hrvIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetHrvMetricByIdAsync(token, hrvIdObj.GetInt32().ToString());
                        break;

                    // Heart Rate Max Metrics (3 tools)
                    case "api_v1metrics_heart_rate_max_get":
                        int? hrmPage = null;
                        string? hrmOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement hrmPageObj) && hrmPageObj.ValueKind == JsonValueKind.Number)
                            hrmPage = hrmPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement hrmOrderObj) && hrmOrderObj.ValueKind == JsonValueKind.String)
                            hrmOrderById = hrmOrderObj.GetString();
                        response = await apiClient.GetHeartRateMaxMetricsAsync(token, hrmPage, hrmOrderById);
                        break;

                    case "api_v1metrics_heart_rate_max_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement hrmMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateHeartRateMaxMetricAsync(token, hrmMetricDataObj);
                        break;

                    case "api_v1metrics_heart_rate_max_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement hrmIdObj) || hrmIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetHeartRateMaxMetricByIdAsync(token, hrmIdObj.GetInt32().ToString());
                        break;

                    // Heart Rate Rest Metrics (3 tools)
                    case "api_v1metrics_heart_rate_rest_get":
                        int? hrrPage = null;
                        string? hrrOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement hrrPageObj) && hrrPageObj.ValueKind == JsonValueKind.Number)
                            hrrPage = hrrPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement hrrOrderObj) && hrrOrderObj.ValueKind == JsonValueKind.String)
                            hrrOrderById = hrrOrderObj.GetString();
                        response = await apiClient.GetHeartRateRestMetricsAsync(token, hrrPage, hrrOrderById);
                        break;

                    case "api_v1metrics_heart_rate_rest_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement hrrMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateHeartRateRestMetricAsync(token, hrrMetricDataObj);
                        break;

                    case "api_v1metrics_heart_rate_rest_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement hrrIdObj) || hrrIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetHeartRateRestMetricByIdAsync(token, hrrIdObj.GetInt32().ToString());
                        break;

                    // Mental Metrics (3 tools)
                    case "api_v1metrics_mental_get":
                        int? mentalPage = null;
                        string? mentalOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement mentalPageObj) && mentalPageObj.ValueKind == JsonValueKind.Number)
                            mentalPage = mentalPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement mentalOrderObj) && mentalOrderObj.ValueKind == JsonValueKind.String)
                            mentalOrderById = mentalOrderObj.GetString();
                        response = await apiClient.GetMentalMetricsAsync(token, mentalPage, mentalOrderById);
                        break;

                    case "api_v1metrics_mental_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement mentalMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateMentalMetricAsync(token, mentalMetricDataObj);
                        break;

                    case "api_v1metrics_mental_date_get":
                        if (!arguments.TryGetValue("date", out JsonElement mentalDateObj) || mentalDateObj.ValueKind != JsonValueKind.String)
                        {
                            throw new McpException("Missing required 'date' parameter");
                        }
                        response = await apiClient.GetMentalMetricByDateAsync(token, mentalDateObj.GetString()!);
                        break;

                    // Sleep Metrics (3 tools)
                    case "api_v1metrics_sleep_get":
                        int? sleepPage = null;
                        string? sleepOrderById = null;
                        if (arguments.TryGetValue("page", out JsonElement sleepPageObj) && sleepPageObj.ValueKind == JsonValueKind.Number)
                            sleepPage = sleepPageObj.GetInt32();
                        if (arguments.TryGetValue("orderById", out JsonElement sleepOrderObj) && sleepOrderObj.ValueKind == JsonValueKind.String)
                            sleepOrderById = sleepOrderObj.GetString();
                        response = await apiClient.GetSleepMetricsAsync(token, sleepPage, sleepOrderById);
                        break;

                    case "api_v1metrics_sleep_post":
                        if (!arguments.TryGetValue("metricData", out JsonElement sleepMetricDataObj))
                        {
                            throw new McpException("Missing required 'metricData' parameter");
                        }
                        response = await apiClient.CreateSleepMetricAsync(token, sleepMetricDataObj);
                        break;

                    case "api_v1metrics_sleep_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement sleepIdObj) || sleepIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetSleepMetricByIdAsync(token, sleepIdObj.GetInt32().ToString());
                        break;

                    // Race Results (1 additional tool)
                    case "api_v1raceresults_activity_id_get":
                        if (!arguments.TryGetValue("activityId", out JsonElement raceActivityIdObj) || raceActivityIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'activityId' parameter");
                        }
                        response = await apiClient.GetRaceResultByActivityAsync(token, raceActivityIdObj.GetInt32().ToString());
                        break;

                    // Tags (1 additional tool)
                    case "api_v1tags_id_get":
                        if (!arguments.TryGetValue("id", out JsonElement tagIdObj) || tagIdObj.ValueKind != JsonValueKind.Number)
                        {
                            throw new McpException("Missing required 'id' parameter");
                        }
                        response = await apiClient.GetTagByIdAsync(token, tagIdObj.GetInt32().ToString());
                        break;

                    default:
                        throw new McpException($"Unknown tool: '{toolName}'");
                }

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                return new CallToolResult
                {
                    Content = [new TextContentBlock { Text = content, Type = "text" }]
                };
            }
            catch (Exception ex)
            {
                throw new McpException($"Tool execution failed: {ex.Message}");
            }
        }
    }
} 