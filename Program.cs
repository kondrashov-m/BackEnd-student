// Лабораторная работа №2
// Кондрашов Михаил, 241-331

using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.DependencyInjection; 

// DI контейнер ASP.NET CORE
var builder = Host.CreateApplicationBuilder(args); // команда для создания контейнера


builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IUniversityService, UniversityService>();
builder.Services.AddTransient<IBebraService, BebraService>();
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

public interface IBebraService
{
    void Bababa();
}

// Реализации
public class StudentService : IStudentService
{
       
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
    public void ShowUniversityInfo()
    {
        Console.WriteLine("");
        Console.WriteLine("=== УНИВЕРСИТЕТ ===");
        Console.WriteLine("");
        Console.WriteLine("Московский Политехнический Университет");
        Console.WriteLine("Факультет: Информационных технологий");
    }
}

public class BebraService : IBebraService
{
    public void Bababa()
    {
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");
        Console.WriteLine("                                         ");

    }
}

// Главное приложение
public class ConsoleApp
{
    private readonly IStudentService _studentService;
    private readonly IUniversityService _universityService;
    private readonly IBebraService _bebraService;

    
    public ConsoleApp(
        IStudentService studentService,
        IUniversityService universityService,
        IBebraService bebraService)
    {
        _studentService = studentService;
        _universityService = universityService;
        _bebraService = bebraService;
    }
    
    public void Run()
    {
        Console.WriteLine("");
        Console.WriteLine("     Лабораторная работа 2       ");
        
        while (true)
        {
            Console.WriteLine("_____________________________");
            Console.WriteLine("");
            Console.WriteLine("1. Информация о студенте");
            Console.WriteLine("2. Информация об университете");
            Console.WriteLine("3. О работе");
            Console.WriteLine("4. Портфолио разработчика");
            Console.WriteLine("0. Выход");
            
            _bebraService.Bababa();

            Console.WriteLine("");
            Console.Write("Выберите: ");
            var choice = Console.ReadLine();
            
            if (choice == "1")
                _studentService.ShowStudentInfo();
            else if (choice == "2")
                _universityService.ShowUniversityInfo();
            else if (choice == "3")
                _bebraService.Bababa();
            else if (choice == "0")
                break;
        }
        
    
    }
}