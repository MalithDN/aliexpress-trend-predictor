using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Analysis_Trends.Models;

namespace Analysis_Trends.Services
{
    public class MongoDbService
    {
        private readonly MongoDbOptions _options;
        private IMongoDatabase? _db;
        private readonly ILogger<MongoDbService> _logger;

        public MongoDbService(IOptions<MongoDbOptions> options, ILogger<MongoDbService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        private void EnsureConnected()
        {
            if (_db != null) return;

            try
            {
                var client = new MongoClient(_options.ConnectionString);
                _db = client.GetDatabase(_options.Database);
                _logger.LogInformation("MongoDB connected successfully to {Database}", _options.Database);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to MongoDB");
                throw;
            }
        }

        public IMongoCollection<T> GetCollection<T>(string? collectionName = null)
        {
            EnsureConnected();
            if (_db == null)
                throw new InvalidOperationException("MongoDB database is not connected");

            var name = collectionName ?? _options.Collection;
            return _db.GetCollection<T>(name);
        }
    }
}
