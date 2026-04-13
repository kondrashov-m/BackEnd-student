using BackEnd_student.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => Results.Content(
"<!DOCTYPE html>" +
"<html>" +
"<head>" +
"<title>Laboratornaya rabota 9</title>" +
"<style>" +
"body{font-family:Arial;text-align:center;padding:50px;background:#667eea;margin:0;display:flex;justify-content:center;align-items:center;min-height:100vh;}" +
".container{background:white;border-radius:20px;padding:40px;box-shadow:0 10px 40px rgba(0,0,0,0.2);max-width:500px;width:100%;}" +
"h1{color:#333;margin-bottom:10px;}" +
"h2{color:#666;font-size:18px;margin-bottom:30px;font-weight:normal;}" +
".links{display:flex;flex-direction:column;gap:15px;}" +
".btn{display:inline-block;background:#667eea;color:white;text-decoration:none;padding:12px 24px;border-radius:10px;font-size:16px;transition:all 0.3s ease;}" +
".btn:hover{background:#5a67d8;transform:scale(1.05);}" +
".btn-secondary{background:#48bb78;}" +
".btn-secondary:hover{background:#38a169;}" +
".env{margin-top:30px;padding:15px;background:#f0f0f0;border-radius:10px;font-size:14px;color:#555;}" +
".env span{font-weight:bold;color:#667eea;}" +
"</style>" +
"</head>" +
"<body>" +
"<div class='container'>" +
"<h1>Laboratornaya rabota 9</h1>" +
"<h2>Konfiguratsiya veb-prilozheniya</h2>" +
"<div class='links'>" +
"<a href='/config' class='btn'>/config - Prosmotr konfiguratsii</a>" +
"<a href='/api/config' class='btn btn-secondary'>/api/config - API endpoint</a>" +
"</div>" +
"<div class='env'>Tekushchaya sreda: <span id='envName'>Zagruzka...</span></div>" +
"</div>" +
"<script>fetch('/config').then(res=>res.json()).then(data=>{document.getElementById('envName').textContent=data.environment;}).catch(()=>{document.getElementById('envName').textContent='Development';});</script>" +
"</body>" +
"</html>", "text/html; charset=utf-8"));

app.MapGet("/config", (IConfiguration config, IOptions<AppSettings> appSettings, IWebHostEnvironment env) =>
{
    return Results.Ok(new
    {
        Environment = env.EnvironmentName,
        AppName = appSettings.Value.AppName,
        Version = appSettings.Value.Version,
        MaxItems = appSettings.Value.MaxItems,
        ConnectionString = config.GetConnectionString("DefaultConnection"),
        LoggingLevel = config["Logging:LogLevel:Default"]
    });
});

app.Run();
