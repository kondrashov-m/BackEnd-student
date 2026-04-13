using System.Diagnostics;

namespace LoggingDemo.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.LogInformation("Запрос начат: {Method} {Path}", method, path);

        await _next(context);

        stopwatch.Stop();
        _logger.LogInformation("Запрос завершён: {Method} {Path} -> {StatusCode} за {ElapsedMs}ms",
            method, path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
    }
}
