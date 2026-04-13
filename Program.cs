using Serilog;
using LoggingDemo.Middlewares;
using LoggingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Настройка Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();

app.MapGet("/info", (ILogger<Program> logger) =>
{
    logger.LogInformation("Это информационное сообщение");
    return Results.Ok("Info logged");
});

app.MapGet("/warning", (ILogger<Program> logger) =>
{
    logger.LogWarning("Это предупреждение");
    return Results.Ok("Warning logged");
});

app.MapGet("/error", (ILogger<Program> logger) =>
{
    logger.LogError("Это сообщение об ошибке");
    return Results.BadRequest("Error logged");
});

app.MapGet("/debug", (ILogger<Program> logger) =>
{
    logger.LogDebug("Это отладочное сообщение");
    return Results.Ok("Debug logged");
});

app.MapPost("/user", (string name, IUserService userService, ILogger<Program> logger) =>
{
    logger.LogInformation("POST /user вызван с name={Name}", name);
    userService.CreateUser(name);
    return Results.Ok($"User {name} created");
});

app.MapGet("/", () => "Логирование работает. Попробуй /info, /warning, /error, /debug");

app.Run();
