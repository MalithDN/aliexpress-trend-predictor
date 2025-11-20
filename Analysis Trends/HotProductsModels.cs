using System.Text.Json.Serialization;

namespace Analysis_Trends
{
    public class HotProductsResponse
    {
        [JsonPropertyName("data")]
        public List<HotProduct> Data { get; set; } = new();
    }

    public class HotProduct
    {
        [JsonPropertyName("product_id")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
        public long? Id { get; set; }

        [JsonPropertyName("product_title")]
        public string? Title { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("app_sale_price")]
        public string? Price { get; set; }

        [JsonPropertyName("original_price")]
        public string? OriginalPrice { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("orders")]
        public int Orders { get; set; }

        [JsonPropertyName("product_small_image_urls")]
        public ImageUrls? ImageUrls { get; set; }

        [JsonPropertyName("product_url")]
        public string? ProductUrl { get; set; }
    }

    public class ImageUrls
    {
        [JsonPropertyName("string")]
        public List<string>? Urls { get; set; }
    }

    // For analysis result:

    public class GlobalTrendSummary
    {
        public int TotalProducts { get; set; }
        public List<HotProduct> TopProducts { get; set; } = new();
        public List<CategoryTrend> TopCategories { get; set; } = new();
    }

    public class CategoryTrend
    {
        public string CategoryName { get; set; } = "";
        public int ProductCount { get; set; }
        public int TotalOrders { get; set; }
    }
}
