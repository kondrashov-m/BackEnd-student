// Лабораторная работа №2
// Кондрашов Михаил, 241-331

using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.DependencyInjection; 

// DI контейнер ASP.NET CORE
var builder = Host.CreateApplicationBuilder(args); // команда для создания контейнера

builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IUniversityService, UniversityService>();
builder.Services.AddTransient<IJobService, JobService>();
builder.Services.AddTransient<IDeveloperService, DeveloperService>();
builder.Services.AddTransient<IVopros, Vopros>();
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

public interface IJobService
{
    void ShowJobService();
}

public interface IDeveloperService
{
    void ShowDeveloperService();
}

public interface IVopros
{
    void ShowVopros();
}

// Реализации
public class StudentService : IStudentService
{
       
    public void ShowStudentInfo()
    {
        Console.Clear();
        Console.WriteLine("         Информация о студенте");
        Console.WriteLine("_________________________________________");
        Console.WriteLine("");
        Console.WriteLine("ФИО: Кондрашов Михаил Иванович");
        Console.WriteLine("Группа: 241-331");
        Console.WriteLine("Направление: АСОИиУ");
        Console.WriteLine("");
    }
}

public class UniversityService : IUniversityService
{
    public void ShowUniversityInfo()
    {
        Console.Clear();
        Console.WriteLine("        Информация об университете");
        Console.WriteLine("_________________________________________");
        Console.WriteLine("");
        Console.WriteLine("ВУЗ: Московский Политехнический Университет");
        Console.WriteLine("Факультет: ФИТ");
        Console.WriteLine("");
    }
}

public class JobService : IJobService
{
    public void ShowJobService()
    {
        Console.Clear();
        Console.WriteLine("                 О работе");
        Console.WriteLine("_________________________________________");
        Console.WriteLine("");
        Console.WriteLine("Номер лабораторной работы: 2");
        Console.WriteLine("Тема: Создание приложения на основе класса"); 
        Console.WriteLine("WebApplication на основе ASP.NET Core 2");
        Console.WriteLine("");
    }
    
}

public class DeveloperService : IDeveloperService
{
    public void ShowDeveloperService()
    {
        Console.Clear();
        Console.WriteLine("             О разработчике");
        Console.WriteLine("_________________________________________");
        Console.WriteLine("");
        Console.WriteLine("Разработчик: Михаил Кондрашов");
        Console.WriteLine("Личный сайт разработчика: kondrashov-m.ru"); 
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("           kondrashov-m 2026");
        Console.WriteLine("");
    }
    
}

public class Vopros : IVopros
{
    public void ShowVopros()
    {
        Console.Clear();
        Console.WriteLine("                  Ой... ");
        Console.WriteLine("_________________________________________");
        Console.WriteLine("");
        Console.WriteLine("⚠️ Такой цифры тут нет!");
        Console.WriteLine("");

    }
    
}

// Главное приложение
public class ConsoleApp
{
    private readonly IStudentService _studentService;
    private readonly IUniversityService _universityService;
    private readonly IJobService _jobService;
    private readonly IDeveloperService _developerService;
    private readonly IVopros _vopros;
 
    public ConsoleApp(
        IStudentService studentService,
        IUniversityService universityService,
        IJobService jobService,
        IDeveloperService developerService,
        IVopros vopros)
    {
        _studentService = studentService;
        _universityService = universityService;
        _jobService = jobService;
        _developerService = developerService;
        _vopros = vopros;
    }
    
    public void Run()
    {
        Console.Clear();
        Console.WriteLine("           Лабораторная работа 2         ");
        Console.WriteLine("_________________________________________");
        Console.WriteLine("");
        
        while (true)
        {
            
            
            Console.WriteLine("1. Информация о студенте");
            Console.WriteLine("2. Информация об университете");
            Console.WriteLine("3. О работе");
            Console.WriteLine("4. О разработчике");
            Console.WriteLine("5. ?");
            Console.WriteLine("0. Выход");
            
            

            Console.WriteLine("");
            Console.Write("Выберите: ");
            var choice = Console.ReadLine();
            
            if (choice == "1")
                _studentService.ShowStudentInfo();
            else if (choice == "2")
                _universityService.ShowUniversityInfo();
            else if (choice == "3")
                _jobService.ShowJobService();
            else if (choice == "4")
                _developerService.ShowDeveloperService();
            else if (choice == "0")
                break;
            else
                _vopros.ShowVopros();
        }
        
    
    }
}