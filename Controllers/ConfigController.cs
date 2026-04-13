using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BackEnd_student.Models;

namespace BackEnd_student.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppSettings _appSettings;
    private readonly IWebHostEnvironment _environment;

    public ConfigController(
        IConfiguration configuration,
        IOptions<AppSettings> appSettings,
        IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _appSettings = appSettings.Value;
        _environment = environment;
    }

    [HttpGet]
    public IActionResult GetConfig()
    {
        return Ok(new
        {
            Environment = _environment.EnvironmentName,
            AppName = _appSettings.AppName,
            Version = _appSettings.Version,
            MaxItems = _appSettings.MaxItems,
            ConnectionString = _configuration.GetConnectionString("DefaultConnection"),
            LoggingLevel = _configuration["Logging:LogLevel:Default"]
        });
    }
}
