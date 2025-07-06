using ModelContextProtocol.Protocol;
using System.Text.Json;
using System.Collections.Generic;

namespace RunalyzeMcp
{
    public static class ToolDefinitions
    {
        public static readonly List<Tool> AllTools = new List<Tool>
        {
            // Activity Endpoints (5 tools)
            new Tool
            {
                Name = "api_activity_upload",
                Description = "Upload an activity file to Runalyze",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""file"": {\n            ""type"": ""string"",\n            ""description"": ""Activity file as base64-encoded string""\n        },\n        ""filename"": {\n            ""type"": ""string"",\n            ""description"": ""Filename of the activity file""\n        }\n    },\n    ""required"": [""token"", ""file"", ""filename""]\n}")
            },
            new Tool
            {
                Name = "api_activity_download",
                Description = "Download an activity file from Runalyze",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID to download""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activitiesuploads_id_get",
                Description = "Get upload status for an activity",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Upload ID to check""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activities_id_get",
                Description = "Get activity details",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activities_get",
                Description = "Get list of activities",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""limit"": {\n            ""type"": ""integer"",\n            ""description"": ""Number of activities to return""\n        },\n        ""offset"": {\n            ""type"": ""integer"",\n            ""description"": ""Number of activities to skip""\n        }\n    },\n    ""required"": [""token""]\n}")
            },

            // Download Endpoints (5 tools)
            new Tool
            {
                Name = "api_v1activity_id_fit_get",
                Description = "Download original FIT file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activity_id_fitlog_get",
                Description = "Download FITLOG file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activity_id_gpx_get",
                Description = "Download GPX file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activity_id_kml_get",
                Description = "Download KML file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activity_id_social_image_get",
                Description = "Download social image",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1activity_id_tcx_get",
                Description = "Download TCX file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Statistics Endpoints (1 tool)
            new Tool
            {
                Name = "api_v1statistics_get",
                Description = "Get user statistics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },

            // Equipment Endpoints (4 tools)
            new Tool
            {
                Name = "api_v1equipment_get",
                Description = "Get user equipment",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1equipment_id_get",
                Description = "Get specific equipment",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Equipment ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },
            new Tool
            {
                Name = "api_v1equipment_category_get",
                Description = "Get equipment categories",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1equipment_category_id_get",
                Description = "Get specific equipment category",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Category ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Health Endpoints (2 tools)
            new Tool
            {
                Name = "api_v1health_get",
                Description = "Get health data",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1health_bulk_upload_post",
                Description = "Bulk upload health data",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""file"": {\n            ""type"": ""string"",\n            ""description"": ""Health data file as base64-encoded string""\n        }\n    },\n    ""required"": [""token"", ""file""]\n}")
            },

            // Metrics Endpoints (2 tools)
            new Tool
            {
                Name = "api_v1metrics_get",
                Description = "Get metrics data",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },

            // Blood Glucose Metrics (4 tools)
            new Tool
            {
                Name = "api_v1metrics_blood_glucose_get",
                Description = "Get blood glucose metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_blood_glucose_post",
                Description = "Create blood glucose metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Blood glucose metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_blood_glucose_id_get",
                Description = "Get blood glucose metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Blood Pressure Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_blood_pressure_get",
                Description = "Get blood pressure metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_blood_pressure_post",
                Description = "Create blood pressure metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Blood pressure metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_blood_pressure_id_get",
                Description = "Get blood pressure metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Body Composition Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_body_composition_get",
                Description = "Get body composition metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_body_composition_post",
                Description = "Create body composition metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Body composition metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_body_composition_id_get",
                Description = "Get body composition metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Body Temperature Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_body_temperature_get",
                Description = "Get body temperature metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_body_temperature_post",
                Description = "Create body temperature metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Body temperature metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_body_temperature_id_get",
                Description = "Get body temperature metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Daily Note Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_daily_note_get",
                Description = "Get daily note metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_daily_note_post",
                Description = "Create daily note metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Daily note metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_daily_note_date_get",
                Description = "Get daily note by date",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""date"": {\n            ""type"": ""string"",\n            ""description"": ""Date in YYYY-MM-DD format""\n        }\n    },\n    ""required"": [""token"", ""date""]\n}")
            },

            // HRV Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_hrv_get",
                Description = "Get HRV metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_hrv_post",
                Description = "Create HRV metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""HRV metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_hrv_id_get",
                Description = "Get HRV metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Heart Rate Max Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_heart_rate_max_get",
                Description = "Get heart rate max metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_heart_rate_max_post",
                Description = "Create heart rate max metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Heart rate max metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_heart_rate_max_id_get",
                Description = "Get heart rate max metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Heart Rate Rest Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_heart_rate_rest_get",
                Description = "Get heart rate rest metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_heart_rate_rest_post",
                Description = "Create heart rate rest metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Heart rate rest metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_heart_rate_rest_id_get",
                Description = "Get heart rate rest metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Mental Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_mental_get",
                Description = "Get mental metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_mental_post",
                Description = "Create mental metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Mental metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_mental_date_get",
                Description = "Get mental metric by date",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""date"": {\n            ""type"": ""string"",\n            ""description"": ""Date in YYYY-MM-DD format""\n        }\n    },\n    ""required"": [""token"", ""date""]\n}")
            },

            // Sleep Metrics (3 tools)
            new Tool
            {
                Name = "api_v1metrics_sleep_get",
                Description = "Get sleep metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_sleep_post",
                Description = "Create sleep metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""metricData"": {\n            ""type"": ""object"",\n            ""description"": ""Sleep metric data""\n        }\n    },\n    ""required"": [""token"", ""metricData""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_sleep_id_get",
                Description = "Get sleep metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Metric ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            },

            // Race Results (2 tools)
            new Tool
            {
                Name = "api_v1raceresults_get",
                Description = "Get user race results",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1raceresults_activity_id_get",
                Description = "Get race result by activity",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""activityId"": {\n            ""type"": ""integer"",\n            ""description"": ""Activity ID""\n        }\n    },\n    ""required"": [""token"", ""activityId""]\n}")
            },

            // Tags (2 tools)
            new Tool
            {
                Name = "api_v1tags_get",
                Description = "Get user tags",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""page"": {\n            ""type"": ""integer"",\n            ""description"": ""Page number""\n        },\n        ""orderById"": {\n            ""type"": ""string"",\n            ""description"": ""Order by ID""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1tags_id_get",
                Description = "Get tag by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        },\n        ""id"": {\n            ""type"": ""integer"",\n            ""description"": ""Tag ID""\n        }\n    },\n    ""required"": [""token"", ""id""]\n}")
            }
        };
    }
} 