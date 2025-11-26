# AliExpress Trend Predictor 🚀

A full-stack real-time e-commerce trend analysis application built with ASP.NET Core 10.0 that analyzes global product trends from AliExpress. Features a responsive web dashboard with live data updates, MongoDB persistence, and comprehensive API integration.

## 🎯 Overview

This production-ready application leverages the **Free AliExpress API** from RapidAPI to deliver:
- 🔥 Real-time hot products across 100+ categories
- 📊 Live trend analysis with sales volume and rating metrics
- 🏆 Top-performing product identification
- 💾 MongoDB Atlas persistence for historical data
- 🔄 Auto-refresh every 40 seconds with countdown timer
- 🎨 Responsive web dashboard with gradient UI design

## ✨ Features

### Backend API
- ✅ 5 RESTful API endpoints with comprehensive error handling
- ✅ Real-time data fetching from RapidAPI (AliExpress)
- ✅ MongoDB Atlas integration for data persistence
- ✅ Fire-and-forget asynchronous data storage
- ✅ Category-based filtering and pagination
- ✅ Global statistics and trend calculations
- ✅ CORS-enabled for cross-origin requests
- ✅ OpenAPI/Swagger documentation

### Frontend Dashboard
- ✅ Responsive single-page application
- ✅ Auto-refresh with visual countdown (40-second intervals)
- ✅ Manual stop/start refresh controls
- ✅ Category dropdown with live data
- ✅ Product grid with image, price, orders, rating
- ✅ Top sellers tab for best-performing products
- ✅ Global statistics dashboard
- ✅ Error handling with user-friendly messages
- ✅ Gradient purple theme with modern UI

## 🛠️ Tech Stack

### Backend
- **Framework:** ASP.NET Core 10.0 (Minimal API)
- **Language:** C# with nullable reference types
- **Database:** MongoDB Atlas (MongoDB.Driver 3.5.1)
- **API Integration:** RapidAPI (Free AliExpress API)
- **Serialization:** System.Text.Json with camelCase policy
- **Architecture:** Service-based with dependency injection
- **Logging:** Microsoft.Extensions.Logging

### Frontend
- **HTML5** with semantic markup
- **CSS3** with gradient backgrounds and flexbox
- **JavaScript (ES6+)** with Fetch API
- **No frameworks** - pure vanilla JS for performance

## 📋 Prerequisites

- **.NET 10.0 SDK** or later
- **MongoDB Atlas Account** (free tier available)
- **RapidAPI Account** with Free AliExpress API subscription
- **API Key** from RapidAPI
- **Web Browser** for dashboard access

## 🚀 Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/MalithDN/aliexpress-trend-predictor.git
   cd "ASP.NET Core Web API/Analysis Trends"
   ```

2. **Install dependencies:**
   ```bash
   dotnet restore
   ```

3. **Configure API credentials:**
   
   Update `appsettings.json` with your credentials:
   ```json
   {
     "RapidApi": {
       "ApiKey": "YOUR_RAPIDAPI_KEY_HERE",
       "Host": "free-aliexpress-api.p.rapidapi.com",
       "BaseUrl": "https://free-aliexpress-api.p.rapidapi.com"
     },
     "MongoDb": {
       "ConnectionString": "mongodb+srv://username:password@cluster.mongodb.net/aliexpress?retryWrites=true&w=majority",
       "Database": "aliexpress",
       "Collection": "hot_products"
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

   The API will be available at: **`http://localhost:5280`**

6. **Access the dashboard:**
   
   Open your browser and navigate to: **`http://localhost:5280`**

## 📡 API Endpoints

### 1. Get Hot Products
**Endpoint:** `GET /aliexpress/hot-products`

Fetches real-time hot products from AliExpress and stores them in MongoDB.

**Parameters:**
- `category` (required): Category ID (e.g., `2` for Food, `44` for Consumer Electronics)
- `page` (optional): Page number (default: `1`)
- `sort` (optional): Sort order - `LAST_VOLUME_DESC` (default), `LAST_PRICE_ASC`, etc.
- `targetCurrency` (optional): Currency code (default: `USD`)
- `targetLanguage` (optional): Language code (default: `EN`)

**Example:**
```bash
curl "http://localhost:5280/aliexpress/hot-products?category=44&page=1"
```

**Response:**
```json
{
  "data": [
    {
      "id": 32989738329,
      "title": "Wireless Bluetooth Headphones",
      "categoryName": "Consumer Electronics",
      "price": "12.99",
      "originalPrice": "29.99",
      "rating": "95.0%",
      "orders": 5234,
      "productUrl": "https://www.aliexpress.com/item/...",
      "imageUrls": null
    }
  ]
}
```

### 2. Get Categories
**Endpoint:** `GET /aliexpress/categories`

Retrieves all available AliExpress categories.

**Example:**
```bash
curl "http://localhost:5280/aliexpress/categories"
```

**Response:**
```json
[
  {
    "category_name": "Food",
    "category_id": 2
  },
  {
    "category_name": "Consumer Electronics",
    "category_id": 44
  },
  ...
]
```

### 3. Global Trend Analysis
**Endpoint:** `GET /analysis/global-trend`

Analyzes product trends and returns top products and categories.

**Parameters:**
- `category` (required): Category ID
- `page` (optional): Page number (default: `1`)

**Example:**
```bash
curl "http://localhost:5280/analysis/global-trend?category=44&page=1"
```

**Response:**
```json
{
  "totalProducts": 12,
  "topProducts": [
    {
      "id": 123456789,
      "title": "Best Selling Product",
      "orders": 10000,
      "rating": "98.5%",
      "price": "15.99"
    }
  ],
  "topCategories": [
    {
      "categoryName": "Consumer Electronics",
      "productCount": 8,
      "totalOrders": 45000
    }
  ]
}
```

### 4. Database Query
**Endpoint:** `GET /database/products`

Queries all products stored in MongoDB.

**Example:**
```bash
curl "http://localhost:5280/database/products"
```

**Response:**
```json
{
  "status": "success",
  "count": 120,
  "data": [...]
}
```

### 5. Test Endpoint
**Endpoint:** `GET /test/products`

Debug endpoint for testing API integration.

**Example:**
```bash
curl "http://localhost:5280/test/products"
```

## 📁 Project Structure

```
Analysis Trends/
├── Program.cs                          # Main application & API endpoints
├── appsettings.json                    # Configuration (API keys, MongoDB)
├── appsettings.Development.json        # Development settings
├── Analysis Trends.csproj              # Project file with dependencies
├── RapidApiOptions.cs                  # RapidAPI configuration model
├── HotProductsModels.cs                # Product data models
├── Services/
│   ├── AliExpressRapidApiClient.cs    # RapidAPI HTTP client
│   ├── MongoDbService.cs              # MongoDB connection manager
│   └── ProductRepository.cs           # Data persistence layer
├── Models/
│   └── MongoDbOptions.cs              # MongoDB configuration
├── wwwroot/
│   └── index.html                     # Frontend web dashboard (794 lines)
└── Properties/
    └── launchSettings.json            # Launch configuration
```

## ⚙️ Configuration

### Complete appsettings.json
```json
{
  "RapidApi": {
    "ApiKey": "your_rapidapi_key_here",
    "Host": "free-aliexpress-api.p.rapidapi.com",
    "BaseUrl": "https://free-aliexpress-api.p.rapidapi.com"
  },
  "MongoDb": {
    "ConnectionString": "mongodb+srv://username:password@cluster.mongodb.net/aliexpress?retryWrites=true&w=majority",
    "Database": "aliexpress",
    "Collection": "hot_products"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### MongoDB Atlas Setup
1. Create free cluster at [mongodb.com/cloud/atlas](https://mongodb.com/cloud/atlas)
2. Create database user with read/write permissions
3. Whitelist your IP or use `0.0.0.0/0` for all IPs
4. Get connection string and update `appsettings.json`
5. **Note:** Special characters in password must be URL-encoded (e.g., `@` → `%40`)

### RapidAPI Setup
1. Sign up at [rapidapi.com](https://rapidapi.com)
2. Subscribe to **Free AliExpress API**
3. Copy your API key from the dashboard
4. Update `appsettings.json` with the key

## 💻 Usage Examples

### Using the Web Dashboard
1. Start the application: `dotnet run`
2. Open browser: `http://localhost:5280`
3. Select a category from the dropdown
4. Enter page number (default: 1)
5. Click "Search Trends" button
6. View results with auto-refresh every 40 seconds
7. Click "Stop Refresh" to pause updates

### Using the API Directly

**Get Consumer Electronics Trending Products:**
```bash
curl "http://localhost:5280/aliexpress/hot-products?category=44&page=1"
```

**Get Food Category Trends:**
```bash
curl "http://localhost:5280/aliexpress/hot-products?category=2&page=1"
```

**Analyze Global Trends:**
```bash
curl "http://localhost:5280/analysis/global-trend?category=44&page=1"
```

**Get All Categories:**
```bash
curl "http://localhost:5280/aliexpress/categories"
```

**Query MongoDB Data:**
```bash
curl "http://localhost:5280/database/products"
```

### Popular Category IDs
- **2** - Food
- **3** - Apparel & Accessories
- **6** - Home Appliances
- **7** - Computer & Office
- **13** - Home Improvement
- **15** - Home & Garden
- **18** - Sports & Entertainment
- **44** - Consumer Electronics
- **66** - Beauty & Health

## 🎨 Dashboard Features

### Main Interface
- **Category Selector** - Dropdown with 100+ categories
- **Page Input** - Navigate through product pages
- **Search Button** - Fetch latest trends
- **Stop Refresh** - Manual control for auto-updates

### Statistics Cards
1. **Global Statistics** - Total products, average rating, total orders
2. **Top Categories** - Best-performing categories by sales
3. **Hot Products** - Trending items with key metrics
4. **Top Sellers** - Highest order volume products

### Product Display
- Product title and category
- Current price vs. original price
- Order count
- Rating percentage
- Product images (when available)
- Direct links to AliExpress

### Auto-Refresh
- **40-second intervals** with countdown timer
- Visual indicator showing "🔄 Refreshing in Xs"
- Manual stop/start controls
- Preserves selected category and page

## 📊 Performance

- **Response Time:** <2 seconds per API call
- **Products per Request:** 10-12 products
- **Auto-Refresh:** Every 40 seconds
- **Database Operations:** Async fire-and-forget (non-blocking)
- **Concurrent Requests:** Supported via CORS
- **Error Recovery:** Direct exception throwing (no fallbacks)

## 🔍 Logging

Comprehensive logging throughout the application:

```
info: Analysis_Trends.Services.AliExpressRapidApiClient[0]
      Requesting: https://free-aliexpress-api.p.rapidapi.com/hot_products?category=44&page=1
      
info: Analysis_Trends.Services.AliExpressRapidApiClient[0]
      Successfully deserialized 12 products
      
info: Analysis_Trends.Services.ProductRepository[0]
      Inserted 12 products into MongoDB
```

View logs in console when running in development mode.

## 🐛 Troubleshooting

### MongoDB Connection Issues

**Error:** `The Local Security Authority cannot be contacted`

**Solution:**
- SSL/TLS authentication issue on Windows
- Update MongoDB connection string
- Verify credentials in MongoDB Atlas Dashboard
- Check IP whitelist settings
- URL-encode special characters in password

### API Returns Errors

**Error:** `Cannot read properties of undefined (reading 'map')`

**Cause:** Some RapidAPI categories have no products or malformed data

**Solution:** 
- Try different category IDs (use `/aliexpress/categories` endpoint)
- Use popular categories: 2, 44, 66, 7

### "Category parameter is required"

**Solution:** Always provide `category` parameter:
```bash
curl "http://localhost:5280/aliexpress/hot-products?category=44&page=1"
```

### 404 Not Found

**Solution:** Ensure correct endpoint paths:
- ✅ `/aliexpress/hot-products`
- ✅ `/aliexpress/categories`
- ✅ `/analysis/global-trend`
- ❌ Not: `/Products` or `/Categories` (case-sensitive)

### RapidAPI Rate Limits

Check your subscription limits at RapidAPI dashboard:
- Free tier: Limited requests per month
- Monitor usage to avoid throttling

### Build Warnings

Warning `CS8602` (null reference) can be ignored - it's handled in runtime with null-conditional operators.

## 🚀 Future Enhancements

Planned features for upcoming releases:

- [ ] **Machine Learning Integration** - Predictive analytics for trend forecasting
- [ ] **Historical Data Analysis** - Time-series charts and comparisons
- [ ] **Redis Caching** - Improve response times for frequent queries
- [ ] **Authentication & Authorization** - User accounts and API keys
- [ ] **Advanced Filtering** - Price ranges, rating filters, search
- [ ] **Scheduled Jobs** - Automated data collection with Hangfire
- [ ] **Export Features** - CSV/Excel export for trend reports
- [ ] **Multi-Language Support** - Internationalization (i18n)
- [ ] **Mobile App** - React Native or Flutter companion app
- [ ] **Email Notifications** - Alerts for trending products
- [ ] **Advanced Analytics** - Profit margin calculations, competitor analysis
- [ ] **Docker Support** - Containerization for easy deployment

## 🤝 Contributing

Contributions are welcome! Here's how:

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/amazing-feature
   ```
3. **Commit your changes**
   ```bash
   git commit -m 'Add amazing feature'
   ```
4. **Push to branch**
   ```bash
   git push origin feature/amazing-feature
   ```
5. **Open a Pull Request**

### Coding Standards
- Follow C# naming conventions
- Add XML documentation for public methods
- Include unit tests for new features
- Update README for significant changes

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## ⚠️ Disclaimer

This application uses real-time data from AliExpress via the Free AliExpress API provided by RapidAPI. 

**Important Notes:**
- Data accuracy depends on the external API
- Trends are based on current snapshots, not predictions
- Use insights at your own discretion for business decisions
- Not affiliated with or endorsed by AliExpress
- Subject to RapidAPI terms of service and rate limits

## 📞 Support & Contact

Need help or have questions?

- 📧 **Email:** [Your Email]
- 🐛 **Issues:** [GitHub Issues](https://github.com/MalithDN/aliexpress-trend-predictor/issues)
- 💬 **Discussions:** [GitHub Discussions](https://github.com/MalithDN/aliexpress-trend-predictor/discussions)
- 🌐 **Website:** [Your Portfolio]
- 💼 **LinkedIn:** [MalithDN](https://linkedin.com/in/malithdn)

## 🙏 Acknowledgments

- **RapidAPI** - For providing the Free AliExpress API
- **AliExpress** - For the real-time product data
- **MongoDB Atlas** - For cloud database hosting
- **Microsoft** - For the excellent ASP.NET Core framework
- **Open Source Community** - For inspiration and tools

## 📈 Project Stats

- **Lines of Code:** ~2,500+
- **API Endpoints:** 5
- **Dependencies:** 2 NuGet packages
- **Frontend:** 794 lines of HTML/CSS/JS
- **Database:** MongoDB with BsonDocument storage
- **Response Time:** <2 seconds average

## 🎯 Use Cases

This application is perfect for:

1. **E-commerce Entrepreneurs** - Discover trending products to sell
2. **Market Researchers** - Analyze consumer behavior patterns
3. **Dropshippers** - Find profitable products quickly
4. **Data Analysts** - Study e-commerce trends and metrics
5. **Developers** - Learn API integration and full-stack development
6. **Students** - Portfolio project for web development courses

---

**⭐ Star this repo** if you find it helpful!

**🔗 Connect with me:** [GitHub](https://github.com/MalithDN) | [LinkedIn](https://linkedin.com/in/malithdn)

Made with ❤️ and ☕ by **MalithDN**

---

*Last Updated: November 26, 2025*
