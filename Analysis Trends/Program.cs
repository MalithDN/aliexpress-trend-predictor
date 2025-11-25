using Analysis_Trends;
using Analysis_Trends.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// OpenAPI (Swagger)
builder.Services.AddOpenApi();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Bind RapidApi section
builder.Services.Configure<RapidApiOptions>(
    builder.Configuration.GetSection("RapidApi"));

// Register HttpClient-based AliExpress client
builder.Services.AddHttpClient<AliExpressRapidApiClient>();

// MongoDB configuration and services
builder.Services.Configure<Analysis_Trends.Models.MongoDbOptions>(
    builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<Analysis_Trends.Services.MongoDbService>();
builder.Services.AddSingleton<Analysis_Trends.Services.ProductRepository>();

var app = builder.Build();

// Dev-only OpenAPI explorer
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable static files (for index.html)
app.UseStaticFiles();

// Use CORS
app.UseCors("AllowAll");

// Default route to index.html
app.MapFallback("/{**path}", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync(Path.Combine(app.Environment.WebRootPath, "index.html"));
});

//app.UseHttpsRedirection();

// ----------------------
// Sample weather endpoint
// ----------------------
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

// -------------------------
// 🔹 1) Raw hot products API
// -------------------------
app.MapGet("/aliexpress/hot-products", async (
    string category,
    int page,
    AliExpressRapidApiClient client,
    Analysis_Trends.Services.ProductRepository repo) =>
{
    if (page <= 0) page = 1;
    if (string.IsNullOrWhiteSpace(category))
        return Results.BadRequest("category parameter is required");

    var result = await client.GetGlobalHotProductsAsync(category, page);

    // Persist products to MongoDB (best-effort)
    try
    {
        if (result?.Data != null && result.Data.Count > 0)
        {
            // fire-and-forget persistence, don't block API response
            _ = Task.Run(async () =>
            {
                try { await repo.InsertManyAsync(result.Data); } catch { /* swallow */ }
            });
        }
    }
    catch { /* swallow */ }

    return Results.Ok(result);
})
.WithName("GetGlobalHotProducts")
.WithDescription("Get hot products by category ID. Example: /aliexpress/hot-products?category=15&page=1");

// -------------------------
// 🔹 Debug: Test data endpoint
// -------------------------
app.MapGet("/test/products", async (AliExpressRapidApiClient client) =>
{
    var result = await client.GetGlobalHotProductsAsync("15", 1);
    return Results.Ok(new { 
        status = "success",
        count = result?.Data?.Count ?? 0,
        sampleProduct = result?.Data?.FirstOrDefault(),
        allProducts = result?.Data
    });
})
.WithName("TestProductsDebug");

// -------------------------
// 🔹 1b) Get categories
// -------------------------
app.MapGet("/aliexpress/categories", async (
    AliExpressRapidApiClient client) =>
{
    var result = await client.GetCategoriesAsync();
    return Results.Ok(result);
})
.WithName("GetCategories");

// -------------------------------------
// 🔹 2) Global trend analysis endpoint
// -------------------------------------
app.MapGet("/analysis/global-trend", async (
    string category,
    int page,
    AliExpressRapidApiClient client) =>
{
    if (page <= 0) page = 1;
    if (string.IsNullOrWhiteSpace(category))
        return Results.BadRequest("category parameter is required");

    var data = await client.GetGlobalHotProductsAsync(category, page);

    if (data == null || data.Data == null || data.Data.Count == 0)
        return Results.Ok(new GlobalTrendSummary());

    var products = data.Data;

    // Top 10 globally by orders
    var topProducts = products
        .OrderByDescending(p => p.Orders)
        .Take(10)
        .ToList();

    // Category trend
    var topCategories = products
        .GroupBy(p => p.CategoryName ?? "Unknown")
        .Select(g => new CategoryTrend
        {
            CategoryName = g.Key,
            ProductCount = g.Count(),
            TotalOrders = g.Sum(x => x.Orders)
        })
        .OrderByDescending(c => c.TotalOrders)
        .Take(5)
        .ToList();

    var summary = new GlobalTrendSummary
    {
        TotalProducts = products.Count,
        TopProducts = topProducts,
        TopCategories = topCategories
    };

    return Results.Ok(summary);
})
.WithName("GetGlobalTrendSummary");

// -------------------------
// 🔹 Query MongoDB data endpoint
// -------------------------
app.MapGet("/database/products", async (Analysis_Trends.Services.MongoDbService mongoDbService) =>
{
    try
    {
        var collection = mongoDbService.GetCollection<MongoDB.Bson.BsonDocument>("hot_products");
        var products = await collection.Find(new MongoDB.Bson.BsonDocument()).ToListAsync();
        
        return Results.Ok(new 
        { 
            status = "success",
            count = products.Count,
            data = products
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new 
        { 
            status = "error",
            message = ex.Message,
            data = new object[0]
        });
    }
})
.WithName("GetDatabaseProducts")
.WithDescription("Query all products stored in MongoDB");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
