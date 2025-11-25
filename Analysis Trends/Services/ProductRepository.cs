using MongoDB.Bson;
using MongoDB.Driver;
using Analysis_Trends.Models;
using Microsoft.Extensions.Options;
using Analysis_Trends;

namespace Analysis_Trends.Services
{
    public class ProductRepository
    {
        private readonly MongoDbService _mongoDbService;
        private readonly MongoDbOptions _options;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(MongoDbService mongoDbService, IOptions<MongoDbOptions> options, ILogger<ProductRepository> logger)
        {
            _mongoDbService = mongoDbService;
            _options = options.Value;
            _logger = logger;
        }

        public async Task InsertManyAsync(IEnumerable<HotProduct> products)
        {
            try
            {
                if (products == null) return;

                var collection = _mongoDbService.GetCollection<BsonDocument>(_options.Collection);
                
                var docs = products.Select(p =>
                {
                    var doc = new BsonDocument
                    {
                        { "productId", p.Id.HasValue ? (BsonValue)p.Id.Value : BsonNull.Value },
                        { "title", p.Title ?? string.Empty },
                        { "categoryName", p.CategoryName ?? string.Empty },
                        { "price", p.Price ?? string.Empty },
                        { "originalPrice", p.OriginalPrice ?? string.Empty },
                        { "rating", p.Rating ?? string.Empty },
                        { "orders", p.Orders },
                        { "productUrl", p.ProductUrl ?? string.Empty },
                        { "shopName", p.ShopName ?? string.Empty },
                        { "shopId", p.ShopId.HasValue ? (BsonValue)p.ShopId.Value : BsonNull.Value },
                        { "discount", p.Discount ?? string.Empty },
                        { "fetchedAt", DateTime.UtcNow }
                    };

                    // image urls
                    var images = new BsonArray();
                    if (p.ImageUrls != null)
                    {
                        var list = p.ImageUrls.String;
                        if (list != null)
                        {
                            foreach (var u in list)
                                images.Add(u);
                        }
                    }
                    doc.Add("images", images);
                    return doc;
                }).ToList();

                if (docs.Count == 0) return;

                await collection.InsertManyAsync(docs);
                _logger.LogInformation("Inserted {Count} products into MongoDB", docs.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting products into MongoDB");
                // Don't throw - allow API to continue even if MongoDB is unavailable
            }
        }
    }
}
