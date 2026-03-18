// Лабораторная работа №5
// Кондрашов Михаил, 241-331
using System.Text;
using System.Text.Json;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Добавление контроллеров для API маршрутизации
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "Routing Demo API", 
        Version = "v1",
        Description = "API для демонстрации возможностей маршрутизации"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Routing Demo API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/html", () =>
{
    string html = @"<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>HTML ответ</title>
    <style>
        body { font-family: Arial; background: #f0f0f0; padding: 20px; }
        .card { background: white; padding: 20px; border-radius: 10px; max-width: 600px; margin: 0 auto; }
        h1 { color: #333; }
    </style>
</head>
<body>
    <div class='card'>
        <h1>Лабораторная работа №3</h1>
        <p><b>Тип ответа:</b> text/html</p>
        <p><b>Студент:</b> Кондрашов Михаил</p>
        <p><b>Группа:</b> 241-331</p>
        <p><b>Время:</b> " + DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy") + @"</p>
    </div>
</body>
</html>";
    
    return Results.Text(html, "text/html", Encoding.UTF8);
});

app.MapGet("/", () =>
{
    string html = @"<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Лабораторная работа №3 и №5</title>
    <style>
        body { font-family: Arial; padding: 20px; background: #f5f5f5; }
        .container { max-width: 1200px; margin: 0 auto; }
        h1 { color: #333; }
        h2 { color: #666; margin-top: 30px; }
        ul { list-style: none; padding: 0; }
        li { margin: 10px 0; }
        a { display: inline-block; padding: 10px 20px; background: #007bff; color: white; text-decoration: none; border-radius: 5px; width: 300px; }
        a:hover { background: #0056b3; }
        .api-section { margin-top: 30px; padding: 20px; background: #e9ecef; border-radius: 5px; }
        .api-section a { background: #28a745; }
        .api-section a:hover { background: #218838; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Лабораторные работы №3 и №5</h1>
        <h2>Студент: Кондрашов Михаил, группа 241-331</h2>
        
        <h2>Лабораторная работа №3 (различные типы ответов):</h2>
        <ul>
            <li><a href='/html'>1. HTML-контент (text/html)</a></li>
            <li><a href='/plain'>2. Текстовый ответ (text/plain)</a></li>
            <li><a href='/json'>3. JSON данные (application/json)</a></li>
            <li><a href='/xml'>4. XML-ответ (application/xml)</a></li>
            <li><a href='/csv'>5. CSV-данные (text/csv)</a></li>
            <li><a href='/binary'>6. Бинарные данные (application/octet-stream)</a></li>
            <li><a href='/image'>7. Изображение (image/png)</a></li>
            <li><a href='/pdf'>8. PDF-файл (application/pdf)</a></li>
            <li><a href='/redirect'>9. Редирект 302 → /html</a></li>
            <li><a href='/redirect-permanent'>10. Редирект 301 → /plain</a></li>
        </ul>
        
        <div class='api-section'>
            <h2>Лабораторная работа №5 (маршрутизация API):</h2>
            <ul>
                <li><a href='/swagger'>Открыть Swagger UI для тестирования API</a></li>
                <li><a href='/api/products'>GET /api/products (все продукты)</a></li>
                <li><a href='/api/students'>GET /api/students (все студенты)</a></li>
                <li><a href='/api/orders'>GET /api/orders (все заказы)</a></li>
            </ul>
        </div>
    </div>
</body>
</html>";
    
    return Results.Text(html, "text/html", Encoding.UTF8);
});

// Добавление маршрутизации для контроллеров
app.MapControllers();

app.Run();