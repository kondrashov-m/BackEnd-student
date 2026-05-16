using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd_student.Metrics;

namespace BackEnd_student.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetricsDemoController : ControllerBase
{
    private readonly AppMetrics _metrics;
    private readonly ILogger<MetricsDemoController> _logger;

    public MetricsDemoController(AppMetrics metrics, ILogger<MetricsDemoController> logger)
    {
        _metrics = metrics;
        _logger = logger;
    }

    [HttpGet("hello")]
    public IActionResult Hello()
    {
        var stopwatch = Stopwatch.StartNew();
        
        _metrics.IncrementActiveRequests();
        _metrics.RecordRequest();
        
        var randomDelay = new Random().Next(10, 100);
        Thread.Sleep(randomDelay);
        
        _metrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        _metrics.DecrementActiveRequests();
        
        return Ok(new { message = "Hello from monitored endpoint!", delay = randomDelay });
    }


    [HttpGet("heavy")]
    public IActionResult Heavy()
    {
        var stopwatch = Stopwatch.StartNew();
        
        _metrics.IncrementActiveRequests();
        _metrics.RecordRequest();
        
        var randomDelay = new Random().Next(200, 1000);
        Thread.Sleep(randomDelay);
        
        _metrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        _metrics.DecrementActiveRequests();
        
        return Ok(new { message = "Heavy operation completed!", delay = randomDelay });
    }


    [HttpGet("error")]
    public IActionResult Error()
    {
        _metrics.IncrementActiveRequests();
        _metrics.RecordRequest();
        _metrics.DecrementActiveRequests();
        
        _logger.LogError("Error endpoint called - returning 500");
        return StatusCode(500, new { error = "Internal server error" });
    }

    [HttpGet("stress")]
    public async Task<IActionResult> Stress(int count = 10)
    {
        var results = new List<object>();
        
        for (int i = 0; i < count; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            _metrics.IncrementActiveRequests();
            _metrics.RecordRequest();
            
            await Task.Delay(new Random().Next(5, 20));
            
            _metrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
            _metrics.DecrementActiveRequests();
            
            results.Add(new { request = i + 1, duration = stopwatch.Elapsed.TotalMilliseconds });
        }
        
        return Ok(new { totalRequests = count, results });
    }

    [HttpGet("info")]
    public IActionResult Info()
    {
        return Ok(new
        {
            metrics = new
            {
                counter = "app.requests.total - счётчик запросов",
                gauge = "app.requests.active - текущие активные запросы",
                histogram = "app.requests.duration - распределение времени"
            },
            endpoints = new[]
            {
                "GET /api/MetricsDemo/hello",
                "GET /api/MetricsDemo/heavy", 
                "GET /api/MetricsDemo/error",
                "GET /api/MetricsDemo/stress?count=10",
                "GET /api/MetricsDemo/info"
            },
            prometheusEndpoint = "/metrics"
        });
    }
}