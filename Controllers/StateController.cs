using Microsoft.AspNetCore.Mvc;

namespace BackEnd_student.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StateController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StateController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    [HttpPost("set-cookie")]
    public IActionResult SetCookie([FromBody] CookieData data)
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            SameSite = SameSiteMode.Lax
        };
        Response.Cookies.Append("UserName", data.Value, options);
        return Ok(new { message = "Cookie сохранён", value = data.Value });
    }

    [HttpGet("get-cookie")]
    public IActionResult GetCookie()
    {
        var userName = Request.Cookies["UserName"];
        if (string.IsNullOrEmpty(userName))
        {
            return Ok(new { exists = false, message = "Cookie не найден" });
        }
        return Ok(new { exists = true, value = userName });
    }

    [HttpDelete("delete-cookie")]
    public IActionResult DeleteCookie()
    {
        Response.Cookies.Delete("UserName");
        return Ok(new { message = "Cookie удалён" });
    }


    [HttpPost("set-session")]
    public IActionResult SetSession([FromBody] SessionData data)
    {
        HttpContext.Session.SetString("UserData", data.Value);
        return Ok(new { message = "Данные сохранены в сессии", value = data.Value });
    }

    [HttpGet("get-session")]
    public IActionResult GetSession()
    {
        var userData = HttpContext.Session.GetString("UserData");
        if (string.IsNullOrEmpty(userData))
        {
            return Ok(new { exists = false, message = "Сессия пуста" });
        }
        return Ok(new { exists = true, value = userData });
    }

    [HttpDelete("clear-session")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        return Ok(new { message = "Сессия очищена" });
    }
}

public class CookieData
{
    public string Value { get; set; } = string.Empty;
}

public class SessionData
{
    public string Value { get; set; } = string.Empty;
}