using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Analysis_Trends.Services
{
    public class AliExpressRapidApiClient
    {
        private readonly HttpClient _http;
        private readonly RapidApiOptions _options;
        private readonly ILogger<AliExpressRapidApiClient> _logger;

        public AliExpressRapidApiClient(HttpClient http, IOptions<RapidApiOptions> options, ILogger<AliExpressRapidApiClient> logger)
        {
            _http = http;
            _options = options.Value;
            _logger = logger;

            if (!string.IsNullOrEmpty(_options.BaseUrl))
            {
                _http.BaseAddress = new Uri(_options.BaseUrl);
            }
            
            if (!string.IsNullOrEmpty(_options.ApiKey))
            {
                _http.DefaultRequestHeaders.Add("x-rapidapi-key", _options.ApiKey);
            }
            
            if (!string.IsNullOrEmpty(_options.Host))
            {
                _http.DefaultRequestHeaders.Add("x-rapidapi-host", _options.Host);
            }
        }

        // Get products from the API
        // Endpoint: GET /hot_products
        // Note: cat_id (category ID) is REQUIRED by the API
        // API returns an array directly: [{...}, {...}]
        public async Task<HotProductsResponse?> GetGlobalHotProductsAsync(
            string category,
            int page = 1,
            string sort = "LAST_VOLUME_DESC",
            string targetCurrency = "USD",
            string targetLanguage = "EN")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    _logger.LogWarning("Category ID is required for hot_products endpoint");
                    return new HotProductsResponse { Data = new List<HotProduct>() };
                }

                var url = $"/hot_products?page={page}&sort={sort}&target_currency={targetCurrency}&target_language={targetLanguage}&cat_id={Uri.EscapeDataString(category)}";

                _logger.LogInformation($"Requesting: {_options.BaseUrl}{url}");

                var response = await _http.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                
                _logger.LogInformation($"Response Status: {response.StatusCode}");
                _logger.LogInformation($"Response Content: {content.Substring(0, Math.Min(500, content.Length))}...");

                response.EnsureSuccessStatusCode();

                // API returns an array directly, so parse it and wrap in our response model
                var products = System.Text.Json.JsonSerializer.Deserialize<List<HotProduct>>(content, 
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return new HotProductsResponse 
                { 
                    Data = products ?? new List<HotProduct>() 
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP Error: {ex.Message}");
                // Return error response instead of throwing
                return new HotProductsResponse
                {
                    Data = new List<HotProduct>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return new HotProductsResponse
                {
                    Data = new List<HotProduct>()
                };
            }
        }

        // Get categories from the API
        // Endpoint: GET /categories
        public async Task<dynamic> GetCategoriesAsync()
        {
            try
            {
                _logger.LogInformation($"Requesting: {_options.BaseUrl}/categories");

                var response = await _http.GetAsync("/categories");
                var content = await response.Content.ReadAsStringAsync();
                
                _logger.LogInformation($"Response Status: {response.StatusCode}");
                _logger.LogInformation($"Response Content: {content}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<dynamic>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
