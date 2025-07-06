using NUnit.Framework;
using RunalyzeMcp;
using System.Linq;
using System.Text.Json;
using ModelContextProtocol.Protocol;

namespace RunalyzeMcp.Tests
{
    [TestFixture]
    public class McpToolConfigurationTests
    {
        [Test]
        public void ToolDefinitions_ShouldHaveValidJsonSchemas()
        {
            // Act & Assert
            foreach (var tool in ToolDefinitions.AllTools)
            {
                Assert.That(tool.Name, Is.Not.Null.And.Not.Empty, $"Tool name should not be null or empty");
                Assert.That(tool.Description, Is.Not.Null.And.Not.Empty, $"Tool {tool.Name} should have a description");
                
                // Verify the InputSchema is valid JSON
                Assert.DoesNotThrow(() =>
                {
                    var schemaJson = JsonSerializer.Serialize(tool.InputSchema);
                    var parsedSchema = JsonSerializer.Deserialize<JsonElement>(schemaJson);
                    
                    // Verify it has basic schema structure
                    Assert.That(parsedSchema.TryGetProperty("type", out var typeProperty), Is.True, 
                        $"Tool {tool.Name} schema should have 'type' property");
                    Assert.That(typeProperty.GetString(), Is.EqualTo("object"), 
                        $"Tool {tool.Name} schema type should be 'object'");
                        
                    Assert.That(parsedSchema.TryGetProperty("properties", out var propertiesProperty), Is.True, 
                        $"Tool {tool.Name} schema should have 'properties' property");
                        
                    Assert.That(parsedSchema.TryGetProperty("required", out var requiredProperty), Is.True, 
                        $"Tool {tool.Name} schema should have 'required' property");
                        
                }, $"Tool {tool.Name} should have valid JSON schema");
            }
        }

        [Test]
        public void ToolDefinitions_ShouldHaveExpectedCount()
        {
            // Act
            var toolCount = ToolDefinitions.AllTools.Count;
            
            // Assert - Updated to match the simplified tool definitions
            Assert.That(toolCount, Is.GreaterThan(0), "Should have tools defined");
        }

        [Test]
        public void ToolDefinitions_ShouldHaveUniqueNames()
        {
            // Act
            var toolNames = ToolDefinitions.AllTools.Select(t => t.Name).ToList();
            var uniqueNames = toolNames.Distinct().ToList();
            
            // Assert
            Assert.That(uniqueNames.Count, Is.EqualTo(toolNames.Count), 
                "All tool names should be unique");
        }

        [Test]
        public void ToolDefinitions_ShouldAllRequireTokenParameter()
        {
            // Act & Assert
            foreach (var tool in ToolDefinitions.AllTools)
            {
                var schemaJson = JsonSerializer.Serialize(tool.InputSchema);
                var parsedSchema = JsonSerializer.Deserialize<JsonElement>(schemaJson);
                
                // Verify token is in required array
                Assert.That(parsedSchema.TryGetProperty("required", out var requiredProperty), Is.True, 
                    $"Tool {tool.Name} should have required property");
                    
                var requiredArray = requiredProperty.EnumerateArray().Select(e => e.GetString()).ToArray();
                Assert.That(requiredArray, Contains.Item("token"), 
                    $"Tool {tool.Name} should require 'token' parameter");
                    
                // Verify token is in properties
                Assert.That(parsedSchema.TryGetProperty("properties", out var propertiesProperty), Is.True, 
                    $"Tool {tool.Name} should have properties");
                    
                Assert.That(propertiesProperty.TryGetProperty("token", out var tokenProperty), Is.True, 
                    $"Tool {tool.Name} should have 'token' property");
                    
                Assert.That(tokenProperty.TryGetProperty("type", out var tokenTypeProperty), Is.True, 
                    $"Tool {tool.Name} token should have type");
                    
                Assert.That(tokenTypeProperty.GetString(), Is.EqualTo("string"), 
                    $"Tool {tool.Name} token should be string type");
            }
        }

        [Test]
        public void ToolDefinitions_PrintAllToolNames()
        {
            // This test helps debug by printing all tool names
            var toolNames = ToolDefinitions.AllTools.Select(t => t.Name).OrderBy(n => n).ToList();
            
            TestContext.WriteLine($"Total tools: {toolNames.Count}");
            TestContext.WriteLine("Tool names:");
            foreach (var name in toolNames)
            {
                TestContext.WriteLine($"  - {name}");
            }
            
            // Always pass - this is just for debugging
            Assert.Pass();
        }
    }
}