// Лабораторная работа №2
// Кондрашов Михаил, 241-331

using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.DependencyInjection; 

// DI контейнер ASP.NET CORE
var builder = Host.CreateApplicationBuilder(args); // команда для создания контейнера

builder.Services.AddSingleton<ILogger, ConsoleLogger>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IUniversityService, UniversityService>();
builder.Services.AddTransient<ConsoleApp>();

var host = builder.Build();
var app = host.Services.GetRequiredService<ConsoleApp>();
app.Run();

// Интерфейсы
public interface IStudentService
{
    void ShowStudentInfo();
}

public interface IUniversityService
{
    void ShowUniversityInfo();
}

public interface ILogger
{
    void Log(string message);
}

// Реализации
public class StudentService : IStudentService
{
    private readonly ILogger _logger;
    
    public StudentService(ILogger logger)
    {
        _logger = logger;
    }
    
    public void ShowStudentInfo()
    {
        Console.WriteLine("=== СТУДЕНТ ===");
        Console.WriteLine("");
        Console.WriteLine("ФИО: Кондрашов Михаил Иванович");
        Console.WriteLine("Группа: 241-331");
        Console.WriteLine("Дата: 03.02.2026");
    }
}

public class UniversityService : IUniversityService
{
    private readonly ILogger _logger;
    
    public UniversityService(ILogger logger)
    {
        _logger = logger;
    }
    
    public void ShowUniversityInfo()
    {
        _logger.Log("Показываю информацию об университете...");
        Console.WriteLine("");
        Console.WriteLine("=== УНИВЕРСИТЕТ ===");
        Console.WriteLine("");
        Console.WriteLine("Московский Политехнический Университет");
        Console.WriteLine("Факультет: Информационных технологий");
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}

// Главное приложение
public class ConsoleApp
{
    private readonly IStudentService _studentService;
    private readonly IUniversityService _universityService;
    private readonly ILogger _logger;
    
    public ConsoleApp(
        IStudentService studentService,
        IUniversityService universityService,
        ILogger logger)
    {
        _studentService = studentService;
        _universityService = universityService;
        _logger = logger;
    }
    
    public void Run()
    {
        
        Console.WriteLine("Лабораторная работа 2");
        
        while (true)
        {
            Console.WriteLine("_____________________________");
            Console.WriteLine("");
            Console.WriteLine("1. Информация о студенте");
            Console.WriteLine("2. Информация об университете");
            Console.WriteLine("0. Выход");
            
            Console.WriteLine("");
            Console.Write("Выберите: ");
            var choice = Console.ReadLine();
            
            if (choice == "1")
                _studentService.ShowStudentInfo();
            else if (choice == "2")
                _universityService.ShowUniversityInfo();
            else if (choice == "0")
                break;
        }
        
    
    }
}