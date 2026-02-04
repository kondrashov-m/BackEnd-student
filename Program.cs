// ============================================================================
// Лабораторная работа №1
// Дисциплина: BackEnd-разработка
// Тема: Создание приложения на основе класса WebApplication
// Студент: Кондрашов Михаил Иванович, группа 241-331
// Дата: 03.02.2026
// ============================================================================

// Создание и конфигурация приложения
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware конфигурация
app.UseStaticFiles();
app.UseRouting();

// ==================== МАРШРУТЫ ПРИЛОЖЕНИЯ ====================

// Маршрут 1: Главная страница
app.MapGet("/", () =>
{
    return """
    <!DOCTYPE html>
    <html lang='ru'>
    <head>
        <meta charset='utf-8'>
        <title>Лабораторная работа №1</title>
        <style>
            body {
                font-family: Arial, sans-serif;
                max-width: 800px;
                margin: 0 auto;
                padding: 20px;
                background-color: #f5f5f5;
            }
            .header {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                color: white;
                padding: 30px;
                border-radius: 10px;
                text-align: center;
                margin-bottom: 30px;
            }
            .nav {
                background: white;
                padding: 15px;
                border-radius: 8px;
                margin: 20px 0;
                box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            }
            .nav a {
                margin-right: 15px;
                color: #0066cc;
                text-decoration: none;
                padding: 8px 15px;
                border-radius: 5px;
                transition: background 0.3s;
            }
            .nav a:hover {
                background: #e6f2ff;
            }
            .card {
                background: white;
                padding: 20px;
                border-radius: 8px;
                margin: 15px 0;
                box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            }
            .student-info {
                background: #e8f4f8;
                border-left: 4px solid #3498db;
            }
            .api-demo {
                background: #f0f8ff;
                border: 1px dashed #0066cc;
            }
        </style>
    </head>
    <body>
        <div class="header">
            <h1>Лабораторная работа №1</h1>
            <p>BackEnd-разработка: Создание приложения на основе WebApplication</p>
        </div>
        
        <div class="card student-info">
            <h2>????? Информация о студенте</h2>
            <p><strong>ФИО:</strong> Кондрашов Михаил Иванович</p>
            <p><strong>Группа:</strong> 241-331</p>
            <p><strong>Направление:</strong> 09.03.02 «Информационные системы и технологии»</p>
            <p><strong>Дата выполнения:</strong> 03.02.2026</p>
        </div>
        
        <div class="nav">
            <h3>?? Навигация по проекту:</h3>
            <a href="/">?? Главная</a>
            <a href="/about">?? О проекте</a>
            <a href="/university">?? Университет</a>
            <a href="/api/data">?? API Данные</a>
            <a href="/patterns">?? Паттерны</a>
            <a href="/lab-info">?? Детали работы</a>
        </div>
        
        <div class="card">
            <h2>?? Цель работы</h2>
            <p>Ознакомиться с базовыми шагами создания веб-приложения на основе класса <strong>WebApplication</strong> в ASP.NET Core.</p>
        </div>
        
        <div class="card">
            <h2>?? Реализованные функции</h2>
            <ul>
                <li>? Создание проекта на базе WebApplication</li>
                <li>? Несколько страниц с уникальным содержимым</li>
                <li>? API endpoint с возвратом JSON</li>
                <li>? Маршрутизация с помощью MapGet</li>
                <li>? Комментарии к ключевому коду</li>
                <li>? Адаптивный дизайн</li>
            </ul>
        </div>
        
        <div class="card api-demo">
            <h2>?? Тестирование API</h2>
            <p>Для проверки работы API используйте следующие маршруты:</p>
            <ul>
                <li><a href="/api/data">/api/data</a> - Основные данные проекта</li>
                <li><a href="/api/student">/api/student</a> - Информация о студенте в JSON</li>
                <li><a href="/api/patterns">/api/patterns</a> - Список паттернов</li>
            </ul>
        </div>
        
        <div class="card">
            <h2>?? Технологии</h2>
            <ul>
                <li>ASP.NET Core 8.0</li>
                <li>C# 12.0</li>
                <li>Minimal API</li>
                <li>WebApplication класс</li>
                <li>Raw String Literals</li>
            </ul>
        </div>
    </body>
    </html>
    """;
});

// Маршрут 2: О проекте
app.MapGet("/about", () =>
{
    return """
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'>
        <title>О проекте</title>
        <style>
            body { font-family: Arial; max-width: 800px; margin: 0 auto; padding: 20px; }
            .back-button { display: inline-block; margin-bottom: 20px; padding: 10px 15px; 
                          background: #0066cc; color: white; text-decoration: none; border-radius: 5px; }
            .tech-grid { display: grid; grid-template-columns: repeat(2, 1fr); gap: 15px; margin: 20px 0; }
            .tech-item { background: #f0f0f0; padding: 15px; border-radius: 5px; }
        </style>
    </head>
    <body>
        <a href="/" class="back-button">? На главную</a>
        <h1>?? О проекте</h1>
        
        <div class="tech-grid">
            <div class="tech-item">
                <h3>?? Цель</h3>
                <p>Демонстрация работы с классом WebApplication в ASP.NET Core Minimal API</p>
            </div>
            
            <div class="tech-item">
                <h3>?? Архитектура</h3>
                <p>Minimal API с прямой маршрутизацией и обработкой запросов</p>
            </div>
            
            <div class="tech-item">
                <h3>?? Технологии</h3>
                <p>ASP.NET Core 8.0, C# 12, WebApplication, Middleware</p>
            </div>
            
            <div class="tech-item">
                <h3>?? Файлы</h3>
                <p>Вся логика в одном файле Program.cs</p>
            </div>
        </div>
        
        <h2>Пример кода:</h2>
        <pre style="background: #f4f4f4; padding: 15px; border-radius: 5px;">
// Создание приложения
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Добавление маршрута
app.MapGet("/", () => "Hello World!");

// Запуск приложения
app.Run();
        </pre>
    </body>
    </html>
    """;
});

// Маршрут 3: Университет
app.MapGet("/university", () =>
{
    return """
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'>
        <title>Университет</title>
        <style>
            body { font-family: Arial; max-width: 800px; margin: 0 auto; padding: 20px; }
            .back-button { display: inline-block; margin-bottom: 20px; padding: 10px 15px; 
                          background: #0066cc; color: white; text-decoration: none; border-radius: 5px; }
            .uni-info { background: #e8f4f8; padding: 20px; border-radius: 8px; margin: 20px 0; }
        </style>
    </head>
    <body>
        <a href="/" class="back-button">? На главную</a>
        <h1>?? Московский Политехнический Университет</h1>
        
        <div class="uni-info">
            <p><strong>Факультет:</strong> Информационных технологий</p>
            <p><strong>Кафедра:</strong> Информатики и информационных технологий</p>
            <p><strong>Направление:</strong> 09.03.02 «Информационные системы и технологии»</p>
        </div>
        
        <h2>?? Лабораторная работа №1</h2>
        <p><strong>Дисциплина:</strong> BackEnd-разработка</p>
        <p><strong>Тема:</strong> Создание приложения на основе класса WebApplication</p>
        <p><strong>Студент:</strong> Кондрашов Михаил Иванович (группа 241-331)</p>
        <p><strong>Дата:</strong> 03.02.2026</p>
    </body>
    </html>
    """;
});

// Маршрут 4: Паттерны проектирования (упрощенная версия)
app.MapGet("/patterns", () =>
{
    return """
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'>
        <title>Паттерны проектирования</title>
        <style>
            body { font-family: Arial; max-width: 800px; margin: 0 auto; padding: 20px; }
            .back-button { display: inline-block; margin-bottom: 20px; padding: 10px 15px; 
                          background: #0066cc; color: white; text-decoration: none; border-radius: 5px; }
            .pattern-grid { display: grid; grid-template-columns: repeat(2, 1fr); gap: 15px; margin: 20px 0; }
            .pattern-card { background: #f8f9fa; padding: 15px; border-radius: 8px; border: 1px solid #dee2e6; }
            .pattern-card h3 { margin-top: 0; color: #2c3e50; }
        </style>
    </head>
    <body>
        <a href="/" class="back-button">? На главную</a>
        <h1>?? Паттерны проектирования для бэкенда</h1>
        
        <div class="pattern-grid">
            <div class="pattern-card">
                <h3>?? Repository</h3>
                <p>Абстракция доступа к данным</p>
            </div>
            
            <div class="pattern-card">
                <h3>? Unit of Work</h3>
                <p>Управление транзакциями</p>
            </div>
            
            <div class="pattern-card">
                <h3>?? CQRS</h3>
                <p>Разделение команд и запросов</p>
            </div>
            
            <div class="pattern-card">
                <h3>?? Mediator</h3>
                <p>Посредник для коммуникации</p>
            </div>
        </div>
        
        <p>Полные примеры кода будут добавлены в следующих версиях проекта.</p>
    </body>
    </html>
    """;
});

// Маршрут 5: Детали лабораторной работы
app.MapGet("/lab-info", () =>
{
    return """
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'>
        <title>Детали лабораторной</title>
        <style>
            body { font-family: Arial; max-width: 800px; margin: 0 auto; padding: 20px; }
            .back-button { display: inline-block; margin-bottom: 20px; padding: 10px 15px; 
                          background: #0066cc; color: white; text-decoration: none; border-radius: 5px; }
            .requirement { background: #e8f4f8; padding: 15px; margin: 10px 0; border-radius: 5px; }
            .requirement.completed { border-left: 4px solid #2ecc71; }
        </style>
    </head>
    <body>
        <a href="/" class="back-button">? На главную</a>
        <h1>?? Детали лабораторной работы №1</h1>
        
        <div class="requirement completed">
            <h3>? Требование 1: Создание проекта</h3>
            <p>Проект создан с использованием команды: <code>dotnet new web</code></p>
        </div>
        
        <div class="requirement completed">
            <h3>? Требование 2: Класс WebApplication</h3>
            <p>Используется <code>WebApplication.CreateBuilder()</code> и <code>WebApplication</code></p>
        </div>
        
        <div class="requirement completed">
            <h3>? Требование 3: Несколько страниц</h3>
            <p>Реализовано 6 маршрутов: /, /about, /university, /patterns, /lab-info, /api/*</p>
        </div>
        
        <div class="requirement completed">
            <h3>? Требование 4: API endpoint</h3>
            <p>Созданы 3 API endpoint, возвращающие JSON данные</p>
        </div>
        
        <div class="requirement completed">
            <h3>? Требование 5: Комментарии к коду</h3>
            <p>Все ключевые части кода прокомментированы</p>
        </div>
        
        <h2>?? Выводы</h2>
        <ul>
            <li>Освоил создание веб-приложений на Minimal API</li>
            <li>Понял принципы работы класса WebApplication</li>
            <li>Научился создавать маршруты и возвращать разные типы контента</li>
            <li>Создал проект с практической ценностью</li>
        </ul>
    </body>
    </html>
    """;
});

// ==================== API ENDPOINTS ====================

// API 1: Основные данные
app.MapGet("/api/data", () =>
{
    return Results.Json(new
    {
        Project = "Лабораторная работа №1",
        Student = new
        {
            Name = "Кондрашов Михаил Иванович",
            Group = "241-331",
            University = "Московский Политехнический Университет"
        },
        LabInfo = new
        {
            Number = 1,
            Title = "Создание приложения на основе класса WebApplication",
            Date = "2026-02-03",
            Status = "Завершено"
        },
        Endpoints = new[] { "/", "/about", "/university", "/patterns", "/lab-info", "/api/data", "/api/student", "/api/patterns" },
        Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    });
});

// API 2: Информация о студенте
app.MapGet("/api/student", () =>
{
    return Results.Json(new
    {
        FullName = "Кондрашов Михаил Иванович",
        Group = "241-331",
        Faculty = "Информационных технологий",
        Department = "Информатики и информационных технологий",
        Program = "09.03.02 «Информационные системы и технологии»",
        LabWork = new
        {
            Number = 1,
            Subject = "BackEnd-разработка",
            Grade = "На проверке"
        }
    });
});

// API 3: Паттерны проектирования
app.MapGet("/api/patterns", () =>
{
    var patterns = new[]
    {
        new { Id = 1, Name = "Repository", Description = "Абстракция доступа к данным", Category = "Data Access" },
        new { Id = 2, Name = "Unit of Work", Description = "Управление транзакциями", Category = "Data Access" },
        new { Id = 3, Name = "CQRS", Description = "Разделение команд и запросов", Category = "Architecture" },
        new { Id = 4, Name = "Mediator", Description = "Посредник для коммуникации", Category = "Behavioral" }
    };

    return Results.Json(new
    {
        Count = patterns.Length,
        Patterns = patterns,
        Note = "Примеры паттернов для бэкенд-разработки на C#"
    });
});

// ==================== ЗАПУСК ПРИЛОЖЕНИЯ ====================
app.Run();