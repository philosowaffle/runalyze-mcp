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
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""file"": {
            ""type"": ""string"",
            ""description"": ""Activity file as base64-encoded string""
        },
        ""filename"": {
            ""type"": ""string"",
            ""description"": ""Filename of the activity file""
        }
    },
    ""required"": [""token"", ""file"", ""filename""]
}")
            },
            new Tool
            {
                Name = "api_activity_download",
                Description = "Download an activity file from Runalyze",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID to download""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activitiesuploads_id_get",
                Description = "Get upload status for an activity",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Upload ID to check""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activities_id_get",
                Description = "Get activity details",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activities_get",
                Description = "Get list of activities",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""limit"": {
            ""type"": ""integer"",
            ""description"": ""Number of activities to return""
        },
        ""offset"": {
            ""type"": ""integer"",
            ""description"": ""Number of activities to skip""
        }
    },
    ""required"": [""token""]
}")
            },

            // Download Endpoints (6 tools)
            new Tool
            {
                Name = "api_v1activity_id_fit_get",
                Description = "Download original FIT file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activity_id_fitlog_get",
                Description = "Download FITLOG file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activity_id_gpx_get",
                Description = "Download GPX file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activity_id_kml_get",
                Description = "Download KML file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activity_id_social_image_get",
                Description = "Download social image",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1activity_id_tcx_get",
                Description = "Download TCX file",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Activity ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },

            // Statistics Endpoints (1 tool)
            new Tool
            {
                Name = "api_v1statistics_get",
                Description = "Get user statistics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        }
    },
    ""required"": [""token""]
}")
            },

            // Equipment Endpoints (4 tools)
            new Tool
            {
                Name = "api_v1equipment_get",
                Description = "Get user equipment",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""page"": {
            ""type"": ""integer"",
            ""description"": ""Page number""
        },
        ""orderById"": {
            ""type"": ""string"",
            ""description"": ""Order by ID""
        }
    },
    ""required"": [""token""]
}")
            },
            new Tool
            {
                Name = "api_v1equipment_id_get",
                Description = "Get specific equipment",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Equipment ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },
            new Tool
            {
                Name = "api_v1equipment_category_get",
                Description = "Get equipment categories",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""page"": {
            ""type"": ""integer"",
            ""description"": ""Page number""
        },
        ""orderById"": {
            ""type"": ""string"",
            ""description"": ""Order by ID""
        }
    },
    ""required"": [""token""]
}")
            },
            new Tool
            {
                Name = "api_v1equipment_category_id_get",
                Description = "Get specific equipment category",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Category ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            },

            // Health Endpoints (2 tools)
            new Tool
            {
                Name = "api_v1health_get",
                Description = "Get health data",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        }
    },
    ""required"": [""token""]
}")
            },
            new Tool
            {
                Name = "api_v1health_bulk_upload_post",
                Description = "Bulk upload health data",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""file"": {
            ""type"": ""string"",
            ""description"": ""Health data file as base64-encoded string""
        }
    },
    ""required"": [""token"", ""file""]
}")
            },

            // Metrics Endpoints (2 tools)
            new Tool
            {
                Name = "api_v1metrics_get",
                Description = "Get metrics data",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        }
    },
    ""required"": [""token""]
}")
            },

            // Blood Glucose Metrics (3 tools) - Note: Count updated to match actual number
            new Tool
            {
                Name = "api_v1metrics_blood_glucose_get",
                Description = "Get blood glucose metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""page"": {
            ""type"": ""integer"",
            ""description"": ""Page number""
        },
        ""orderById"": {
            ""type"": ""string"",
            ""description"": ""Order by ID""
        }
    },
    ""required"": [""token""]
}")
            },
            new Tool
            {
                Name = "api_v1metrics_blood_glucose_post",
                Description = "Create blood glucose metric",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""metricData"": {
            ""type"": ""object"",
            ""description"": ""Blood glucose metric data""
        }
    },
    ""required"": [""token"", ""metricData""]
}")
            },
            new Tool
            {
                Name = "api_v1metrics_blood_glucose_id_get",
                Description = "Get blood glucose metric by ID",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{
    ""type"": ""object"",
    ""properties"": {
        ""token"": {
            ""type"": ""string"",
            ""description"": ""Runalyze API token""
        },
        ""id"": {
            ""type"": ""integer"",
            ""description"": ""Metric ID""
        }
    },
    ""required"": [""token"", ""id""]
}")
            }
        };
    }
} 