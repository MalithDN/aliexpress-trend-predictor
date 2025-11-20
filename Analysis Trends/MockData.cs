namespace Analysis_Trends
{
    public static class MockData
    {
        public static List<HotProduct> GetMockProducts(string categoryId)
        {
            return new List<HotProduct>
            {
                new HotProduct
                {
                    Id = 32989738329,
                    Title = "Wireless Bluetooth Headphones",
                    CategoryName = "Consumer Electronics",
                    Price = "12.99",
                    OriginalPrice = "29.99",
                    Rating = "95.0%",
                    Orders = 5234,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test1.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738330,
                    Title = "USB-C Fast Charging Cable",
                    CategoryName = "Consumer Electronics",
                    Price = "2.99",
                    OriginalPrice = "8.99",
                    Rating = "97.0%",
                    Orders = 8932,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test2.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738331,
                    Title = "Phone Screen Protector",
                    CategoryName = "Consumer Electronics",
                    Price = "1.49",
                    OriginalPrice = "4.99",
                    Rating = "93.0%",
                    Orders = 12450,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test3.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738332,
                    Title = "LED Desk Lamp",
                    CategoryName = "Lights & Lighting",
                    Price = "8.99",
                    OriginalPrice = "19.99",
                    Rating = "96.0%",
                    Orders = 6782,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test4.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738333,
                    Title = "Portable Phone Stand",
                    CategoryName = "Consumer Electronics",
                    Price = "3.99",
                    OriginalPrice = "9.99",
                    Rating = "94.0%",
                    Orders = 9123,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test5.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738334,
                    Title = "Wireless Mouse",
                    CategoryName = "Computer & Office",
                    Price = "4.99",
                    OriginalPrice = "12.99",
                    Rating = "95.0%",
                    Orders = 7645,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test6.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738335,
                    Title = "Phone Case Pack (5pcs)",
                    CategoryName = "Consumer Electronics",
                    Price = "5.99",
                    OriginalPrice = "14.99",
                    Rating = "92.0%",
                    Orders = 10234,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test7.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738336,
                    Title = "Bluetooth Speaker",
                    CategoryName = "Consumer Electronics",
                    Price = "14.99",
                    OriginalPrice = "34.99",
                    Rating = "96.0%",
                    Orders = 8567,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test8.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738337,
                    Title = "USB Hub 7-in-1",
                    CategoryName = "Computer & Office",
                    Price = "9.99",
                    OriginalPrice = "24.99",
                    Rating = "94.0%",
                    Orders = 5432,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test9.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                },
                new HotProduct
                {
                    Id = 32989738338,
                    Title = "USB Rechargeable LED Flashlight",
                    CategoryName = "Lights & Lighting",
                    Price = "6.99",
                    OriginalPrice = "16.99",
                    Rating = "95.0%",
                    Orders = 7234,
                    ImageUrls = new ImageUrls 
                    { 
                        String = new List<string> { "https://ae01.alicdn.com/kf/test10.jpg" } 
                    },
                    ProductUrl = "https://www.aliexpress.com/item/test"
                }
            };
        }

        public static List<object> GetMockCategories()
        {
            return new List<object>
            {
                new { category_name = "Food", category_id = 2 },
                new { category_name = "Apparel & Accessories", category_id = 3 },
                new { category_name = "Home Appliances", category_id = 6 },
                new { category_name = "Computer & Office", category_id = 7 },
                new { category_name = "Home Improvement", category_id = 13 },
                new { category_name = "Home & Garden", category_id = 15 },
                new { category_name = "Sports & Entertainment", category_id = 18 },
                new { category_name = "Office & School Supplies", category_id = 21 },
                new { category_name = "Toys & Hobbies", category_id = 26 },
                new { category_name = "Security & Protection", category_id = 30 },
                new { category_name = "Automobiles, Parts & Accessories", category_id = 34 },
                new { category_name = "Jewelry & Accessories", category_id = 36 },
                new { category_name = "Lights & Lighting", category_id = 39 },
                new { category_name = "Consumer Electronics", category_id = 44 },
                new { category_name = "Beauty & Health", category_id = 66 },
            };
        }
    }
}
