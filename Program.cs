// Лабораторная работа №2
// Кондрашов Михаил, 241-331

using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Временное хранилище данных 

static List<Student> students = new List<Student>
{
    new Student { Id = 1, Name = "Михаил Кондрашов", Age = 19, Group = "241-331" },
    new Student { Id = 2, Name = "Петр Новиков", Age = 21, Group = "241-331" },
    new Student { Id = 3, Name = "Вова Путин", Age = 73 , Group = "241-777" }
};

static List<University> universities = new List<University>
{
    new University { Id = 1, Name = "Московский Политех", Faculty = "Факультет Информационных технологий" },
    new University { Id = 2, Name = "СПбГУ", Faculty = "Засекреченный факультет" }
};

// из лабораторной работы №1 (начало)

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
    html.AppendLine("        .method-badge {");
    html.AppendLine("            display: inline-block;");
    html.AppendLine("            padding: 5px 10px;");
    html.AppendLine("            border-radius: 5px;");
    html.AppendLine("            font-weight: bold;");
    html.AppendLine("            margin-right: 10px;");
    html.AppendLine("        }");
    html.AppendLine("        .get { background: #61affe; color: white; }");
    html.AppendLine("        .post { background: #49cc90; color: white; }");
    html.AppendLine("        .put { background: #fca130; color: white; }");
    html.AppendLine("        .patch { background: #50e3c2; color: white; }");
    html.AppendLine("        footer {");
    html.AppendLine("            margin-top: 30px;");
    html.AppendLine("            text-align: center;");
    html.AppendLine("            color: #7f8c8d;");
    html.AppendLine("            font-size: 0.9rem;");
    html.AppendLine("        }");
    html.AppendLine("        pre {");
    html.AppendLine("            background: #f4f4f4;");
    html.AppendLine("            padding: 10px;");
    html.AppendLine("            border-radius: 5px;");
    html.AppendLine("            overflow-x: auto;");
    html.AppendLine("        }");
    html.AppendLine("    </style>");
    html.AppendLine("</head>");
    html.AppendLine("<body>");
    html.AppendLine("    <div class='container'>");
    html.AppendLine($"        <h1>{title}</h1>");
    html.AppendLine("        <nav>");
    html.AppendLine("            <a href='/'>🏠 Главная</a>");
    html.AppendLine("            <a href='/about'>📝 О проекте</a>");
    html.AppendLine("            <a href='/api-docs'>📚 API методы</a>");
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


app.MapGet("/", () => 
{
    string content = @"
        <div class='card'>
            <h2>👨‍🎓 Информация о студенте</h2>
            <p><strong>ФИО:</strong> Кондрашов Михаил Иванович</p>
            <p><strong>Группа:</strong> 241-331</p>
            <p><strong>Направление:</strong> 09.03.02 «Информационные системы и технологии»</p>
            <p><strong>Дата выполнения:</strong> 25.02.2026</p>
        </div>
        
        <div class='card'>
            <h2>🎯 Цель работы №2</h2>
            <p>Изучить способы передачи данных на сервер в ASP.NET Core Web API: query-параметры, тело запроса, смешанный способ.</p>
            <p>Освоить HTTP методы: GET, POST, PUT, PATCH.</p>
        </div>
        
        <div class='card'>
            <h2>🔌 Доступные API методы</h2>
            <p>Перейдите в раздел <a href='/api-docs'>API методы</a> для тестирования.</p>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("Лабораторная работа №2", content), "text/html", Encoding.UTF8);
});

app.MapGet("/about", () => 
{
    string content = @"
        <div class='card'>
            <h2>О проекте</h2>
            <p>Данное веб-приложение создано в рамках лабораторной работы №2 по дисциплине ""BackEnd-разработка"".</p>
            <p>Проект демонстрирует работу с различными HTTP методами и способами передачи данных.</p>
            <p>Код основан на лабораторной работе №1 (ветка lab1) и расширен новыми API-обработчиками.</p>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("О проекте", content), "text/html", Encoding.UTF8);
});

app.MapGet("/university", () => 
{
    string content = @"
        <div class='card'>
            <h2>Московский Политехнический Университет</h2>
            <p><strong>Факультет:</strong> Информационных технологий</p>
            <p><strong>Кафедра:</strong> Инфокогнитивные технологии</p>
            <p><strong>Направление:</strong> 09.03.02 «Информационные системы и технологии»</p>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("Университет", content), "text/html", Encoding.UTF8);
});

app.MapGet("/lab", () => 
{
    string content = @"
        <div class='card'>
            <h2>Лабораторная работа №2</h2>
            <p><strong>Дисциплина:</strong> BackEnd-разработка</p>
            <p><strong>Тема:</strong> HTTP методы и передача данных на сервер</p>
            <p><strong>Выполненные пункты:</strong></p>
            <ul>
                <li>✅ GET с query-параметрами (/api/students/filter)</li>
                <li>✅ POST с телом запроса (/api/students)</li>
                <li>✅ POST смешанный (query + тело) (/api/universities/{id}/students)</li>
                <li>✅ PUT полное обновление (/api/students/{id})</li>
                <li>✅ PATCH частичное обновление (/api/students/{id})</li>
            </ul>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("О работе", content), "text/html", Encoding.UTF8);
});

app.MapGet("/api-docs", () => 
{
    string content = @"
        <div class='card'>
            <h2>📋 Доступные API методы</h2>
            
            <h3><span class='method-badge get'>GET</span> /api/students</h3>
            <p>Получить список всех студентов</p>
            
            <h3><span class='method-badge get'>GET</span> /api/students/{id}</h3>
            <p>Получить студента по ID</p>
            
            <h3><span class='method-badge get'>GET</span> /api/students/filter?group=241-331&minAge=18</h3>
            <p>Фильтрация студентов через query-параметры</p>
            
            <h3><span class='method-badge post'>POST</span> /api/students</h3>
            <p>Создать нового студента (данные в теле запроса)</p>
            <pre>{
    'name': 'Иван Иванов',
    'age': 20,
    'group': '241-333'
}</pre>
            
            <h3><span class='method-badge post'>POST</span> /api/universities/{universityId}/students</h3>
            <p>Добавить студента в университет (ID университета в адресе, студент в теле)</p>
            
            <h3><span class='method-badge put'>PUT</span> /api/students/{id}</h3>
            <p>Полностью обновить данные студента</p>
            
            <h3><span class='method-badge patch'>PATCH</span> /api/students/{id}</h3>
            <p>Частично обновить данные студента (например, только имя)</p>
            
            <h3><span class='method-badge get'>GET</span> /api/universities</h3>
            <p>Получить список университетов</p>
        </div>
        
        <div class='card'>
            <h2>🛠 Как тестировать</h2>
            <p>Используйте <strong>Postman</strong> или любой другой HTTP-клиент для отправки запросов.</p>
            <p>Базовый URL: <code>http://localhost:5000</code> или <code>https://localhost:5001</code></p>
        </div>
    ";
    
    return Results.Text(CreateHtmlPage("API методы", content), "text/html", Encoding.UTF8);
});
// из лабораторной работы №1 (конец)

// API методы

// GET всех студентов
app.MapGet("/api/students", () =>
{
    return Results.Json(students);
});

// GET одного студента по ID
app.MapGet("/api/students/{id}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    if (student == null)
    {
        return Results.NotFound(new { Message = $"Студент с ID {id} не найден" });
    }
    return Results.Json(student);
});

// 1. GET с query-параметрами
app.MapGet("/api/students/filter", (string? group, int? minAge) =>
{
    var filteredStudents = students.AsEnumerable();
    
    if (!string.IsNullOrEmpty(group))
    {
        filteredStudents = filteredStudents.Where(s => s.Group.Contains(group));
    }
    
    if (minAge.HasValue)
    {
        filteredStudents = filteredStudents.Where(s => s.Age >= minAge.Value);
    }
    
    return Results.Json(new
    {
        Count = filteredStudents.Count(),
        Students = filteredStudents,
        Filters = new { Group = group, MinAge = minAge }
    });
});

// 2. POST с телом запроса
app.MapPost("/api/students", (Student newStudent) =>
{
    if (string.IsNullOrEmpty(newStudent.Name))
    {
        return Results.BadRequest(new { Message = "Имя студента обязательно" });
    }
    
    newStudent.Id = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
    
    students.Add(newStudent);
    
    return Results.Created($"/api/students/{newStudent.Id}", new
    {
        Message = "Студент успешно создан",
        Student = newStudent
    });
});

// 3. POST с query и телом запароса 
app.MapPost("/api/universities/{universityId}/students", (int universityId, Student newStudent) =>
{
    var university = universities.FirstOrDefault(u => u.Id == universityId);
    if (university == null)
    {
        return Results.NotFound(new { Message = $"Университет с ID {universityId} не найден" });
    }
    
    if (string.IsNullOrEmpty(newStudent.Name))
    {
        return Results.BadRequest(new { Message = "Имя студента обязательно" });
    }
    
    newStudent.Id = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
    students.Add(newStudent);
    
    return Results.Created($"/api/students/{newStudent.Id}", new
    {
        Message = $"Студент добавлен в университет '{university.Name}'",
        UniversityId = universityId,
        UniversityName = university.Name,
        Student = newStudent
    });
});

// 4. PUT с приёмом данных из тела запроса
app.MapPut("/api/students/{id}", (int id, Student updatedStudent) =>
{
    var existingStudent = students.FirstOrDefault(s => s.Id == id);
    if (existingStudent == null)
    {
        return Results.NotFound(new { Message = $"Студент с ID {id} не найден" });
    }

    existingStudent.Name = updatedStudent.Name;
    existingStudent.Age = updatedStudent.Age;
    existingStudent.Group = updatedStudent.Group;
    
    return Results.Json(new
    {
        Message = $"Студент с ID {id} полностью обновлен",
        Student = existingStudent
    });
});

// 5. PATCH частичное обновление данных
app.MapPatch("/api/students/{id}", (int id, JsonElement partialData) =>
{
    var existingStudent = students.FirstOrDefault(s => s.Id == id);
    if (existingStudent == null)
    {
        return Results.NotFound(new { Message = $"Студент с ID {id} не найден" });
    }
    
    var updatedFields = new List<string>();
    
    // Обновляем только те поля, которые пришли
    if (partialData.TryGetProperty("name", out var name) && name.ValueKind == JsonValueKind.String)
    {
        existingStudent.Name = name.GetString() ?? existingStudent.Name;
        updatedFields.Add("name");
    }
    
    if (partialData.TryGetProperty("age", out var age) && age.ValueKind == JsonValueKind.Number)
    {
        existingStudent.Age = age.GetInt32();
        updatedFields.Add("age");
    }
    
    if (partialData.TryGetProperty("group", out var group) && group.ValueKind == JsonValueKind.String)
    {
        existingStudent.Group = group.GetString() ?? existingStudent.Group;
        updatedFields.Add("group");
    }
    
    return Results.Json(new
    {
        Message = $"Студент с ID {id} частично обновлен",
        UpdatedFields = updatedFields,
        Student = existingStudent
    });
});

app.MapGet("/api/universities", () =>
{
    return Results.Json(universities);
});

app.Run();

