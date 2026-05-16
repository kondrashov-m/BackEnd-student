using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using BackEnd_student.Models;
using BackEnd_student.Services;

namespace BackEnd_student.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CacheDemoController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CacheDemoController> _logger;

    private static List<Product> _products = new();

    public CacheDemoController(
        IMemoryCache memoryCache,
        ICacheService cacheService,
        ILogger<CacheDemoController> logger)
    {
        _memoryCache = memoryCache;
        _cacheService = cacheService;
        _logger = logger;
        
        if (!_products.Any())
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Ноутбук", Price = 1000, Stock = 10 },
                new Product { Id = 2, Name = "Мышь", Price = 50, Stock = 100 },
                new Product { Id = 3, Name = "Клавиатура", Price = 150, Stock = 50 }
            };
        }
    }

    [HttpGet("no-cache")]
    public IActionResult GetProductsNoCache()
    {
        _logger.LogInformation("GET /no-cache - Запрос без кеша");
        return Ok(new
        {
            source = "Database (no cache)",
            data = _products,
            timestamp = DateTime.Now,
            count = _products.Count
        });
    }

    [HttpGet("memory-cache")]
    public IActionResult GetProductsWithMemoryCache()
    {
        const string cacheKey = "all_products";
        
        _logger.LogInformation("GET /memory-cache - Проверка кеша...");
        
        var cachedData = _memoryCache.Get(cacheKey);
        
        if (cachedData != null)
        {
            _logger.LogInformation("Данные получены из кеша памяти");
            return Ok(new
            {
                source = "In-Memory Cache",
                data = cachedData,
                timestamp = DateTime.Now,
                cached = true
            });
        }
        
        _logger.LogInformation("Кеш пуст, загружаем из БД");
        
        Thread.Sleep(500);
        
        _memoryCache.Set(cacheKey, _products, TimeSpan.FromSeconds(30));
        
        return Ok(new
        {
            source = "Database (cached for 30s)",
            data = _products,
            timestamp = DateTime.Now,
            cached = false
        });
    }

    [HttpGet("response-cache")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
    public IActionResult GetProductsResponseCache()
    {
        _logger.LogInformation("GET /response-cache - Запрос с HTTP кешем");
        
        return Ok(new
        {
            source = "HTTP Response Cache (30s)",
            data = _products,
            timestamp = DateTime.Now,
            cacheHeader = "Cache-Control: public,max-age=30"
        });
    }

    [HttpGet("service-cache")]
    public IActionResult GetProductsWithServiceCache()
    {
        const string cacheKey = "products_service";
        
        var cachedData = _cacheService.Get<List<Product>>(cacheKey);
        
        if (cachedData != null)
        {
            return Ok(new
            {
                source = "Service Cache",
                data = cachedData,
                timestamp = DateTime.Now,
                cached = true
            });
        }
        
        _cacheService.Set(cacheKey, _products, TimeSpan.FromMinutes(1));
        
        return Ok(new
        {
            source = "Database (cached via service)",
            data = _products,
            timestamp = DateTime.Now,
            cached = false
        });
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        product.CreatedAt = DateTime.UtcNow;
        _products.Add(product);
        
        _memoryCache.Remove("all_products");
        _cacheService.Remove("products_service");
        
        _logger.LogInformation("Продукт добавлен, кеш очищен");
        
        return Ok(new
        {
            message = "Продукт добавлен, кеш инвалидирован",
            product = product
        });
    }

    [HttpDelete("cache")]
    public IActionResult ClearCache()
    {
        _memoryCache.Remove("all_products");
        _cacheService.Remove("products_service");
        
        return Ok(new { message = "Кеш очищен" });
    }

    [HttpGet("info")]
    public IActionResult GetCacheInfo()
    {
        var memoryStats = new
        {
            productsInCache = _memoryCache.Get("all_products") != null,
            productsServiceCache = _cacheService.Exists("products_service"),
            cacheKeys = new[] { "all_products", "products_service" }
        };
        
        return Ok(new
        {
            memoryCache = memoryStats,
            timestamp = DateTime.Now,
            totalProducts = _products.Count
        });
    }
}