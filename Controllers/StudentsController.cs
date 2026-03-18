using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> _students = new();

    static StudentsController()
    {
        var student1 = new Student
        {
            Id = 1,
            FirstName = "Михаил",
            LastName = "Кондрашов",
            Email = "m.kondrashov@example.com",
            EnrollmentDate = DateTime.Now.AddMonths(-6)
        };
        student1.Courses.Add(new Course { Id = 101, Name = "Математика", Credits = 3, Grade = "A" });
        student1.Courses.Add(new Course { Id = 102, Name = "Физика", Credits = 4, Grade = "B+" });
        
        var student2 = new Student
        {
            Id = 2,
            FirstName = "Петр",
            LastName = "Новиков",
            Email = "p.novikov@example.com",
            EnrollmentDate = DateTime.Now.AddMonths(-3)
        };
        student2.Courses.Add(new Course { Id = 101, Name = "Математика", Credits = 3, Grade = "A-" });
        student2.Courses.Add(new Course { Id = 103, Name = "Информатика", Credits = 3, Grade = "A" });

        var student3 = new Student
        {
            Id = 3,
            FirstName = "Владимир",
            LastName = "Путин",
            Email = "v.putin@example.com",
            EnrollmentDate = DateTime.Now.AddMonths(-12)
        };
        student3.Courses.Add(new Course { Id = 104, Name = "Политология", Credits = 2, Grade = "A+" });

        _students.AddRange(new[] { student1, student2, student3 });
    }


    [HttpGet]
    public IActionResult GetStudents()
    {
        return Ok(_students);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound(new { Message = $"Студент с ID {id} не найден" });

        return Ok(student);
    }

  
    [HttpGet("{id:int}/courses")]
    public IActionResult GetStudentCourses(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound(new { Message = $"Студент с ID {id} не найден" });

        return Ok(student.Courses);
    }

    [HttpGet("{id:int}/courses/{courseId:int}")]
    public IActionResult GetStudentCourseById(int id, int courseId)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound(new { Message = $"Студент с ID {id} не найден" });

        var course = student.Courses.FirstOrDefault(c => c.Id == courseId);
        if (course == null)
            return NotFound(new { Message = $"Курс с ID {courseId} не найден у студента {id}" });

        return Ok(course);
    }


    [HttpGet("by-email/{email}")]
    public IActionResult GetStudentByEmail(string email)
    {
        var student = _students.FirstOrDefault(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (student == null)
            return NotFound(new { Message = $"Студент с email {email} не найден" });

        return Ok(student);
    }

    [HttpGet("enrollment/{date:datetime}")]
    public IActionResult GetStudentsEnrolledAfter(DateTime date)
    {
        var students = _students.Where(s => s.EnrollmentDate >= date).ToList();
        return Ok(students);
    }


    [HttpGet("search")]
    public IActionResult SearchStudents(
        [FromQuery] string? firstName = null,
        [FromQuery] string? lastName = null,
        [FromQuery] int? year = null)
    {
        var query = _students.AsEnumerable();

        if (!string.IsNullOrEmpty(firstName))
            query = query.Where(s => s.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase));
        
        if (!string.IsNullOrEmpty(lastName))
            query = query.Where(s => s.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
        
        if (year.HasValue)
            query = query.Where(s => s.EnrollmentDate.Year == year.Value);

        return Ok(query.ToList());
    }
}