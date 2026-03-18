using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class ProductsController : ControllerBase
{
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Ноутбук", Price = 1200.50m, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-30) },
        new Product { Id = 2, Name = "Мышь", Price = 25.99m, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-20) },
        new Product { Id = 3, Name = "Клавиатура", Price = 89.90m, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-15) },
        new Product { Id = 4, Name = "Монитор", Price = 350.00m, Category = "Electronics", CreatedDate = DateTime.Now.AddDays(-10) },
        new Product { Id = 5, Name = "Стол", Price = 450.00m, Category = "Furniture", CreatedDate = DateTime.Now.AddDays(-5) }
    };

    [HttpGet]
    public IActionResult GetProducts(
        [FromQuery] string? category = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sort = "name")
    {
        var query = _products.AsEnumerable();

        if (!string.IsNullOrEmpty(category))
            query = query.Where(p => p.Category.Contains(category, StringComparison.OrdinalIgnoreCase));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);
        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        query = sort.ToLower() switch
        {
            "price" => query.OrderBy(p => p.Price),
            "pricedesc" => query.OrderByDescending(p => p.Price),
            "date" => query.OrderBy(p => p.CreatedDate),
            "datedesc" => query.OrderByDescending(p => p.CreatedDate),
            _ => query.OrderBy(p => p.Name)
        };

        var totalItems = query.Count();
        var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var result = new
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            Items = items,
            Filter = new { Category = category, MinPrice = minPrice, MaxPrice = maxPrice },
            Sort = sort
        };

        return Ok(result);
    }

    [HttpGet("{id:int}")] 
    public IActionResult GetProductById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound(new { Message = $"Продукт с ID {id} не найден" });

        return Ok(product);
    }

    [HttpGet("by-name/{name:minlength(3)}")]
    public IActionResult GetProductByName(string name)
    {
        var products = _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        
        if (!products.Any())
            return NotFound(new { Message = $"Продукты с именем, содержащим '{name}', не найдены" });

        return Ok(products);
    }

    [HttpGet("by-date/{date:datetime}")]
    public IActionResult GetProductsByDate(DateTime date)
    {
        var products = _products.Where(p => p.CreatedDate.Date == date.Date).ToList();
        return Ok(products);
    }

  
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        product.Id = _products.Max(p => p.Id) + 1;
        product.CreatedDate = DateTime.Now;
        _products.Add(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest(new { Message = "ID в URL не совпадает с ID в теле запроса" });

        var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
            return NotFound(new { Message = $"Продукт с ID {id} не найден" });

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Category = product.Category;

        return Ok(existingProduct);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound(new { Message = $"Продукт с ID {id} не найден" });

        _products.Remove(product);
        return NoContent();
    }
}