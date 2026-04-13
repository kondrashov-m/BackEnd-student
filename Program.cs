using MiddlewareDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

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

app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html>
<head>
    <title>Главная</title>
    <link rel='stylesheet' href='/css/style.css'>
    <script src='/js/script.js'></script>
</head>
<body>
    <h1>Статические файлы в ASP.NET Core</h1>
    <img src='/images/logo.png' alt='Логотип' width='200'>
    <p><a href='/html/about.html'>О проекте</a></p>
</body>
</html>
""", "text/html"));

app.Run();