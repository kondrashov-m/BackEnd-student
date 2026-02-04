// Лабораторная работа №1
// Кондрашов Михаил, 241-331

using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Функция для создания HTML страниц
string CreateHtmlPage(string title, string content)
{
    var html = new StringBuilder();
    
    html.AppendLine("<!DOCTYPE html>");
    html.AppendLine("<html lang='ru'>");
    html.AppendLine("<head>");
    html.AppendLine("    <meta charset='UTF-8'>");
    html.AppendLine($"    <title>{title}</title>");
    html.AppendLine("    <style>");
    html.AppendLine("        body {");
    html.AppendLine("            font-family: Arial, sans-serif;");
    html.AppendLine("            margin: 0;");
    html.AppendLine("            padding: 20px;");
    html.AppendLine("            background: linear-gradient(135deg, #990909 0%, #887b94 100%);");
    html.AppendLine("            min-height: 100vh;");
    html.AppendLine("        }");
    html.AppendLine("        .container {");
    html.AppendLine("            max-width: 1000px;");
    html.AppendLine("            margin: 0 auto;");
    html.AppendLine("            background: white;");
    html.AppendLine("            border-radius: 15px;");
    html.AppendLine("            padding: 30px;");
    html.AppendLine("            box-shadow: 0 10px 30px rgba(0,0,0,0.2);");
    html.AppendLine("        }");
    html.AppendLine("        h1 {");
    html.AppendLine("            color: #333;");
    html.AppendLine("            text-align: center;");
    html.AppendLine("            margin-bottom: 30px;");
    html.AppendLine("        }");
    html.AppendLine("        nav {");
    html.AppendLine("            background: #2c3e50;");
    html.AppendLine("            padding: 15px;");
    html.AppendLine("            border-radius: 8px;");
    html.AppendLine("            margin: 20px 0;");
    html.AppendLine("            text-align: center;");
    html.AppendLine("        }");
    html.AppendLine("        nav a {");
    html.AppendLine("            color: white;");
    html.AppendLine("            text-decoration: none;");
    html.AppendLine("            margin: 0 15px;");
    html.AppendLine("            padding: 8px 16px;");
    html.AppendLine("            border-radius: 5px;");
    html.AppendLine("            transition: background 0.3s;");
    html.AppendLine("        }");
    html.AppendLine("        nav a:hover {");
    html.AppendLine("            background: #34495e;");
    html.AppendLine("        }");
    html.AppendLine("        .card {");
    html.AppendLine("            background: #f8f9fa;");
    html.AppendLine("            padding: 20px;");
    html.AppendLine("            margin: 15px 0;");
    html.AppendLine("            border-radius: 8px;");
    html.AppendLine("            border-left: 4px solid #3498db;");
    html.AppendLine("        }");
    html.AppendLine("        footer {");
    html.AppendLine("            margin-top: 30px;");
    html.AppendLine("            text-align: center;");
    html.AppendLine("            color: #7f8c8d;");
    html.AppendLine("            font-size: 0.9rem;");
    html.AppendLine("        }");
    html.AppendLine("    </style>");
    html.AppendLine("</head>");
    html.AppendLine("<body>");
    html.AppendLine("    <div class='container'>");
    html.AppendLine($"        <h1>{title}</h1>");
    html.AppendLine("        <nav>");
    html.AppendLine("            <a href='/'>🏠 Главная</a>");
    html.AppendLine("            <a href='/about'>📝 О проекте</a>");
    html.AppendLine("            <a href='/aspnet'>🔄 ASP.NET</a>");
    html.AppendLine("            <a href='/university'>🏛 Университет</a>");
    html.AppendLine("            <a href='/lab'>📊 О работе</a>");
    html.AppendLine("        </nav>");
    html.AppendLine($"        {content}");
    html.AppendLine("        <footer>");
    html.AppendLine("            <p>Разработал: <strong>Михаил Кондрашов</strong> | Группа: 241-331</p>");
    html.AppendLine("            <p>Портфолио: <a href='https://kondrashov-m.ru' target='_blank'>kondrashov-m.ru</a></p>");
    html.AppendLine("            <p>Московский Политехнический Университет • 2026</p>");
    html.AppendLine("        </footer>");
    html.AppendLine("    </div>");
    html.AppendLine("</body>");
    html.AppendLine("</html>");
    
    return html.ToString();
}

// Главная страница
app.MapGet("/", () => 
{
    string content = @"
        <div class='card'>
            <h2>👨‍🎓 Информация о студенте</h2>
            <p><strong>ФИО:</strong> Кондрашов Михаил Иванович</p>
            <p><strong>Группа:</strong> 241-331</p>
            <p><strong>Направление:</strong> 09.03.02 «Информационные системы и технологии»</p>
            <p><strong>Дата выполнения:</strong> 03.02.2026</p>
        </div>
        
        <div class='card'>
            <h2>🎯 Цель работы</h2>
            <p>Освоить создание веб-приложений с использованием класса <strong>WebApplication</strong> в ASP.NET Core.</p>
        </div>
        
        <div class='card'>
            <h2>💻 Технологии</h2>
            <ul>
                <li>ASP.NET Core 8.0</li>
                <li>C# 12.0</li>
                <li>Minimal API</li>
                <li>WebApplication класс</li>
                <li>HTML5 + CSS3</li>
            </ul>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("Лабораторная работа №1", content), "text/html", System.Text.Encoding.UTF8);
});

// О проекте
app.MapGet("/about", () => 
{
    string content = @"
        <div class='card'>
            <h2>О проекте</h2>
            <p>Данное веб-приложение создано в рамках лабораторной работы №1 по дисциплине ""BackEnd-разработка"".</p>
            <p>Проект демонстрирует использование класса WebApplication в ASP.NET Core для создания минимального веб-приложения.</p>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("О проекте", content), "text/html", System.Text.Encoding.UTF8);
});

// ASP.NET Core
app.MapGet("/aspnet", () => 
{
    string content = @"
        <div class='card'>
            <h2>ASP.NET Core</h2>
            <p>Кросс-платформенный фреймворк для создания веб-приложений на C#.</p>
            <p>В данной работе используется Minimal API - упрощенный подход к созданию веб-приложений.</p>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("ASP.NET Core", content), "text/html", System.Text.Encoding.UTF8);
});

// Университет
app.MapGet("/university", () => 
{
    string content = @"
        <div class='card'>
            <h2>Московский Политехнический Университет</h2>
            <p><strong>Факультет:</strong> Информационных технологий</p>
            <p><strong>Кафедра: ИиИТ</strong> </p>
            <p><strong>Направление:</strong> 09.03.02 «Информационные системы и технологии»</p>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("Университет", content), "text/html", System.Text.Encoding.UTF8);
});

// Лабораторная работа
app.MapGet("/lab", () => 
{
    string content = @"
        <div class='card'>
            <h2>Лабораторная работа №1</h2>
            <p><strong>Дисциплина:</strong> BackEnd-разработка</p>
            <p><strong>Тема:</strong> Создание приложения на основе класса WebApplication</p>
            <p><strong>Требования:</strong></p>
            <ul>
                <li>✅ Создание проекта на базе WebApplication</li>
                <li>✅ Несколько минимальных страниц</li>
                <li>✅ Использование ASP.NET Core</li>
            </ul>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("О работе", content), "text/html", System.Text.Encoding.UTF8);
});

app.Run();