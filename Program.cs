using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using BackEnd_student.Metrics;

var builder = WebApplication.CreateBuilder(args);


var serviceName = "BackEnd-student";
var serviceVersion = "1.0.0";

var meterProvider = Sdk.CreateMeterProviderBuilder()
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService(serviceName, serviceVersion: serviceVersion)
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = "development",
            ["service.version"] = serviceVersion
        }))
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddRuntimeInstrumentation()
    .AddPrometheusExporter()
    .Build();

builder.Services.AddSingleton(meterProvider);
builder.Services.AddSingleton<AppMetrics>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();