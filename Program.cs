using DependencyInjectionDemo.Repositories;
using DependencyInjectionDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ========== DI Registration ==========
// Repository: Singleton (хранит данные в памяти между запросами)
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

// Service: Scoped (один экземпляр на HTTP-запрос)
builder.Services.AddScoped<IProductService, ProductService>();
// ======================================

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();