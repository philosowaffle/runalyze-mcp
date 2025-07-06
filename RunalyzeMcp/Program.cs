using ModelContextProtocol.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configure the server to listen on port 8080
builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

// Basic health check endpoint
app.MapGet("/health", () => "OK");

app.Run();
