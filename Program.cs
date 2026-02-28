using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddXmlSerializerFormatters();  // ← Включаем XML
var app = builder.Build();


// Временное хранилище данных
List<Student> students = new List<Student>
{
    new Student { Id = 1, Name = "Михаил Кондрашов", Age = 19, Group = "241-331" },
    new Student { Id = 2, Name = "Петр Новиков", Age = 21, Group = "241-331" },
    new Student { Id = 3, Name = "Вова Путин", Age = 73 , Group = "241-777" }
};

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

app.MapGet("/plain", () =>
{
    string text = "Лабораторная работа №3\n";
    text += "Тип ответа: text/plain\n";
    text += $"Студент: Кондрашов Михаил\n";
    text += $"Группа: 241-331\n";
    text += $"Время: {DateTime.Now}\n";
    
    return Results.Text(text, "text/plain", Encoding.UTF8);
});

app.MapGet("/json", () =>
{

    return Results.Json(students);
});

app.MapGet("/xml", () =>
{
    string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
    <lab>3</lab>
    <student>
        <name>Кондрашов Михаил</name>
        <group>241-331</group>
    </student>
    <time>" + DateTime.Now + @"</time>
</response>";
    
    return Results.Text(xml, "application/xml", Encoding.UTF8);
});

app.MapGet("/csv", () =>
{
    var csv = new StringBuilder();
    
    byte[] bom = new byte[] { 0xEF, 0xBB, 0xBF };
    
    csv.AppendLine("Id,Name,Age,Group");
    foreach (var s in students)
        csv.AppendLine($"{s.Id},{s.Name},{s.Age},{s.Group}");
    
    byte[] csvBytes = bom.Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
    
    return Results.Bytes(csvBytes, "text/csv", "students.csv");
});

app.MapGet("/binary", () =>
{
    string data = $"Бинарные данные\nСтудент: Кондрашов Михаил\nВремя: {DateTime.Now}";
    byte[] bytes = Encoding.UTF8.GetBytes(data);
    return Results.Bytes(bytes, "application/octet-stream", "data.bin");
});

app.MapGet("/image", () =>
{
    string svg = $@"<svg width='400' height='200' xmlns='http://www.w3.org/2000/svg'>
        <rect width='400' height='200' fill='#4CAF50'/>
        <text x='20' y='50' fill='white' font-size='20'>Кондрашов Михаил</text>
        <text x='20' y='100' fill='white' font-size='20'>241-331</text>
        <text x='20' y='150' fill='white' font-size='15'>{DateTime.Now:HH:mm:ss}</text>
    </svg>";
    
    byte[] img = Encoding.UTF8.GetBytes(svg);
    return Results.Bytes(img, "image/svg+xml", "image.svg");
});

app.MapGet("/pdf", () =>
{
    string html = $@"<!DOCTYPE html>
<html>
<head><meta charset='UTF-8'></head>
<body>
    <h1>Отчет по лабораторной работе №3</h1>
    <p>Студент: Кондрашов Михаил</p>
    <p>Группа: 241-331</p>
    <p>Дата: {DateTime.Now}</p>
    <table border='1'>
        <tr><th>ID</th><th>Имя</th><th>Возраст</th><th>Группа</th></tr>";
    
    foreach (var s in students)
        html += $"<tr><td>{s.Id}</td><td>{s.Name}</td><td>{s.Age}</td><td>{s.Group}</td></tr>";
    
    html += "</table></body></html>";
    
    byte[] pdf = Encoding.UTF8.GetBytes(html);
    return Results.Bytes(pdf, "application/pdf", "report.pdf");
});

// 9. Редирект 302 (временный)
app.MapGet("/redirect", () => Results.Redirect("/html", false));

// 10. Редирект 301 (постоянный)
app.MapGet("/redirect-permanent", () => Results.Redirect("/text", true));

app.MapGet("/", () =>
{
    string html = @"<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Лабораторная работа №3</title>
</head>
<body style='font-family: Arial; padding: 20px;'>
    <h1>Лабораторная работа №3</h1>
    <h2>Студент: Кондрашов Михаил, группа 241-331</h2>
    <ul>
        <li><a href='/html'>1. HTML ответ (text/html)</a></li>
        <li><a href='/plain'>2. Текстовый ответ (text/plain)</a></li>
        <li><a href='/json'>3. JSON данные (application/json)</a></li>
        <li><a href='/xml'>4. XML ответ (application/xml)</a></li>
        <li><a href='/csv'>5. CSV данные (text/csv)</a></li>
        <li><a href='/binary'>6. Бинарные данные (application/octet-stream)</a></li>
        <li><a href='/image'>7. Изображение (image/svg+xml)</a></li>
        <li><a href='/pdf'>8. PDF файл (application/pdf)</a></li>
        <li><a href='/redirect'>9. Редирект 302 → /html</a></li>
        <li><a href='/redirect-permanent'>10. Редирект 301 → /text</a></li>
    </ul>
</body>
</html>";
    
    return Results.Text(html, "text/html", Encoding.UTF8);
});

app.Run();

// Класс Student
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Group { get; set; } = "";
}