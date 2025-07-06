using ModelContextProtocol.Protocol;
using System.Text.Json;
using System.Collections.Generic;

namespace RunalyzeMcp
{
    public static class ToolDefinitions
    {
        public static readonly List<Tool> AllTools = new List<Tool>
        {
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
            new Tool
            {
                Name = "api_v1statistics_get",
                Description = "Get user statistics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1equipment_get",
                Description = "Get user equipment",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1health_get",
                Description = "Get user health data",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1metrics_get",
                Description = "Get user metrics",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1raceresults_get",
                Description = "Get user race results",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            },
            new Tool
            {
                Name = "api_v1tags_get",
                Description = "Get user tags",
                InputSchema = JsonSerializer.Deserialize<JsonElement>(@"{\n    ""type"": ""object"",\n    ""properties"": {\n        ""token"": {\n            ""type"": ""string"",\n            ""description"": ""Runalyze API token""\n        }\n    },\n    ""required"": [""token""]\n}")
            }
        };
    }
} 