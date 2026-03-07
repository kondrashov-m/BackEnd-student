using Microsoft.AspNetCore.Mvc;
using BackEnd_student.Models;
using BackEnd_student.Models.DTOs;

namespace BackEnd_student.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<Student> _students = new List<Student>
    {
        new Student
        {
            Id = 1,
            FullName = "Кондрашов Михаил Иванович",
            Group = "241-331",
            Age = 19,
            CreatedAt = new DateTime(2026, 2, 3)
        },
        new Student
        {
            Id = 2,
            FullName = "Петр Новиков",
            Group = "241-331",
            Age = 21,
            CreatedAt = DateTime.Now.AddDays(-5)
        },
        new Student
        {
            Id = 3,
            FullName = "Вова Путин",
            Group = "241-777",
            Age = 73,
            CreatedAt = DateTime.Now.AddDays(-10)
        }
    };

    private static int _nextId = 4;

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        return Ok(_students);
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound(new { message = $"Студент с ID {id} не найден" });

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> CreateStudent([FromBody] CreateStudentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = new Student
        {
            Id = _nextId++,
            FullName = dto.FullName,
            Group = dto.Group,
            Age = dto.Age,
            CreatedAt = DateTime.Now
        };

        _students.Add(student);

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound(new { message = $"Студент с ID {id} не найден" });

        student.FullName = dto.FullName;
        student.Group = dto.Group;
        student.Age = dto.Age;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound(new { message = $"Студент с ID {id} не найден" });

        _students.Remove(student);
        return NoContent();
    }
}