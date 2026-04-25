using Microsoft.AspNetCore.Mvc;

namespace BackEnd_student.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new 
        { 
            message = "CORS работает!",
            timestamp = DateTime.UtcNow,
            server = "ASP.NET Core"
        });
    }

    [HttpPost]
    public IActionResult Post([FromBody] object data)
    {
        return Ok(new 
        { 
            message = "POST запрос успешно выполнен",
            receivedData = data,
            timestamp = DateTime.UtcNow
        });
    }

    [HttpPut]
    public IActionResult Put([FromBody] object data)
    {
        return Ok(new 
        { 
            message = "PUT запрос успешно выполнен",
            receivedData = data
        });
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok(new { message = "DELETE запрос успешно выполнен" });
    }
}