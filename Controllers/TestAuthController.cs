using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd_student.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAuthController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult PublicEndpoint()
    {
        return Ok(new { message = "Это публичный эндпоинт. Доступен всем." });
    }

    [Authorize]
    [HttpGet("user")]
    public IActionResult UserEndpoint()
    {
        var username = User.Identity?.Name;
        var userId = User.FindFirst("UserId")?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        
        return Ok(new 
        { 
            message = $"Привет, {username}! Ты авторизован.",
            userId = userId,
            role = role
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminEndpoint()
    {
        return Ok(new { message = "Только для администраторов!" });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("manager")]
    public IActionResult ManagerEndpoint()
    {
        return Ok(new { message = "Доступно для Admin и Manager" });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(new
        {
            username = User.Identity?.Name,
            isAuthenticated = User.Identity?.IsAuthenticated,
            claims = claims
        });
    }
}