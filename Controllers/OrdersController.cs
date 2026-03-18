using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly List<Order> _orders = new()
    {
        new Order { 
            Id = Guid.NewGuid(), 
            OrderNumber = "ORD-001", 
            ProductId = 1, 
            Quantity = 2, 
            OrderDate = DateTime.Now.AddDays(-5), 
            Status = "Delivered" 
        },
        new Order { 
            Id = Guid.NewGuid(), 
            OrderNumber = "ORD-002", 
            ProductId = 2, 
            Quantity = 5, 
            OrderDate = DateTime.Now.AddDays(-3), 
            Status = "Shipped" 
        },
        new Order { 
            Id = Guid.NewGuid(), 
            OrderNumber = "ORD-003", 
            ProductId = 3, 
            Quantity = 1, 
            OrderDate = DateTime.Now.AddDays(-1), 
            Status = "Processing" 
        }
    };

    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(_orders);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetOrderById(Guid id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound(new { Message = $"Заказ с ID {id} не найден" });

        return Ok(order);
    }

    [HttpGet("number/{orderNumber:minlength(5)}")]
    public IActionResult GetOrderByNumber(string orderNumber)
    {
        var order = _orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber, StringComparison.OrdinalIgnoreCase));
        if (order == null)
            return NotFound(new { Message = $"Заказ с номером {orderNumber} не найден" });

        return Ok(order);
    }

    [HttpGet("date/{date:datetime}")]
    public IActionResult GetOrdersByDate(DateTime date)
    {
        var orders = _orders.Where(o => o.OrderDate.Date == date.Date).ToList();
        return Ok(orders);
    }

    [HttpGet("status/{status}")]
    public IActionResult GetOrdersByStatus(string status)
    {
        var orders = _orders.Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(orders);
    }

    [HttpGet("recent/{days:int?}")]
    public IActionResult GetRecentOrders(int? days = 7)
    {
        var cutoffDate = DateTime.Now.AddDays(-(days ?? 7));
        var orders = _orders.Where(o => o.OrderDate >= cutoffDate).ToList();
        
        return Ok(new
        {
            Days = days ?? 7,
            Count = orders.Count,
            Orders = orders
        });
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        order.Id = Guid.NewGuid();
        order.OrderDate = DateTime.Now;
        order.Status = "Processing";
        _orders.Add(order);

        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateOrderStatus(Guid id, [FromBody] string status)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound(new { Message = $"Заказ с ID {id} не найден" });

        order.Status = status;
        return Ok(order);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteOrder(Guid id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound(new { Message = $"Заказ с ID {id} не найден" });

        _orders.Remove(order);
        return NoContent();
    }
}