using Microsoft.AspNetCore.Mvc;
using BackEnd_student.Models;

namespace BackEnd_student.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private static List<Item> _items = new()
    {
        new Item { Id = 1, Name = "Item 1", Description = "First item", CreatedAt = DateTime.UtcNow },
        new Item { Id = 2, Name = "Item 2", Description = "Second item", CreatedAt = DateTime.UtcNow }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_items);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Item item)
    {
        item.Id = _items.Max(i => i.Id) + 1;
        item.CreatedAt = DateTime.UtcNow;
        _items.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Item item)
    {
        var existing = _items.FirstOrDefault(i => i.Id == id);
        if (existing == null)
            return NotFound();

        existing.Name = item.Name;
        existing.Description = item.Description;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            return NotFound();

        _items.Remove(item);
        return NoContent();
    }
}