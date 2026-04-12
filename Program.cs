using MiddlewareDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<BlockPathMiddleware>();
app.UseMiddleware<RequestTraceMiddleware>();
app.UseMiddleware<EndpointTimingMiddleware>();

app.MapGet("/ping", () => "pong");

app.MapGet("/trace", (HttpContext ctx) =>
{
    var traceId = ctx.Items["TraceId"]?.ToString() ?? "not found";
    return Results.Ok(new { traceId });
});

app.MapGet("/error", () =>
{
    throw new Exception("Test exception from /error endpoint");
});

app.Run();