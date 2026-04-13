using MiddlewareDemo.Middlewares;
using Microsoft.AspNetCore.StaticFiles;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin);
    options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".css"] = "text/css; charset=utf-8";
provider.Mappings[".js"] = "application/javascript; charset=utf-8";
provider.Mappings[".html"] = "text/html; charset=utf-8";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

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

app.MapGet("/", () => Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
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
", "text/html; charset=utf-8"));



app.Run();
