# AliExpress Trend Predictor

An ASP.NET Core Web API that integrates with the AliExpress API to analyze global product trends and predict next month's most popular products using real-time data and machine-learning inspired scoring methods.

## Overview

This project leverages the **Free AliExpress API** from RapidAPI to:
- Fetch real-time hot products across multiple categories
- Analyze product trends by category and sales volume
- Identify top-performing products based on ratings, sales, and price points
- Provide predictive insights for e-commerce trend analysis

## Features

✨ **Core Features:**
- 🔥 Real-time hot products from AliExpress
- 📊 Global trend analysis and category insights
- 🏆 Top product identification by sales volume and ratings
- 🔄 Trend prediction based on current market data
- 📱 RESTful API endpoints with Swagger/OpenAPI documentation
- 🚀 Scalable ASP.NET Core architecture

## Tech Stack

- **Framework:** ASP.NET Core 10.0
- **Language:** C#
- **API Integration:** RapidAPI (Free AliExpress API)
- **Serialization:** System.Text.Json
- **Logging:** Built-in Microsoft.Extensions.Logging
- **Documentation:** OpenAPI/Swagger

## Prerequisites

- .NET 10.0 SDK or later
- RapidAPI Account with Free AliExpress API subscription
- API Key from RapidAPI

## Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/aliexpress-trend-predictor.git
   cd "Analysis Trends"
   ```

2. **Install dependencies:**
   ```bash
   dotnet restore
   ```

3. **Configure API credentials:**
   
   Update `appsettings.json` with your RapidAPI credentials:
   ```json
   {
     "RapidApi": {
       "ApiKey": "YOUR_RAPIDAPI_KEY_HERE",
       "Host": "free-aliexpress-api.p.rapidapi.com",
       "BaseUrl": "https://free-aliexpress-api.p.rapidapi.com"
     }
   }
   ```

4. **Build the project:**
   ```bash
   dotnet build
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```

   The API will be available at: `http://localhost:5280`

## API Endpoints

### 1. Get Hot Products
**Endpoint:** `GET /aliexpress/hot-products`

**Parameters:**
- `category` (required): Category ID (e.g., 15 for Electronics)
- `page` (optional): Page number (default: 1)
- `sort` (optional): Sort order - `LAST_VOLUME_DESC`, `LAST_PRICE_ASC`, etc. (default: `LAST_VOLUME_DESC`)
- `targetCurrency` (optional): Currency code (default: `USD`)
- `targetLanguage` (optional): Language code (default: `EN`)

**Example:**
```bash
curl "http://localhost:5280/aliexpress/hot-products?category=15&page=1"
```

**Response:**
```json
{
  "data": [
    {
      "id": 32989738329,
      "title": "Product Title",
      "categoryName": "Electronics",
      "price": "0.21",
      "originalPrice": "0.25",
      "rating": 4.5,
      "orders": 1250,
      "imageUrls": {
        "urls": ["url1", "url2", ...]
      },
      "productUrl": "https://..."
    }
  ]
}
```

### 2. Get Categories
**Endpoint:** `GET /aliexpress/categories`

**Example:**
```bash
curl "http://localhost:5280/aliexpress/categories"
```

**Response:**
```json
{
  "categories": [
    {
      "id": 15,
      "name": "Electronics"
    },
    ...
  ]
}
```

### 3. Global Trend Analysis
**Endpoint:** `GET /analysis/global-trend`

**Parameters:**
- `category` (required): Category ID
- `page` (optional): Page number (default: 1)

**Example:**
```bash
curl "http://localhost:5280/analysis/global-trend?category=15&page=1"
```

**Response:**
```json
{
  "totalProducts": 50,
  "topProducts": [
    {
      "id": 32989738329,
      "title": "Best Seller",
      "orders": 5000,
      "rating": 4.8,
      ...
    }
  ],
  "topCategories": [
    {
      "categoryName": "Electronics",
      "productCount": 35,
      "totalOrders": 25000
    }
  ]
}
```

## Project Structure

```
Analysis Trends/
├── Program.cs                          # Application entry point & API endpoints
├── appsettings.json                    # Configuration
├── RapidApiOptions.cs                  # API configuration model
├── HotProductsModels.cs                # Data models
├── Services/
│   └── AliExpressRapidApiClient.cs    # RapidAPI integration service
├── Properties/
│   └── launchSettings.json             # Launch configuration
└── Analysis Trends.csproj              # Project file
```

## Configuration

### appsettings.json
```json
{
  "RapidApi": {
    "ApiKey": "your_rapidapi_key",
    "Host": "free-aliexpress-api.p.rapidapi.com",
    "BaseUrl": "https://free-aliexpress-api.p.rapidapi.com"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Usage Examples

### Get Electronics Trending Now
```bash
curl "http://localhost:5280/aliexpress/hot-products?category=15&page=1&sort=LAST_VOLUME_DESC"
```

### Analyze Trends in Home & Garden
```bash
curl "http://localhost:5280/analysis/global-trend?category=50&page=1"
```

### Get All Available Categories
```bash
curl "http://localhost:5280/aliexpress/categories"
```

## Swagger/OpenAPI Documentation

Once the application is running, access the interactive API documentation:
- **Swagger UI:** `http://localhost:5280/openapi/v1.json`

## Logging

The application includes comprehensive logging. View logs in the console output when running in development mode:

```
info: Analysis_Trends.Services.AliExpressRapidApiClient[0]
      Requesting: https://free-aliexpress-api.p.rapidapi.com/hot_products?...
```

## Future Enhancements

🚀 Planned features:
- Machine learning model for trend prediction
- Database persistence for historical trend analysis
- Caching layer for improved performance
- Advanced filtering and sorting options
- Scheduled jobs for periodic data collection
- Frontend dashboard for visualization
- Multi-language support
- Advanced analytics and reporting

## API Rate Limits

The Free AliExpress API on RapidAPI has rate limits. Check your RapidAPI subscription for:
- Requests per month
- Concurrent connections
- Response limits

## Troubleshooting

### "category id is required" Error
This means the API requires a valid category ID. Use the `/aliexpress/categories` endpoint to fetch available categories.

### 404 Not Found
Ensure you're using the correct endpoint paths:
- `/hot_products` - not `/Products`
- `/categories` - not `/Categories`

### API Key Issues
Verify your RapidAPI credentials in `appsettings.json`:
1. Log in to RapidAPI
2. Go to your Free AliExpress API subscription
3. Copy the API key and host header
4. Update `appsettings.json`

## Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Disclaimer

This project uses data from AliExpress via the Free AliExpress API. The predictions and insights are based on real-time data and machine-learning inspired algorithms, but results may vary. Use at your own discretion for business decisions.

## Support

For issues and questions:
- 📧 Email: your-email@example.com
- 🐛 Issues: [GitHub Issues](https://github.com/yourusername/aliexpress-trend-predictor/issues)
- 💬 Discussions: [GitHub Discussions](https://github.com/yourusername/aliexpress-trend-predictor/discussions)

## Acknowledgments

- **RapidAPI** - For providing the Free AliExpress API
- **AliExpress** - For the data source
- **ASP.NET Core Community** - For the amazing framework

---

Made with ❤️ for e-commerce trend enthusiasts
