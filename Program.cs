// Лабораторная работа №3
// Кондрашов Михаил, 241-331

using System.Text;
using System.Text.Json;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
    var data = new[]
    {
        new { id = 1, name = "Михаил Кондрашов", age = 19, group = "241-331" },
        new { id = 2, name = "Петр Новиков", age = 21, group = "241-331" },
        new { id = 3, name = "Владимир Путин", age = 73, group = "241-777" }
    };
    
    var options = new JsonSerializerOptions 
    { 
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    
    return Results.Json(data, options);
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
    <students>
        <student><id>1</id><name>Михаил Кондрашов</name><age>19</age><group>241-331</group></student>
        <student><id>2</id><name>Петр Новиков</name><age>21</age><group>241-331</group></student>
        <student><id>3</id><name>Владимир Путин</name><age>73</age><group>241-777</group></student>
    </students>
    <time>" + DateTime.Now + @"</time>
</response>";
    
    return Results.Text(xml, "application/xml", Encoding.UTF8);
});

app.MapGet("/csv", () =>
{
    var csv = new StringBuilder();
    csv.AppendLine("Id,Name,Age,Group");
    csv.AppendLine("1,Михаил Кондрашов,19,241-331");
    csv.AppendLine("2,Петр Новиков,21,241-331");
    csv.AppendLine("3,Владимир Путин,73,241-777");
    
    byte[] bom = new byte[] { 0xEF, 0xBB, 0xBF };
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
    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "1.png");
    
    if (!File.Exists(imagePath))
    {
        return Results.Problem("Файл 1.png не найден в папке проекта");
    }
    
    byte[] imageBytes = File.ReadAllBytes(imagePath);
    return Results.Bytes(imageBytes, "image/png", "image.png");
});

app.MapGet("/pdf", () =>
{
    var stream = new MemoryStream();
    
    var document = Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            
            page.Header()
                .Text("Лабораторная работа №3")
                .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);
            
            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .Column(column =>
                {
                    column.Item().Text($"Студент: Кондрашов Михаил").FontSize(14);
                    column.Item().Text($"Группа: 241-331").FontSize(14);
                    column.Item().Text($"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(14);
                    column.Item().Text($"Тип ответа: application/pdf").FontSize(14);
                    
                    column.Item().PaddingTop(20).Text("Данные студентов:").FontSize(16).Bold();
                    
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);
                            columns.RelativeColumn();
                            columns.ConstantColumn(50);
                            columns.ConstantColumn(80);
                        });
                        
                        table.Header(header =>
                        {
                            header.Cell().Text("ID");
                            header.Cell().Text("Имя");
                            header.Cell().Text("Возраст");
                            header.Cell().Text("Группа");
                        });
                        
                        table.Cell().Text("1");
                        table.Cell().Text("Михаил Кондрашов");
                        table.Cell().Text("19");
                        table.Cell().Text("241-331");
                        
                        table.Cell().Text("2");
                        table.Cell().Text("Петр Новиков");
                        table.Cell().Text("21");
                        table.Cell().Text("241-331");
                        
                        table.Cell().Text("3");
                        table.Cell().Text("Владимир Путин");
                        table.Cell().Text("73");
                        table.Cell().Text("241-777");
                    });
                });
            
            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Страница ");
                    x.CurrentPageNumber();
                });
        });
    });
    
    document.GeneratePdf(stream);
    return Results.Bytes(stream.ToArray(), "application/pdf", "laboratornaya_3.pdf");
});

app.MapGet("/redirect", () => Results.Redirect("/html", false));

app.MapGet("/redirect-permanent", () => Results.Redirect("/plain", true));

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
</body>
</html>";
    
    return Results.Text(html, "text/html", Encoding.UTF8);
});

app.Run();